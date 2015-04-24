using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using XamPaint;

namespace XamPaintMultiTouch
{
	[Activity (Label = "XamPaintMultiTouch", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			var btnClear = FindViewById<Button> (Resource.Id.btnClear);
			var viewDraw = FindViewById<PaintView> (Resource.Id.viewDrawing);

			btnClear.Click += (sender, e) => viewDraw.Clear();
		}
	}
}


