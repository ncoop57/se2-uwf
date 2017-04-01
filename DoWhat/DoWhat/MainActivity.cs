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

        // Instances variable
        private ISpeech speech;
        private IStringMatcher commandMatcher;
        private Context context;
        //private IStringMatcher applicationMatcher;
        private Button recordBtn;
        private TextView textBox;

        // Main entry point for our application
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create the speech object to use for speech recognition
            speech = new Speech(10);
            this.context = this;
            
            // Initialize the command matcher and application matcher to use to parse the user's input
            commandMatcher = new CommandStringMatcher(this.Assets.Open("dictionary.txt"));
            //applicationMatcher = new ApplicationStringMatcher(this.getApplications());



            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // get the resources from the layout
            recordBtn = FindViewById<Button>(Resource.Id.recordBtn);
            textBox = FindViewById<TextView>(Resource.Id.outputTxt);

            // Create a button click event for the recordBtn
            recordBtn.Click += delegate
            {
                
                // create the intent and start the activity
                var voiceIntent = speech.setUpIntent(new Intent());
                StartActivityForResult(voiceIntent, 10);

            };


        }

        // Some default method which is triggered when the result of the android speech returned
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


                        // Process the user's input and parsing the command the user said
                        string arguments = commandMatcher.process(textInput);

                        IAction action = Implementations.Action.createAction(context, commandMatcher.KeyWord);
                        action.setArguments(arguments);

                        action.run();

                        // Process the user's input and parsing the application the user said
                        //applicationMatcher.process(processed);                        

                        // Create an action to handle what the user is requesting the app to do
                        //Action action = new Action(commandMatcher, applicationMatcher, this);

                        // Perform the action
                        //action.run();

                    }
                    else
                        textBox.Text = "No speech was recognised";
                    // change the text back on the button
                    recordBtn.Text = "Start Recording";
                }
            }

            base.OnActivityResult(requestCode, resultVal, data);
        }

        /**
         * Launch the application on the pone with the given application package name
         */
        public void openApplication(string application)
        {

            // Set up the intent which will be used for launching the application
            Intent intent = PackageManager.GetLaunchIntentForPackage(application);

            // Launch the application
            StartActivity(intent);

        }

        /**
         * Get all of the application package names that are currently stored on the phone as and store them as a list
         * @return the list of application package names
         */
        public IList<string> getApplications()
        {

            // Create a list to store all of the applications
            IList<string> applications = new List<string>();

            // Run through each of the applications installed on the phone
            foreach (var item in PackageManager.GetInstalledApplications(new Android.Content.PM.PackageInfoFlags()))
            {

                // Get the context which has the name of the application package from each of the items
                var context = CreatePackageContext(item.PackageName, PackageContextFlags.IgnoreSecurity);

                // Add the name of the application package to the application list
                applications.Add(context.PackageName);

                // Print out the application package names to the console
                Console.WriteLine(context.PackageName);

            }

            // Return the list of application package names that are installed on the phone
            return applications;

        }

    }
    
}

