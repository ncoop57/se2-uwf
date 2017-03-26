using Android.App;
using Android.Widget;
using Android.OS;
using Android.Speech;
using Android.Content;
using System;
using Interfaces;
using Implementations;
using System.Collections.Generic;
using Android.Content.Res;

namespace DoWhat
{
    [Activity(Label = "DoWhat", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        private ISpeech speech;
        private IStringMatcher commandMatcher;
        private IStringMatcher applicationMatcher;
        private Button recordBtn;
        private TextView textBox;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            speech = new Speech(10);

            commandMatcher = new CommandStringMatcher(this.Assets.Open("dictionary.txt"));
            applicationMatcher = new ApplicationStringMatcher(this.getApplications());



            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // get the resources from the layoutW
            recordBtn = FindViewById<Button>(Resource.Id.recordBtn);
            textBox = FindViewById<TextView>(Resource.Id.outputTxt);


            recordBtn.Click += delegate
            {
                
                // create the intent and start the activity
                var voiceIntent = speech.setUpIntent(new Intent());
                StartActivityForResult(voiceIntent, speech.VOICE);

            };


        }

        protected override void OnActivityResult(int requestCode, Result resultVal, Intent data)
        {

            if (requestCode == speech.VOICE)
            {
                if (resultVal == Result.Ok)
                {
                    var matches = data.GetStringArrayListExtra(RecognizerIntent.ExtraResults);
                    if (matches.Count != 0)
                    {
                        string textInput = matches[0];

                        // limit the output to 500 characters
                        if (textInput.Length > 500)
                            textInput = textInput.Substring(0, 500);
                        textBox.Text = textInput;



                        string processed = commandMatcher.process(textInput);
                        applicationMatcher.process(processed);                        

                        Action action = new Action(commandMatcher, applicationMatcher, this);
                        action.run();

                    }
                    else
                        textBox.Text = "No speech was recognised";
                    // change the text back on the button
                    recordBtn.Text = "Start Recording";
                }
            }

            base.OnActivityResult(requestCode, resultVal, data);
        }

        public void openApplication(string application)
        {

            Intent intent = PackageManager.GetLaunchIntentForPackage(application);
            StartActivity(intent);

        }

        public IList<string> getApplications()
        {

            IList<string> applications = new List<string>();

            foreach (var item in PackageManager.GetInstalledApplications(new Android.Content.PM.PackageInfoFlags()))
            {

                var context = CreatePackageContext(item.PackageName, PackageContextFlags.IgnoreSecurity);
                applications.Add(context.PackageName);
                Console.WriteLine(context.PackageName);

            }

            return applications;

        }

    }
    
}

