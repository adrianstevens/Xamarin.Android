using System;
using Android.Views;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Util;
using RotationGesture;

namespace RotationGesture
{
	public class GestureView : View, IOnRotationGestureListener
	{
		private Drawable icon;
		private RotationGestureDetector rotationDetector;

		private float rotation;

		private float xPos, yPos; //to center

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

			rotationDetector = new RotationGestureDetector (this);

		}

		public override void Layout (int l, int t, int r, int b)
		{
			xPos = this.Width / 2 - icon.IntrinsicWidth / 2;
			yPos = this.Height / 2 - icon.IntrinsicHeight / 2;

			base.Layout (l, t, r, b);
		}

		public override bool OnTouchEvent (MotionEvent e)
		{
			rotationDetector.OnTouchEvent (e);

			return true;
		}

		protected override void OnDraw (Canvas canvas)
		{
			base.OnDraw (canvas);
			canvas.Save ();
		
			canvas.Translate (xPos, yPos);
			canvas.Rotate (rotation, icon.Bounds.CenterX(), icon.Bounds.CenterY());
			icon.Draw (canvas);
			canvas.Restore ();
		}

		#region IOnRotationGestureListener implementation
		void IOnRotationGestureListener.OnRotate (float angle)
		{
			rotation = angle;
			Invalidate ();
		}
		#endregion
	}
}

