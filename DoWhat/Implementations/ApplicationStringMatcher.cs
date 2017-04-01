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

    public class ApplicationStringMatcher : IStringMatcher
    {

        // The main application name you are trying to extract from a user's input
        private string keyWord;

        public string KeyWord
        {

            get
            {
                return this.keyWord;
            }

        }

        // The list of application names you will be trying to match against the user's input
        private IList<string> dictionary;

        public IList<string> Dictionary
        {

            get
            {

                return this.dictionary;

            }

        }

        public IList<string> StopWords
        {

            get;

        }

        /**
         * Creates an ApplicationStringMatcher with a given list of application names
         * @param dictionary the file stream where the file for the list of application names is located
         */
        public ApplicationStringMatcher(Stream dictionary)
        {

            this.loadDictionary(new StreamReader(dictionary));

        }

        /**
         * Creates an ApplicationStringMatcher with a given list of application names
         * @param application the list of application names
         */
        public ApplicationStringMatcher(IList<string> applications)
        {

            this.loadDictionary(applications);

        }

        /**
         * Reads in the application names from a stream and stores them
         * @param dictionary the file stream where the file for the list of application names is located 
         */
        public void loadDictionary(StreamReader dictionary)
        {

            string line;
            // Read the file and display it line by line coded nac33, mod jrp58
            while (((line = dictionary.ReadLine()) != null) && (dictionary.Peek() > -1))
            {
                Dictionary.Add(line);
            }
            dictionary.Close();

        }

        /**
         * Stores a given list of applications
         * @param application the list of application names 
         */
        public void loadDictionary(IList<string> applications)
        {

            this.dictionary = applications;

        }

        public void loadStopWords(string stopFilePath)
        {

            string line;
            // Read the file and display it line by line coded nac33, mod jrp58
            StreamReader sr = new StreamReader(stopFilePath);
            while (((line = sr.ReadLine()) != null) && (sr.Peek() > -1))
            {
                this.StopWords.Add(line);
            }
            sr.Close();

        }

        /**
         *  Goes through a given string and finds the application name the user said and stores it
         *  @param str the user's input which contains just the name of the application the user said
         *  @return str
         */
        public string process(string str)
        {

            str = str.ToLower();

            // Search through the application names word by word
            foreach (string word in this.dictionary)
            {

                //Check if the user's input contains the current word
                if (word.ToLower().Contains(str))
                {

                    // Store the word the user said
                    this.keyWord = word;

                }

            }

            // Return the string that was given
            return str;

        }

    }

}