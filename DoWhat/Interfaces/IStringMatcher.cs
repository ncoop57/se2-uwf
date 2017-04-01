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
using System.IO;

namespace Interfaces
{
    public interface IStringMatcher
    {

        // The main substring you are trying to extract from a user's input
        string KeyWord { get; }

        // The list of strings you will be trying to match against the user's input
        IList<string> Dictionary { get; }

        // Reads in the dictionary from a file into the dictionary you will be using
        void loadDictionary(StreamReader dictionary);

        // Goes through a given string and processes it in some fasion and returns the processed string
        string process(string str);

    }
}