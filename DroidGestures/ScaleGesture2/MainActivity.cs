using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace ScaleGesture2
{
	[Activity (Label = "ScaleGesture2", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity, Android.Views.ScaleGestureDetector.IOnScaleGestureListener
	{
		private ScaleGestureDetector scaleDetector;
		private ImageView xamLogo;
		private float scaleFactor = 1.0f;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			xamLogo = FindViewById<ImageView> (Resource.Id.xamLogo);

			scaleDetector = new ScaleGestureDetector (this, this);
		}

		public override bool OnTouchEvent (MotionEvent e)
		{
			scaleDetector.OnTouchEvent (e);

			return true;
		}

		bool ScaleGestureDetector.IOnScaleGestureListener.OnScale (ScaleGestureDetector detector)
		{
			this.scaleFactor *= detector.ScaleFactor;

			xamLogo.ScaleX = scaleFactor;
			xamLogo.ScaleY = scaleFactor;

			return true;
		}

		bool ScaleGestureDetector.IOnScaleGestureListener.OnScaleBegin (ScaleGestureDetector detector)
		{
			return true;
		}

		void ScaleGestureDetector.IOnScaleGestureListener.OnScaleEnd (ScaleGestureDetector detector)
		{
		}
	}
}


