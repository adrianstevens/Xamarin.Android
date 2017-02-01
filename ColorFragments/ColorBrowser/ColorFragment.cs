using Android.Graphics;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;

namespace ColorBrowser
{
    class ColorFragment : Fragment
    {
        string title;
        Color color;

        public ColorFragment()
        {
        }

        public ColorFragment(string title, Color color)
        {
            this.title = title;
            this.color = color;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.ColorFragment, null);

            view.FindViewById<View>(Resource.Id.viewColor).SetBackgroundColor(color);
            view.FindViewById<TextView>(Resource.Id.textTitle).Text = title + " #" + color.ToArgb().ToString("X");

            return view;
        }
    }
}