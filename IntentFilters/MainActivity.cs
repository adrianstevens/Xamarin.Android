using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Deep_Link
{
	[Activity (Label = "Deep_Link", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += delegate 
			{
				var myIntent = new Intent();
				myIntent.SetAction(Intent.ActionView);
				myIntent.AddCategory("com.xamarin.deeplink");

				myIntent.PutExtra("DeepValue", 255);

				StartActivityForResult(myIntent, 100);
			};
		}

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult (requestCode, resultCode, data);

			if (data != null) 
			{
				var value = data.GetIntExtra ("DeepResult", -17);

				Toast.MakeText (this, String.Format("the result is: {0}", value), ToastLength.Long); 
			}
		}
	}
}


