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
    public class SearchAction : IAction
    {

        private string searchTerm;

        private Context context;

        public SearchAction(Context context)
        {

            this.context = context;

        }

        public void setArguments(string args)
        {

            this.searchTerm = args;

        }

        public void run()
        {

            this.search();

        }

        private void search()
        {

            var uri = Android.Net.Uri.Parse("http://www.google.com/search?q=" + this.searchTerm);
            Intent intent = new Intent(Intent.ActionView, uri);

            context.StartActivity(intent);

        }
        
    }
}