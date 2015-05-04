using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Util;

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

		bool bDrag = false;
		float relativeX, relativeY;
		public override bool OnTouchEvent (MotionEvent e)
		{
			var x = e.RawX;
			var y = e.RawY;

			switch (e.Action) 
			{
			case MotionEventActions.Down:
				//are we within the bounds of imgIcon?
		//		if (imgIcon.Left < x && imgIcon.Right > x &&
		//			imgIcon.Top > y && imgIcon.Bottom < y) 
				{
					bDrag = true;
					relativeX = x - imgIcon.Left;
					relativeY = y - imgIcon.Top;
					Log.Debug ("Up", "Within image");
				}
				break;
			case MotionEventActions.Move:
				if (bDrag) {
					RelativeLayout.LayoutParams layoutParams = (RelativeLayout.LayoutParams) imgIcon.LayoutParameters;

					layoutParams.LeftMargin = (int)(x - relativeX);
					layoutParams.TopMargin = (int)(y - relativeY);
					layoutParams.RightMargin = imgIcon.Left + imgIcon.Width;
					layoutParams.BottomMargin = imgIcon.Top + imgIcon.Height;

					imgIcon.LayoutParameters = layoutParams;
				}
				break;
			case MotionEventActions.Up:
				bDrag = false;
				break;				
			}
			return true;
		}
	}
}


