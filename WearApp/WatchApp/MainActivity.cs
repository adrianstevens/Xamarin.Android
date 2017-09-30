using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Gms.Common.Apis;
using Android.Gms.Wearable;
using System.Linq;
using Android.Runtime;

namespace WatchApp
{
    [Activity(Label = "WatchApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, IDataApiDataListener, GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener
    {
        int tapCounter = 0;
        GoogleApiClient client;
        const string syncPath = "/WearDemo/Data"; 

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            client = new GoogleApiClient.Builder(this, this, this)
                            .AddApi(WearableClass.API)
                            .Build();

            SetContentView(Resource.Layout.Main);

            Button button = FindViewById<Button>(Resource.Id.myButton);
            button.Click += (sender, e) => SendData();
        }

        public void SendData()
        {
            try
            {
                tapCounter++;
                var request = PutDataMapRequest.Create(syncPath);
                var map = request.DataMap;
                map.PutString("Message", "Hello, Tap Counter value now at " + tapCounter);
                map.PutLong("UpdatedAt", DateTime.UtcNow.Ticks);
                WearableClass.DataApi.PutDataItem(client, request.AsPutDataRequest());
            }
            catch (Exception ex)
            {
                Android.Util.Log.Error("Exception Occurred: " + ex.Message, "watch");
            }
            //Note: Uncomment only if you want to send a message only once
            //finally
            //{
            //    client.Disconnect();
            //}
        }

        protected override void OnStart()
        {
            client.Connect();
            base.OnStart();
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
            client.Disconnect();
            base.OnStop();
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