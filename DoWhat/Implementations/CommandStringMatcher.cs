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

        private string keyWord;

        public string KeyWord
        {

            get
            {
                return this.keyWord;
            }

        }

        private IList<string> dictionary;

        public IList<string> Dictionary
        {

            get
            {

                return this.dictionary;

            }

        }

        public CommandStringMatcher(Stream dictionary)
        {

            this.dictionary = new List<string>();
            this.loadDictionary(new StreamReader(dictionary));

        }

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

        public string process(string str)
        {

            str = str.ToLower();

            foreach (string word in this.dictionary)
            {

                if (str.Contains(word))
                {

                    this.keyWord = word;
                    break;

                }

            }

            return string.Join(" ", str.Split(' ').Except(this.dictionary));

        }

    }

}