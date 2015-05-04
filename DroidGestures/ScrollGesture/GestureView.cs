using System;
using Android.Views;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Util;

namespace ScrollGesture
{
	public class GestureView : View, Android.Views.GestureDetector.IOnGestureListener
	{
		private Drawable icon;
		private GestureDetector detector;

		float deltaX, deltaY;

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

			detector = new GestureDetector (context, this);
		}

		public override bool OnTouchEvent (MotionEvent e)
		{
			detector.OnTouchEvent (e);

			return true;
		}

		protected override void OnDraw (Canvas canvas)
		{
			base.OnDraw (canvas);
			canvas.Save ();
	
			canvas.Translate (deltaX, deltaY);
			icon.Draw (canvas);
			canvas.Restore ();
		}

		bool GestureDetector.IOnGestureListener.OnDown (MotionEvent e)
		{
			return true;
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

			Invalidate ();
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

