using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;

using Android.Widget;
using Android.OS;
using Android.Graphics.Drawables;
using Android.Util;

namespace DragAndDropEvents
{
	[Activity (Label = "DragAndDrop", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
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
			FindViewById<ImageView> (Resource.Id.imgIcon1).Touch += OnTouchIcons;
			FindViewById<ImageView> (Resource.Id.imgIcon2).Touch += OnTouchIcons;
			FindViewById<ImageView> (Resource.Id.imgIcon3).Touch += OnTouchIcons;
			FindViewById<ImageView> (Resource.Id.imgIcon4).Touch += OnTouchIcons;

			FindViewById<LinearLayout> (Resource.Id.boxBottomLeft).Drag += OnDragIcons;
			FindViewById<LinearLayout> (Resource.Id.boxBottomRight).Drag += OnDragIcons;
			FindViewById<LinearLayout> (Resource.Id.boxTopLeft).Drag += OnDragIcons;
			FindViewById<LinearLayout> (Resource.Id.boxTopRight).Drag += OnDragIcons;
		}

		void OnTouchIcons (object sender, View.TouchEventArgs e)
		{
			var v = sender as View;

			if (v != null && e.Event.Action == MotionEventActions.Down) //when you tap on one of the views
			{
				var shadowBuilder = new View.DragShadowBuilder(v);
				v.StartDrag(null, shadowBuilder, v, 0);
				v.Visibility = ViewStates.Invisible;
			} 
		}

		void OnDragIcons (object sender, View.DragEventArgs e)
		{
			var v = sender as View;
			if (v == null)
				return;

			switch (e.Event.Action) 
			{
			case DragAction.Location:
				Log.WriteLine (LogPriority.Debug, "OnDragIcons", 
					String.Format("View: {0} Location {1}, {2}", v.Id, e.Event.GetX(), e.Event.GetY()));
				break;
			case DragAction.Entered:
				v.Background = drawEnter;
				break;
			case DragAction.Exited:
				v.Background = drawNormal;
				break;
			case DragAction.Drop:
				// Dropped, reassign View to ViewGroup
				View view = (View)e.Event.LocalState;
				ViewGroup owner = (ViewGroup)view.Parent;
				owner.RemoveView (view);
				LinearLayout container = (LinearLayout)v;
				container.AddView (view);
				view.Visibility = ViewStates.Visible;
				break;
			case DragAction.Ended:
				v.Background = drawNormal;
				break;
			default:
				break;
			}
		}
	}
}


