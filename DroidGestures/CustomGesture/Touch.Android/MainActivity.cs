using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Gestures;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace TouchWalkthrough
{
	[Activity(Label="Custom Gesture", MainLauncher=true)]
	public class MainActivity : Activity
	{
		GestureOverlayView gestureOverlayView;
		GestureLibrary     gestureLibrary;
		ImageView          imageView;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			SetContentView(Resource.Layout.main);

			gestureOverlayView = FindViewById<GestureOverlayView>(Resource.Id.gov);
			imageView          = FindViewById<ImageView>         (Resource.Id.imageView);

			gestureLibrary = GestureLibraries.FromRawResource(this, Resource.Raw.gestures);
			if (!gestureLibrary.Load())
			{
				Log.Debug(GetType().FullName, "There was a problem loading the gesture library.");
				Finish();
			}

			//
			// Java style
			//
			// I recommend not covering this in the course. Every Android API that uses "callbacks" uses this Java-style pattern
			// and the Xamarin team dutifully made it available in Xamarin.Android. However, they also duplicated this functionality
			// using C# events. Most C# programmers will prefer the events. In other words, this "two ways to do the same thing"
			// is common all through Xamarin.Android. It's not specific to gestures and I don't see any reason to cover it here.
			//
			//gestureOverlayView.AddOnGestureListener(new MyListener(gestureLibrary, imageView));

			//
			// C# style
			//
			// I recommend mentioning the 4 methods from IOnGestureListener and covering at least the GesturePerformed method in detail.
			// I'm not sure about the other 3 methods since I don't know what they would be used for. I think the hard part of this
			// whole topic is understanding how to build and recognize the custom gesture. So I would spend more time on the
			// builder, the overlay view, and some of the concepts behind the predictions/matches rather than spend time on a complete
			// list of all the different events. As always, it depends on how long the class gets :-)
			//
			// 4 methods from the GestureOverlayView.IOnGestureListener interface
			//gestureOverlayView.GestureStarted   += OnGestureStarted;
			gestureOverlayView.GesturePerformed += OnGesturePerformed;
			//gestureOverlayView.GestureCancelled += OnGestureCancelled;
			//gestureOverlayView.GestureEnded     += OnGestureEnded;
			// 2 methods from the GestureOverlayView.IOnGesturingListener interface
			//gestureOverlayView.GesturingEnded   += OnGesturingEnded;
			//gestureOverlayView.GesturingStarted += OnGesturingStarted;
			// 1 method from the GestureOverlayView.IOnGesturePerformedListener interface
			//gestureOverlayView.GestureEvent     += OnGestureEvent;
		}

		void OnGestureStarted  (object sender, GestureOverlayView.GestureStartedEventArgs e)
		{
		}
		void OnGesturePerformed(object sender, GestureOverlayView.GesturePerformedEventArgs e)
		{
			IEnumerable<Prediction> predictions = from p in gestureLibrary.Recognize(e.Gesture)
				orderby p.Score descending
					where p.Score > 1.0
				select p;
			Prediction prediction = predictions.FirstOrDefault();

			if (prediction == null)
			{
				Log.Debug(GetType().FullName, "Nothing seemed to match the user's gesture, so don't do anything.");
				return;
			}

			Log.Debug(GetType().FullName, "Using the prediction named {0} with a score of {1}.", prediction.Name, prediction.Score);

			if (prediction.Name.StartsWith("checkmark"))
			{
				imageView.SetImageResource(Resource.Drawable.success);
			}
			else if (prediction.Name.StartsWith("erase", StringComparison.OrdinalIgnoreCase))
			{
				// Match one of our "erase" gestures
				imageView.SetImageResource(Resource.Drawable.prompt);
			}
		}
		void OnGestureCancelled(object sender, GestureOverlayView.GestureCancelledEventArgs e) {}
		void OnGestureEnded    (object sender, GestureOverlayView.GestureEndedEventArgs     e) {} 
		void OnGesturingStarted(object sender, GestureOverlayView.GesturingStartedEventArgs e) {}
		void OnGesturingEnded  (object sender, GestureOverlayView.GesturingEndedEventArgs   e) {}
		void OnGestureEvent    (object sender, GestureOverlayView.GestureEventArgs          e) {}
	}

	class MyGesterPerformedListener : Java.Lang.Object, GestureOverlayView.IOnGesturePerformedListener
	{
		public void OnGesturePerformed(GestureOverlayView overlay, Gesture gesture)
		{
		}
	}

	class MyListener : Java.Lang.Object, GestureOverlayView.IOnGestureListener
	{
		GestureLibrary gestureLibrary;
		ImageView      imageView;

		public MyListener(GestureLibrary gl, ImageView iv)
		{
			gestureLibrary = gl;
			imageView      = iv;
		}

		public void OnGesture         (GestureOverlayView overlay, MotionEvent e) { }
		public void OnGestureStarted  (GestureOverlayView overlay, MotionEvent e) { }
		public void OnGestureCancelled(GestureOverlayView overlay, MotionEvent e) { }
		public void OnGestureEnded    (GestureOverlayView overlay, MotionEvent e)
		{
			IEnumerable<Prediction> predictions = from p in gestureLibrary.Recognize(overlay.Gesture)
				orderby p.Score descending
					where p.Score > 1.0
				select p;
			Prediction prediction = predictions.FirstOrDefault();

			if (prediction == null)
			{
				Log.Debug(GetType().FullName, "Nothing seemed to match the user's gesture, so don't do anything.");
				return;
			}

			Log.Debug(GetType().FullName, "Using the prediction named {0} with a score of {1}.", prediction.Name, prediction.Score);

			if (prediction.Name.StartsWith("checkmark"))
			{
				imageView.SetImageResource(Resource.Drawable.success);
			}
			else if (prediction.Name.StartsWith("erase", StringComparison.OrdinalIgnoreCase))
			{
				// Match one of our "erase" gestures
				imageView.SetImageResource(Resource.Drawable.prompt);
			}
		}
	}
}