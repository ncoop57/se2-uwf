﻿using Android.App;
using Android.Widget;
using Android.OS;
using Android.Speech;
using Android.Content;
using System;
using System.Collections.Generic;
using Interfaces;
using Implementations;


namespace DoWhat
{
    // coded by Matthew, Nathan, and Julien
	[Activity(Label = "DoWhat", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{

		// Instances variable
		private ISpeech speech;
		private Context context;
		private IStringMatcher commandMatcher;
		private SuggestionManager suggestionManager;
		private Button recordBtn;
		private Button enterBtn;
		private EditText textBox;
		private TextView commandHeaderText;
		private ListView commandList;
		private string textCommand;

		// Main entry point for our application
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Create the speech object to use for speech recognition
			speech = new Speech(10);

			this.context = this;

			// Initialize the command matcher and application matcher to use to parse the user's input
			commandMatcher = new CommandStringMatcher(this.Assets.Open("dictionary.txt"));

			// Initialize suggestion manager with default commands
			suggestionManager = new SuggestionManager(this, commandMatcher.Dictionary);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// get the resources from the layout
			recordBtn = FindViewById<Button>(Resource.Id.recordBtn);
			textBox = FindViewById<EditText>(Resource.Id.outputTxt);
			enterBtn = FindViewById<Button>(Resource.Id.enterBtn);
			commandHeaderText = FindViewById<TextView>(Resource.Id.cmdPrompt);
			commandList = FindViewById<ListView>(Resource.Id.cmdList);

			// Create a button click event for the recordBtn : Nathan
			recordBtn.Click += delegate
			{

				// create the intent and start the activity
				var voiceIntent = speech.setUpIntent(new Intent());
				StartActivityForResult(voiceIntent, 10);

			};

            // coded by Julien
			enterBtn.Click += delegate
			{
				textCommand = textBox.Text;
				string arguments = commandMatcher.process(textCommand);

				if (!commandMatcher.KeyWord.Equals(""))
				{

					IAction action = Implementations.Action.createAction(context, commandMatcher.KeyWord);
					action.setArguments(arguments);
					action.run();

				}
				else
				{
					this.ErrorMessage("No commands recognised.");
					var commands = suggestionManager.pullSpecificCommands(textCommand);
					this.CommandList(commands);

				}

				commandMatcher.KeyWord = "";

			};

		}

		// Some default method which is triggered when the result of the android speech returned: Nathan
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

						suggestionManager.pullSpecificCommands(textInput);
						if (!commandMatcher.KeyWord.Equals(""))
						{

							IAction action = Implementations.Action.createAction(context, commandMatcher.KeyWord);
							action.setArguments(arguments);
							action.run();

						}
						else
						{

							this.ErrorMessage("No commands recognised.");
							var commands = suggestionManager.pullSpecificCommands(textInput);
							this.CommandList(commands);

						}
						// Add suggested to list if command does not equal "open"
						if (commandMatcher.KeyWord.Equals("search"))
						{

							suggestionManager.storeSuggestion("search " + arguments);

						}

						commandMatcher.KeyWord = "";

					}
					else
					{

						textBox.Text = "No speech was recognised";
						this.ErrorMessage("No Speech was recognised.");
						this.CommandList(suggestionManager.SuggestedWords);

					}

				}

				base.OnActivityResult(requestCode, resultVal, data);
			}
		}
		//Toaster method to display error message (error message is determined by method in which it is called: Matthew Baning
		public void ErrorMessage(String text)
		{
			String errorText = text;
			Toast.MakeText(this, errorText, ToastLength.Long).Show();

		}

        // coded by Julien
		public void CommandList(List<String> commands)
		{
			// Code for command list viewing
			ArrayAdapter adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, commands);
			commands.Sort();
			commandList.Adapter = adapter;
			commandHeaderText.Enabled = true;
			commandList.Enabled = true;
			commandHeaderText.Visibility = Android.Views.ViewStates.Visible;
			commandList.Visibility = Android.Views.ViewStates.Visible;
		}
	}
}
