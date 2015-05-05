using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using RotationGesture;

namespace MultiGesture
{
	[Activity (Label = "MultiGesture", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity, GestureDetector.IOnGestureListener, 
		ScaleGestureDetector.IOnScaleGestureListener, IOnRotationGestureListener
	{
		GestureDetector scrollDetector;
		ScaleGestureDetector scaleDetector;
		RotationGestureDetector rotationDetector;

		private ImageView xamLogo;

		private float deltaX, deltaY;
		private float scale = 1.0f;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			xamLogo = FindViewById<ImageView> (Resource.Id.xamLogo);

			scaleDetector = new ScaleGestureDetector (this, this);
			rotationDetector = new RotationGestureDetector (this);
			scrollDetector = new GestureDetector (this, this);
		}

		public override bool OnTouchEvent (MotionEvent e)
		{
			scrollDetector.OnTouchEvent (e);
			scaleDetector.OnTouchEvent (e);
			rotationDetector.OnTouchEvent (e);

			return true;
		}

		public void OnRotate (float angle)
		{
			xamLogo.Rotation = angle;
		}

		public bool OnScroll (MotionEvent e1, MotionEvent e2, float distanceX, float distanceY)
		{
			deltaX -= distanceX;
			deltaY -= distanceY;

			xamLogo.TranslationX = deltaX;
			xamLogo.TranslationY = deltaY;

			return true;
		}

		public bool OnDown (MotionEvent e)
		{
			return false;
		}

		public bool OnFling (MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
		{
			return false;
		}

		public void OnLongPress (MotionEvent e)
		{
		}

		public void OnShowPress (MotionEvent e)
		{
		}

		public bool OnSingleTapUp (MotionEvent e)
		{
			return false;
		}

		public bool OnScale (ScaleGestureDetector detector)
		{
			this.scale *= detector.ScaleFactor;

			xamLogo.ScaleX = scale;
			xamLogo.ScaleY = scale;

			return true;
		}

		public bool OnScaleBegin (ScaleGestureDetector detector)
		{
			return true;
		}

		public void OnScaleEnd (ScaleGestureDetector detector)
		{
		}
	}
}


