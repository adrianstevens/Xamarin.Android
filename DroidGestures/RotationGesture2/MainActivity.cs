using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics.Drawables;
using RotationGesture;

namespace RotationGesture2
{
	[Activity (Label = "RotationGesture2", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity, IOnRotationGestureListener
	{
		private RotationGestureDetector rotationDetector;
		private ImageView xamLogo;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			xamLogo = FindViewById<ImageView> (Resource.Id.xamLogo);

			rotationDetector = new RotationGestureDetector (this);
		}

		public override bool OnTouchEvent (MotionEvent e)
		{
			rotationDetector.OnTouchEvent (e);

			return true;
		}

		void IOnRotationGestureListener.OnRotate (float angle)
		{
			xamLogo.Rotation = angle;
		}
	}
}


