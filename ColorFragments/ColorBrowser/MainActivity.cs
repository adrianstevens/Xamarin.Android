using Android.Widget;
using Android.OS;
using Android.Support.V4.App;
using Android.Graphics.Drawables;

namespace ColorBrowser
{
    [Android.App.Activity(Label = "ColorBrowser", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : FragmentActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView (Resource.Layout.Main);

            FindViewById<Button>(Resource.Id.buttonBlue).Click += ColorButtonClick;
            FindViewById<Button>(Resource.Id.buttonGreen).Click += ColorButtonClick;
            FindViewById<Button>(Resource.Id.buttonPink).Click += ColorButtonClick;
            FindViewById<Button>(Resource.Id.buttonDarkBlue).Click += ColorButtonClick;
        }

        private void ColorButtonClick(object sender, System.EventArgs e)
        {
            var btn = sender as Button;

            var color = ((ColorDrawable)btn.Background).Color;

            var fragment = new ColorFragment(btn.Text, color);

            SupportFragmentManager.BeginTransaction()
               .Replace(Resource.Id.frameLayout, fragment)
               .Commit();
        }
    }
}