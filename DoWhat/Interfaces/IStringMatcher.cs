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
    // coded by Nathan
    public interface IStringMatcher
    {

        // The main substring you are trying to extract from a user's input
        string KeyWord { get; set; }

        // The list of strings you will be trying to match against the user's input
        IList<string> Dictionary { get; }

        // Goes through a given string and processes it in some fasion and returns the processed string
        string process(string str);

    }
}