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

        public IList<string> StopWords
        {

            get;

        }

        public ApplicationStringMatcher(Stream dictionary)
        {

            this.loadDictionary(new StreamReader(dictionary));

        }

        public ApplicationStringMatcher(IList<string> applications)
        {

            this.loadDictionary(applications);

        }

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

        public string process(string str)
        {

            str = str.ToLower();

            foreach (string word in this.dictionary)
            {

                if (word.ToLower().Contains(str))
                {

                    this.keyWord = word;

                }

            }

            return str;

        }

    }

}