using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Interfaces;
using System.IO;

namespace Implementations
{
	public class SuggestionManager
	{
		private Context context;

		// List of suggested commands
		private readonly List<string> suggestedWords;

		public List<string> SuggestedWords
		{
			get
			{
				return this.suggestedWords;
			}
		}

		public SuggestionManager(Context context, IList<string> defaults)
		{
			this.context = context;
			this.suggestedWords = new List<string>();
			loadDefaultCommands(defaults);
		}
		/**
		* Store a valid command.
		* @param suggested command
		*/
		public void storeSuggestion(String suggestion)
		{
			if (suggestedWords.Contains(suggestion))
			{
				Console.WriteLine(suggestion + " already exists in the suggested words.");
			}
			else
			{
				this.suggestedWords.Add(suggestion);
			}
			this.suggestedWords.Sort();
		}

		/*
		 * Retrieves a list of specific commands 
		 * based around the initial command.
		 * @param initialCmd what the user says
		 * @return list of similar valid commands
		 */
		public List<string> pullSpecificCommands(string initialCmd)
		{
			List<string> specificCommands = new List<string>();
			string validCommand = "";
			if (initialCmd.StartsWith("o", StringComparison.InvariantCultureIgnoreCase))
			{
				validCommand = "open";
			}
			else if (initialCmd.StartsWith("s", StringComparison.InvariantCultureIgnoreCase))
			{
				validCommand = "search";
			}

			if (validCommand.Equals(""))
			{
				suggestedWords.Sort();
				return suggestedWords;
			}

			foreach (var command in suggestedWords)
			{
				if (validCommand.Equals("open") && command.Contains("open"))
				{
					specificCommands.Add(command);
				}
				if (validCommand.Equals("search") && command.Contains("search"))
				{
					specificCommands.Add(command);
				}
			}
			specificCommands.Sort();
			return specificCommands;
		}

		/**
		* Store default commands.
		*/
		public void loadDefaultCommands(IList<string> defaults)
		{
			foreach (string word in defaults)
			{
				if (word.Equals("open"))
				{
					loadOpenCommands();
				}
				else
				{
					storeSuggestion(word);
				}
			}
		}

		/**
		* Store open related commands
		*/
		private void loadOpenCommands()
		{
			foreach (var item in this.context.PackageManager.GetInstalledApplications(new Android.Content.PM.PackageInfoFlags()))
            {
                // Get the context which has the name of the application package from each of the items
                var context = this.context.CreatePackageContext(item.PackageName, PackageContextFlags.IgnoreSecurity);

				// Add the name of the application package to the application list
				suggestedWords.Add("open " + context.PackageName);

            }
		}
	}
}
