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

namespace Implementations
{
    // coded by Matthew
    public class AssetReader
    {

        private Context context;

        public AssetReader(Context context)
        {

            this.context = context;

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