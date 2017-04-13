using Android.App;
using Android.Widget;
using Android.OS;
using Android.Speech;
using Android.Content;
using System;
using Interfaces;
using Implementations;

namespace DoWhat
{
	[Activity(Label = "DoWhat", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{

		// Instances variable
		private ISpeech speech;
		private Context context;
		private IStringMatcher commandMatcher;
		private Button recordBtn;
		private Button enterBtn;
		private EditText textBox;
		private string textCommand;

		// Main entry point for our application
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Create the speech object to use for speech recognition
			speech = new Speech(10);

			// Initialize the command matcher and application matcher to use to parse the user's input
			commandMatcher = new CommandStringMatcher(this.Assets.Open("dictionary.txt"));


			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// get the resources from the layout
			recordBtn = FindViewById<Button>(Resource.Id.recordBtn);
			textBox = FindViewById<EditText>(Resource.Id.outputTxt);
			enterBtn = FindViewById<Button>(Resource.Id.enterBtn);

			// Create a button click event for the recordBtn
			recordBtn.Click += delegate
			{

				// create the intent and start the activity
				var voiceIntent = speech.setUpIntent(new Intent());
				StartActivityForResult(voiceIntent, 10);

			};

			enterBtn.Click += delegate
			{
				textCommand = textBox.Text;
				string arguments = commandMatcher.process(textCommand);
				IAction action = Implementations.Action.createAction(context, commandMatcher.KeyWord);
				action.setArguments(arguments);

                action.run();
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

                    }
                    else
                    {
                        //textBox.Text = "No speech was recognised";
                        this.ErrorMessage("No Speech was recognised.");
                    }

                }

            }

            base.OnActivityResult(requestCode, resultVal, data);

        }
        //Toaster method to display error message (error message is determined by method in which it is called: Matthew Baning
			public void ErrorMessage(String text)
			{
				String errorText = text;
				Toast.MakeText(this, errorText, ToastLength.Long).Show();
			
			}

}

