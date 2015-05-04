using System;
using Android.Views;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Util;

namespace ScaleGesture
{
	public class GestureView : View, Android.Views.ScaleGestureDetector.IOnScaleGestureListener
	{
		private Drawable icon;
		private ScaleGestureDetector scaleDetector;

		private float scaleFactor = 1.0f;

		public GestureView (Context context) : base(context, null, 0)
		{
			Init (context);
		}

		public GestureView (Context context, IAttributeSet attrs) : base(context, attrs)
		{
			Init (context);
		}

		public GestureView (Context context, IAttributeSet attrs, int defStyle) : base (context, attrs, defStyle)
		{
			Init (context);
		}

		void Init (Context context)
		{
			icon = context.Resources.GetDrawable (Resource.Drawable.xamlogo);
			icon.SetBounds (0, 0, icon.IntrinsicWidth, icon.IntrinsicHeight);

			scaleDetector = new ScaleGestureDetector (context, this);
		}

		public override bool OnTouchEvent (MotionEvent e)
		{
			scaleDetector.OnTouchEvent (e);

			return true;
		}

		protected override void OnDraw (Canvas canvas)
		{
			base.OnDraw (canvas);
			canvas.Save ();
			canvas.Scale (scaleFactor, scaleFactor);
			icon.Draw (canvas);
			canvas.Restore ();
		}

		bool ScaleGestureDetector.IOnScaleGestureListener.OnScale (ScaleGestureDetector detector)
		{
			this.scaleFactor *= detector.ScaleFactor;

			Invalidate ();

			return true;

		}

		bool ScaleGestureDetector.IOnScaleGestureListener.OnScaleBegin (ScaleGestureDetector detector)
		{
			return true; //so the detector continues to recognize the gesture
		}

		void ScaleGestureDetector.IOnScaleGestureListener.OnScaleEnd (ScaleGestureDetector detector)
		{
		}
	}
}

