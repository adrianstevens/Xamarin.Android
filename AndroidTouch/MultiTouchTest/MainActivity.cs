using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace MultiTouchTest
{
	[Activity (Label = "MultiTouchTest", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += delegate {
				button.Text = string.Format ("{0} clicks!", count++);
			};
		}

		public override bool OnTouchEvent(MotionEvent e)
		{
			string s = "";

			s += e.ActionMasked.ToString() + ": ";
			s += "PointerCount(" + e.PointerCount + ") ";
			s += "ActionIndex("  + e.ActionIndex + ") ";

			for (int index = 0; index < e.PointerCount; index++)
			{
				int   id = e.GetPointerId(index);
				float x  = e.GetX(index);
				float y  = e.GetY(index);

				s += "Pointer(" + id + ": " + x + "," + y + ") ";
			}

			Console.WriteLine(s);

			return true;		
		}
	}
}


