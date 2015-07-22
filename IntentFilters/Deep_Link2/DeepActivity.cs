
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

namespace Deep_Link2
{
	[Activity (Label = "DeepActivity")]		
	[IntentFilter (new[]{Intent.ActionView}, Categories=new[]{"com.xamarin.deeplink", Intent.CategoryDefault})]
	public class DeepActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Deep);

			Button button = FindViewById<Button> (Resource.Id.myButton);

			button.Click += (object sender, EventArgs e) => 
			{
				var value = this.Intent.GetIntExtra("DeepValue", -1);

				button.Text = String.Format("Deep Value is: {0}", value);

				var rIntent = new Intent();
				rIntent.PutExtra("DeepResult", 42);

				SetResult(Result.Ok, rIntent);

				Finish();
			};
		}
	}
}

/*
 * <activity android:name="com.xamarin.deep_link2.DeepActivity" android:label="Deep2 Title">
		<intent-filter android:label="deeplink">
			<action android:name="android.intent.action.VIEW" />
			<category android:name="android.intent.category.DEFAULT" />
			<category android:name="com.xamarin.deeplink" />
		</intent-filter>
	</activity>


*/