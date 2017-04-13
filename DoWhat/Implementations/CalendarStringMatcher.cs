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

namespace Implementations
{
    class CalendarStringMatcher : IStringMatcher
    {
        IList<string> dictionary;
        string keyword;

        public CalendarStringMatcher(IList<string> dictionary)
        {
            this.dictionary = dictionary;
        }
        public IList<string> Dictionary
        {
            get
            {
                return dictionary;
            }
            set
            {
                this.dictionary = value;
            }
        }

        public string KeyWord
        {
            get
            {
                return this.keyword;
            }
            set
            {
                this.keyword = value;
            }
        }

        public string process(string str)
        {
            string summaryString = "";

            string[] words = str.ToLower().Split(' ');
            for(int i=0; i<words.Length; i++)
            {
                foreach(string word in dictionary)
                {
                    if (words[i].Equals(word))
                    {
                        KeyWord = summaryString;
                        return str.Replace(summaryString, "");
                    }
                    summaryString = summaryString + " " + words[i];
                }
            }
            return str;
        }
    }
}