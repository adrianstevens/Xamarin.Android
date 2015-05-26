using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Util;
using Android.Graphics;

namespace ViewDrag
{
	[Activity (Label = "ViewDrag", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		ImageView imgIcon;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			imgIcon = FindViewById<ImageView> (Resource.Id.imgIcon);
		}

		float relativeX, relativeY;
		public override bool OnTouchEvent (MotionEvent e)
		{
			var x = e.GetX ();
			var y = e.GetY ();

			switch (e.Action) 
			{
			case MotionEventActions.Down:
				relativeX = x - imgIcon.Left;
				relativeY = y - imgIcon.Top;
				break;
			case MotionEventActions.Move:
				imgIcon.TranslationX = x - relativeX;
				imgIcon.TranslationY = y - relativeY;
				break;
			}
			return true;
		}
	}
}


