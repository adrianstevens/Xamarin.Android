using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Util;

namespace Gestures
{
	[Activity (Label = "Gestures", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		int primaryId, secondaryId;
		bool bTwoFinger = false;

		TextView textLocation;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			textLocation = FindViewById<TextView> (Resource.Id.textOuput);
			var imgLogo = FindViewById<ImageView> (Resource.Id.imageLogo);
		}

		public override bool OnTouchEvent (MotionEvent e)
		{
			switch (e.Action) 
			{
			case MotionEventActions.Down:
				primaryId = e.GetPointerId (e.ActionIndex);
				Log.Debug ("Down - pointer index", primaryId.ToString());//
				break;
			case MotionEventActions.PointerDown: 
				bTwoFinger = true;
				
				//this means two fingers are on the screen
				secondaryId = e.GetPointerId (e.ActionIndex);
				Log.Debug ("PointerDown - non primary pointer index", secondaryId.ToString());
				break;
			case MotionEventActions.Move:
				float x, x2, y, y2;

				if (bTwoFinger) {
					x = e.GetX (primaryId);
					y = e.GetY (primaryId);
					x2 = e.GetX (secondaryId);
					y2 = e.GetY (secondaryId);
				} else {
					x = e.GetX ();
					y = e.GetY ();
					x2 = y2 = 0;
				}

				var msg = (bTwoFinger) ? 
					String.Format ("({0:0}, {1:0}) ({2:0}, {3:0})", x, y, x2, y2) :
					String.Format ("({0:0}, {1:0})", x, y); 
				textLocation.Text = msg;
				Log.Debug ("Location: ", msg);

				break;
			case MotionEventActions.Up:
				bTwoFinger = false;
				break;
			} 

			return base.OnTouchEvent (e);
		}

		double CalcAngle (float x1, float y1, float x2, float y2)
		{
			//needs a little polishing
			var rise = y1 - y2;
			var run = x2 - x1;
			var angle = Math.Atan (rise / run) * 180 / Math.PI;

			Log.Debug ("Angle: ", 
				String.Format("{0} {1} {2}", rise, run, angle));

			return angle;
		}
	}
}


