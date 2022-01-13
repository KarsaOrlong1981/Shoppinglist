using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoppinglist.Model
{
    public static class WriteToast
    {
        static Context mContext = Android.App.Application.Context;

        public static void ShowLongToast(string txt)
        {
            Toast.MakeText(mContext, txt, ToastLength.Long).Show();
        }
        public static void ShowShortToast(string txt)
        {
            Toast.MakeText(mContext, txt, ToastLength.Short).Show();
        }
    }

}

