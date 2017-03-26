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
using Android.Content.PM;

namespace DoWhat
{

    public class Action
    {

        IStringMatcher commandMatcher;
        IStringMatcher applicationMatcher;
        MainActivity activity;

        public Action(IStringMatcher commandMatcher, IStringMatcher applicationMatcher, MainActivity activity)
        {

            this.commandMatcher = commandMatcher;
            this.applicationMatcher = applicationMatcher;
            this.activity = activity;

        }

        public void run()
        {

            if (this.commandMatcher.KeyWord.Equals("open"))
                activity.openApplication(applicationMatcher.KeyWord);

        }

    }

}