using System;
using Android.Views;
using Android.Graphics;
using Android.Content;
using Android.Util;

namespace XamPaint
{
	public class PaintView : View
	{
		private Path drawPath;
		private Paint drawPaint, canvasPaint;
		private Canvas drawCanvas;

		private Bitmap canvasBitmap;


		public PaintView (Context context) : base(context, null, 0)
		{
			Init ();
		}

		public PaintView (Context context, IAttributeSet attrs) : base(context, attrs)
		{
			Init ();
		}

		public PaintView (Context context, IAttributeSet attrs, int defStyle) : base (context, attrs, defStyle)
		{
			Init ();
		}

		void Init ()
		{
			drawPath = new Path();

			drawPaint = new Paint () 
			{
				Color = Color.Aqua,
				AntiAlias = true,
				StrokeWidth = 5f,
	
				StrokeJoin = Paint.Join.Round,
				StrokeCap = Paint.Cap.Round,
			};

			drawPaint.SetStyle (Paint.Style.Stroke);

			canvasPaint = new Paint (PaintFlags.Dither);
		}

		protected override void OnSizeChanged (int w, int h, int oldw, int oldh)
		{
			base.OnSizeChanged (w, h, oldw, oldh);

			canvasBitmap = Bitmap.CreateBitmap (w, h, Bitmap.Config.Argb8888);
			drawCanvas = new Canvas(canvasBitmap);
		}

		protected override void OnDraw (Canvas canvas)
		{
		//	base.OnDraw (canvas);

			canvas.DrawBitmap(canvasBitmap, 0, 0, canvasPaint);
			canvas.DrawPath(drawPath, drawPaint);
		} 

		public override bool OnTouchEvent (MotionEvent e)
		{
			var x = e.GetX();
			var y = e.GetY();

			switch (e.Action) 
			{
			case MotionEventActions.Down:
				drawPath.MoveTo(x, y);
				break;
			case MotionEventActions.Move:
				drawPath.LineTo(x, y);
				break;
			case MotionEventActions.Up:
				drawCanvas.DrawPath(drawPath, drawPaint);
				drawPath.Reset();
				break;
			default:
				return false;
			}
			Invalidate ();
			return true;
		}

		public void Clear ()
		{
			drawCanvas.DrawColor(Color.Black, PorterDuff.Mode.Clear);
			Invalidate();
		}
	}
}

