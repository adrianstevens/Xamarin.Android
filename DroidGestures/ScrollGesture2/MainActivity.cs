using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace ScrollGesture2
{
	[Activity (Label = "ScrollGesture2", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity, Android.Views.GestureDetector.IOnGestureListener
	{
		private GestureDetector gestureDetector;
		private ImageView xamLogo;
		private float deltaX, deltaY;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			xamLogo = FindViewById<ImageView> (Resource.Id.xamLogo);

			gestureDetector = new GestureDetector (this, this);
		}

		public override bool OnTouchEvent (MotionEvent e)
		{
			gestureDetector.OnTouchEvent (e);

			return true;
		}

		bool GestureDetector.IOnGestureListener.OnDown (MotionEvent e)
		{
			return false;
		}

		bool GestureDetector.IOnGestureListener.OnFling (MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
		{
			return false;
		}

		void GestureDetector.IOnGestureListener.OnLongPress (MotionEvent e)
		{
		}

		bool GestureDetector.IOnGestureListener.OnScroll (MotionEvent e1, MotionEvent e2, float distanceX, float distanceY)
		{
			deltaX -= distanceX;
			deltaY -= distanceY;

			xamLogo.TranslationX = deltaX;
			xamLogo.TranslationY = deltaY;

			return true;
		}

		void GestureDetector.IOnGestureListener.OnShowPress (MotionEvent e)
		{

		}

		bool GestureDetector.IOnGestureListener.OnSingleTapUp (MotionEvent e)
		{
			return false;
		}
	}
}


