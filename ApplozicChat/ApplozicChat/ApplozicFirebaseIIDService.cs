using System;
using Android.App;
using Firebase.Iid;
using Android.Util;
using Firebase.Messaging;
using Com.Applozic.Mobicomkit.Api.Notification;
using Applozic;

namespace ApplozicChat
{
	[Service]
	[IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
	public class ApplozicFirebaseIIDService : FirebaseInstanceIdService
	{
		const string TAG = "MyFirebaseIIDService";
	
		public override void OnTokenRefresh()
		{
			var refreshedToken = FirebaseInstanceId.Instance.Token;
			Log.Debug(TAG, "Refreshed token: " + refreshedToken);

			ApplozicChatManager ChatManger = new ApplozicChatManager(this);
			ChatManger.SendRegistrationToServer(refreshedToken);
		}

	
	}


	[Service]
	[IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
	public class MyFirebaseMessagingService : FirebaseMessagingService
	{
		const string TAG = "MyFirebaseMsgService";
		public override void OnMessageReceived(RemoteMessage message)
		{
			Log.Debug(TAG, "From: " + message.From);
			Log.Debug(TAG, "Notification Message Body: " + message.Data);

			Log.Info(TAG, "Message data:" + message.Data);

			if (message.Data.Count >0 )
			{
				if (MobiComPushReceiver.IsMobiComPushNotification(message.Data))
				{
					Log.Info(TAG, "Applozic notification processing...");
					MobiComPushReceiver.ProcessMessageAsync(this, message.Data);
					return;
				}
			}
		}
	}


}
