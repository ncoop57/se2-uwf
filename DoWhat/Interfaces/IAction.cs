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

namespace Interfaces
{
    public interface IAction
    {

        /**
         * Sets the needed arguments for the action to be performed
         * @param args The arguments needed to run an action
         */
        void setArguments(string args);

        // Executes the action
        void run();

    }
}