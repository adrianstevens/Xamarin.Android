using System;
using Android.Views;

namespace RotationGesture
{
	public class RotationGestureDetector
	{
		private IOnRotationGestureListener rotationListener;

		private int pId1, pId2;

		private static int INVALID_ID = -1;

		private float angleOffset;
		private float angle;

		public RotationGestureDetector (IOnRotationGestureListener listener)
		{
			rotationListener = listener;
			pId1 = INVALID_ID;
			pId2 = INVALID_ID;
		}

		public bool OnTouchEvent (MotionEvent e)
		{
			switch (e.ActionMasked) 
			{
			case MotionEventActions.Down:
				pId1 = e.GetPointerId(e.ActionIndex);
				break;
			case MotionEventActions.PointerDown:
				pId2 = e.GetPointerId (e.ActionIndex);
				angleOffset = GetAngle (e, pId1, pId2);
				break;
			case MotionEventActions.Move:
				if (e.PointerCount != 2 || 
					pId1 == INVALID_ID || pId2 == INVALID_ID)
					return false;

				angle = GetAngle (e, pId1, pId2) - angleOffset;

				if (rotationListener != null)
					rotationListener.OnRotate (angle); 

				break;
			case MotionEventActions.Up:
				pId1 = INVALID_ID;
				break;
			case MotionEventActions.PointerUp:
				pId2 = INVALID_ID;
				break;
			}
			return true;
		}

		float GetAngle (MotionEvent e, int pointerId1, int pointerId2)
		{
			var x1 = e.GetX (pointerId1);
			var y1 = e.GetY (pointerId1);
			var x2 = e.GetX (pointerId2);
			var y2 = e.GetY (pointerId2);

			return GetAngle (x1, y1, x2, y2);
		}

		float GetAngle (float x1, float y1, float x2, float y2)
		{
			double angle = Math.Atan2((y1 - y2), (x1 - x2));

			return (float)(angle * 180 / Math.PI);
		}
	}
}

