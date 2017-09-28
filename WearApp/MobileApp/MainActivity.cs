using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Support.V4.Content;

namespace WatchApp
{
    [Activity(Label = "MobileApp", MainLauncher = true)]
    public class MainActivity : Activity
    {
        TextView txtMsg;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our TextBox from the layout resource,
            txtMsg = FindViewById<TextView>(Resource.Id.txtMessage);


            IntentFilter filter = new IntentFilter(Intent.ActionSend);
            MessageReciever receiver = new MessageReciever(this);
            LocalBroadcastManager.GetInstance(this).RegisterReceiver(receiver, filter);
        }

        public void ProcessMessage(Intent intent)
        {
            txtMsg.Text = intent.GetStringExtra("WearMessage");
        }

        internal class MessageReciever : BroadcastReceiver
        {
            MainActivity activity;

            public MessageReciever(MainActivity owner)
            {
                this.activity = owner;
            }

            public override void OnReceive(Context context, Intent intent)
            {
                activity.ProcessMessage(intent);
            }
        }
    }

}

