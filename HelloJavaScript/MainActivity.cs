using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Webkit;
using System.IO;

namespace HelloJavaScript
{
	[Activity (Label = "HelloJavaScript", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			WebView wv = FindViewById<WebView> (Resource.Id.webView);
			wv.Settings.JavaScriptEnabled = true;

			Stream input = Assets.Open ("hello.html");

			StreamReader sr = new StreamReader (input);

			var s = sr.ReadToEnd ();

			wv.LoadDataWithBaseURL ("file:///android_asset/", s , "text/html", "UTF-8", null);

		
		}
	}
}


