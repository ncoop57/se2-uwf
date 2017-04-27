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
    //coded agl11 and nac33
    public interface ICalendarAction
    {
        void createCalendarEvent(string summary, string location, DateTime start, DateTime end, string emailAddress, string timeZone);
    }
}