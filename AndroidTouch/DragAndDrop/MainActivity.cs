using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics.Drawables;

namespace DragAndDrop
{
	[Activity (Label = "DragAndDrop", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity, Android.Views.View.IOnTouchListener, Android.Views.View.IOnDragListener
	{
		Drawable drawEnter, drawNormal;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			drawEnter = this.Resources.GetDrawable (Resource.Drawable.DropTarget);
			drawNormal = this.Resources.GetDrawable (Resource.Drawable.Shape);

			WireEvents ();
		}

		void WireEvents ()
		{
			FindViewById<ImageView> (Resource.Id.imgIcon1).SetOnTouchListener (this);
			FindViewById<ImageView> (Resource.Id.imgIcon2).SetOnTouchListener (this);
			FindViewById<ImageView> (Resource.Id.imgIcon3).SetOnTouchListener (this);
			FindViewById<ImageView> (Resource.Id.imgIcon4).SetOnTouchListener (this);

			FindViewById<LinearLayout> (Resource.Id.boxBottomLeft).SetOnDragListener (this);
			FindViewById<LinearLayout> (Resource.Id.boxBottomRight).SetOnDragListener (this);
			FindViewById<LinearLayout> (Resource.Id.boxTopLeft).SetOnDragListener (this);
			FindViewById<LinearLayout> (Resource.Id.boxTopRight).SetOnDragListener (this);

		}

		bool View.IOnTouchListener.OnTouch (View v, MotionEvent e)
		{
			if (e.Action  == MotionEventActions.Down) 
			{
				ClipData data = ClipData.NewPlainText("", "");
				Android.Views.View.DragShadowBuilder shadowBuilder = new View.DragShadowBuilder(v);
				v.StartDrag(data, shadowBuilder, v, 0);
				v.Visibility = ViewStates.Invisible;
				return true;
			} 
			else 
			{
				return false;
			}
		}

		bool View.IOnDragListener.OnDrag (View v, DragEvent e)
		{
			switch (e.Action) 
			{
			case DragAction.Started:
				break;
			case DragAction.Entered:
				v.SetBackgroundDrawable(drawEnter);
				break;
			case DragAction.Exited:
				v.SetBackgroundDrawable(drawNormal);
				break;
			case DragAction.Drop:
				// Dropped, reassign View to ViewGroup
				View view = (View)e.LocalState;
				ViewGroup owner = (ViewGroup)view.Parent;
				owner.RemoveView (view);
				LinearLayout container = (LinearLayout)v;
				container.AddView (view);
				view.Visibility = ViewStates.Visible;
				break;
			case DragAction.Ended:
				v.SetBackgroundDrawable (drawNormal);
				break;
			default:
				break;
			}
			return true;

		}
	}
}


