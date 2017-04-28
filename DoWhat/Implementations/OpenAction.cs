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
    // coded by Julien and Nathan
    public class OpenAction : IAction 
    {

        /**
         *  The IStringMatcher which will handle matching the name of the
         *  application package that the user wants to use
         */
        private IStringMatcher applicationMatcher;

        // The context needed to be able to do functions on the phone
        private Context context;
        
        public OpenAction(Context context, IStringMatcher applicationMatcher)
        {

            this.applicationMatcher = applicationMatcher;
            this.context = context;

        }

        /**
         * Sets the needed arguments for the action to be performed
         * @param args The arguments needed to run an action
         */
        public void setArguments(string args)
        {

            this.applicationMatcher.process(args);

        }

        /**
         * Runs the action needed to be performed
         */
        public void run()
        {

            this.openApplication(applicationMatcher.KeyWord);

        }

        /**
         * Launch the application on the pone with the given application package name
         * @param application the package name for the application to be passed
         */
        public void openApplication(string application)
        {

            // Set up the intent which will be used for launching the application
            Intent intent = this.context.PackageManager.GetLaunchIntentForPackage(application);

            // Launch the application
            context.StartActivity(intent);

        }

    }

}