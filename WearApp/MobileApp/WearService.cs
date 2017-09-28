using Android.App;
using Android.Content;
using Android.Gms.Common.Apis;
using Android.Gms.Wearable;
using Android.Runtime;
using Android.Support.V4.Content;
using System.Linq;

namespace MobileApp
{
    [Service]
    [IntentFilter(new[] { "com.google.android.gms.wearable.BIND_LISTENER" })]
    public class WearService : WearableListenerService
    {
        const string _syncPath = "/WearDemo/Data";
        GoogleApiClient client;

        public override void OnCreate()
        {
            base.OnCreate();
            client = new GoogleApiClient.Builder(this.ApplicationContext)
                    .AddApi(WearableClass.Api)
                    .Build();

            client.Connect();

            Android.Util.Log.Info("WearIntegrationreated", "App");
        }

        public override void OnDataChanged(DataEventBuffer dataEvents)
        {
            var dataEvent = Enumerable.Range(0, dataEvents.Count)
                                      .Select(i => dataEvents.Get(i).JavaCast<IDataEvent>())
                                      .FirstOrDefault(x => x.Type == DataEvent.TypeChanged && x.DataItem.Uri.Path.Equals(_syncPath));
            if (dataEvent == null)
                return;

            //get data from wearable
            var dataMapItem = DataMapItem.FromDataItem(dataEvent.DataItem);
            var map = dataMapItem.DataMap;
            string message = dataMapItem.DataMap.GetString("Message");

            Intent intent = new Intent();
            intent.SetAction(Intent.ActionSend);
            intent.PutExtra("WearMessage", message);
            LocalBroadcastManager.GetInstance(this).SendBroadcast(intent);
        }
    }
}