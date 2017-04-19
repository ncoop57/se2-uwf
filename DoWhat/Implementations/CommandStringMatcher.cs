using System;
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

    public class CommandStringMatcher : IStringMatcher
    {

        // The main command you are trying to extract from a user's input
        private string keyWord;

        public string KeyWord
        {

            get
            {
                return this.keyWord;
            }

        }

        // The list of commands you will be trying to match against the user's input
        private IList<string> dictionary;

        public IList<string> Dictionary
        {

            get
            {

                return this.dictionary;

            }

        }

        /**
         * Creates an CommandStringMatcher with a given list of commands
         * @param dictionary the file stream where the file for the list of commands is located
         */
        public CommandStringMatcher(Stream dictionary)
        {

            this.dictionary = new List<string>();
            this.loadDictionary(new StreamReader(dictionary));
        }

        /**
         * Reads in the commands from a stream and stores them
         * @param dictionary the file stream where the file for the list of commands is located 
         */
        public void loadDictionary(StreamReader dictionary)
        {

            string line;
            // Read the file and display it line by line coded nac33, mod jrp58
            while (((line = dictionary.ReadLine()) != null) && (dictionary.Peek() > -1))
            {
                this.dictionary.Add(line);
            }
            dictionary.Close();

        }

        /**
         *  Goes through a given string and finds the command the user said and stores it
         *  @param str the user's input
         *  @return the string without the command words in it
         */
        public string process(string str)
        {

            str = str.ToLower();

            // Search through the commands word by word
            foreach (string word in this.dictionary)
            {

                //Check if the user's input contains the current word
                if (str.Contains(word))
                {

                    // Store the word the user said
                    this.keyWord = word;
                    break;

                }

            }

            // Form a new string without the command words and return it
            return string.Join(" ", str.Split(' ').Except(this.dictionary)); 

        }
	}

}