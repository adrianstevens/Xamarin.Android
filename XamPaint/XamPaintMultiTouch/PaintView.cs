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
		private Path drawPath2;
		private Paint drawPaint, drawPaint2, canvasPaint;
		private Canvas drawCanvas;

		int primaryId, secondaryId;
		bool bTwoFinger = false;

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
			drawPath2 = new Path ();

			drawPaint = new Paint () 
			{
				Color = Color.Aqua,
				AntiAlias = true,
				StrokeWidth = 5f,
	
				StrokeJoin = Paint.Join.Round,
				StrokeCap = Paint.Cap.Round,
			};
			drawPaint.SetStyle (Paint.Style.Stroke);

			drawPaint2 = new Paint () 
			{
				Color = Color.HotPink,
				AntiAlias = true,
				StrokeWidth = 5f,

				StrokeJoin = Paint.Join.Round,
				StrokeCap = Paint.Cap.Round,
			};
			drawPaint2.SetStyle (Paint.Style.Stroke);

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
			canvas.DrawBitmap(canvasBitmap, 0, 0, canvasPaint);
			canvas.DrawPath(drawPath, drawPaint);
			canvas.DrawPath(drawPath2, drawPaint2);
		} 

		public override bool OnTouchEvent (MotionEvent e)
		{
			float x1, y1, x2, y2;

			x1 = e.GetX();
			y1 = e.GetY();

			switch (e.Action & MotionEventActions.Mask) 
			{
			case MotionEventActions.Down:
				primaryId = e.GetPointerId (e.ActionIndex);
				drawPath.MoveTo(x1, y1);
				break;
			case MotionEventActions.Move:
				if (bTwoFinger) 
				{
					x1 = e.GetX (primaryId);
					y1 = e.GetY (primaryId);
					x2 = e.GetX (secondaryId);
					y2 = e.GetY (secondaryId);

					drawPath.LineTo (x1, y1);
					drawPath2.LineTo (x2, y2);
				} 
				else 
				{
					x1 = e.GetX ();
					y1 = e.GetY ();
					drawPath.LineTo (x1, y1);
				}
				break;
			case MotionEventActions.Up:
				drawCanvas.DrawPath(drawPath, drawPaint);
				drawPath.Reset();
				break;
			case MotionEventActions.PointerDown:
				secondaryId = e.GetPointerId (e.ActionIndex);
				bTwoFinger = true;
				x2 = e.GetX (secondaryId);
				y2 = e.GetY (secondaryId);
				drawPath2.MoveTo (x2, y2);
				break;
			case MotionEventActions.PointerUp:
				bTwoFinger = false;
				drawCanvas.DrawPath(drawPath2, drawPaint2);
				drawPath2.Reset();
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

