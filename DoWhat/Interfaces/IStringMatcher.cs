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

        string KeyWord { get; }

        IList<string> Dictionary { get; }

        void loadDictionary(StreamReader dictionary);

        string process(string str);

    }
}