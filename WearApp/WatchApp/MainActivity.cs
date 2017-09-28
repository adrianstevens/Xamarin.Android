using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.Wearable.Views;
using Android.Support.V4.App;
using Android.Gms.Common.Apis;
using Android.Gms.Wearable;
using System.Linq;
using Android.Runtime;

namespace WatchApp
{
    [Activity(Label = "WatchApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, IDataApiDataListener, GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener
    {
        GoogleApiClient client;
        const string syncPath = "/WearDemo/Data"; 

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            client = new GoogleApiClient.Builder(this, this, this)
                            .AddApi(WearableClass.Api)
                            .Build();

            SetContentView(Resource.Layout.Main);

            var v = FindViewById<WatchViewStub>(Resource.Id.watch_view_stub);
            v.LayoutInflated += delegate
            {
                Button button = FindViewById<Button>(Resource.Id.myButton);

                button.Click += delegate
                {
                    var notification = new NotificationCompat.Builder(this)
                        .SetContentTitle("Button tapped")
                        .SetContentText("Button tapped")
                        .SetSmallIcon(Android.Resource.Drawable.StatNotifyVoicemail)
                        .SetGroup("group_key_demo").Build();

                    var manager = NotificationManagerCompat.From(this);
                    manager.Notify(1, notification);
                    button.Text = "Check Notification!";
                };
            };
        }

        public void SendData()
        {
            try
            {
                var request = PutDataMapRequest.Create(syncPath);
                var map = request.DataMap;
                map.PutString("Message", "Vinz says Hello from Wearable!");
                map.PutLong("UpdatedAt", DateTime.UtcNow.Ticks);
                WearableClass.DataApi.PutDataItem(client, request.AsPutDataRequest());
            }
            finally
            {
                client.Disconnect();
            }

        }
        protected override void OnStart()
        {
            base.OnStart();
            client.Connect();
        }
        public void OnConnected(Bundle p0)
        {
            WearableClass.DataApi.AddListener(client, this);
        }

        public void OnConnectionSuspended(int reason)
        {
            Android.Util.Log.Error("GMSonnection suspended " + reason, "watch");
            WearableClass.DataApi.RemoveListener(client, this);
        }

        public void OnConnectionFailed(Android.Gms.Common.ConnectionResult result)
        {
            Android.Util.Log.Error("GMSonnection failed " + result.ErrorCode, "watch");
        }

        protected override void OnStop()
        {
            base.OnStop();
            client.Disconnect();
        }

        public void OnDataChanged(DataEventBuffer dataEvents)
        {
            var dataEvent = Enumerable.Range(0, dataEvents.Count)
                                      .Select(i => dataEvents.Get(i).JavaCast<IDataEvent>())
                                      .FirstOrDefault(x => x.Type == DataEvent.TypeChanged && x.DataItem.Uri.Path.Equals(syncPath));

            if (dataEvent == null)
                return;

            //do work here
        }

       
    }
}


