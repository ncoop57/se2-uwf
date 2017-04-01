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
    public class OpenAction : IAction 
    {

        /**
         *  The IStringMatcher which will handle matching the name of the
         *  application package that the user wants to use
         */
        private IStringMatcher applicationMatcher;

        private Context context;

        // List of application package names that are installed on the phone
        private IList<string> applicationPackages;
        
        public OpenAction(Context context, IStringMatcher applicationMatcher)
        {

            this.applicationMatcher = applicationMatcher;
            this.context = context;
            this.applicationPackages = this.getApplications();

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
         * Sets the needed arguments for the action to be performed
         * @param args The arguments needed to run an action
         */
        public void run()
        {

            this.openApplication(applicationMatcher.KeyWord);

        }

        /**
         * Launch the application on the pone with the given application package name
         */
        public void openApplication(string application)
        {

            // Set up the intent which will be used for launching the application
            Intent intent = this.context.PackageManager.GetLaunchIntentForPackage(application);

            // Launch the application
            context.StartActivity(intent);

        }

        /**
         * Get all of the application package names that are currently stored on the phone as and store them as a list
         * @return the list of application package names
         */
        public IList<string> getApplications()
        {

            // Create a list to store all of the applications
            IList<string> applications = new List<string>();

            // Run through each of the applications installed on the phone
            foreach (var item in this.context.PackageManager.GetInstalledApplications(new Android.Content.PM.PackageInfoFlags()))
            {

                // Get the context which has the name of the application package from each of the items
                var context = this.context.CreatePackageContext(item.PackageName, PackageContextFlags.IgnoreSecurity);

                // Add the name of the application package to the application list
                applications.Add(context.PackageName);

                // Print out the application package names to the console
                Console.WriteLine(context.PackageName);

            }

            // Return the list of application package names that are installed on the phone
            return applications;

        }

    }

}