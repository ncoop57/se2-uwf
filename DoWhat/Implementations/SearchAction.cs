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
    public class SearchAction : IAction
    {

        private IStringMatcher searchMatcher;

        private Context context;

        public SearchAction(Context context)
        {

            this.context = context;

        }

        public void setArguments(string args)
        {

            this.searchMatcher.process(args);

        }

        public void run()
        {
            throw new NotImplementedException();
        }
        
    }
}