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
    // coded by Nathan
    public class Action
    {

        public static IAction createAction(Context context, string command)
        {
            AssetReader reader = new AssetReader(context);

            IAction newAction = null;

            if (command.Equals("open"))
                newAction = new OpenAction(context, new ApplicationStringMatcher(reader.getApplications()));
            else if (command.Equals("search"))
                newAction = new SearchAction(context);
            else if (command.Equals("create")) 
                newAction = new CreateCalendarAction(context);
            return newAction;

        }

    }
}