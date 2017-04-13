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

        /**
         * Creates an ApplicationStringMatcher with a given list of application names
         * @param application the list of application names
         */
        public ApplicationStringMatcher(IList<string> applications)
        {

            this.dictionary = applications;

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