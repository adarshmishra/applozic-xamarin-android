using System;
using Android.App;
using Firebase.Iid;
using Android.Util;
using Com.Applozic.Mobicomkit.Api.Account.User;
using Com.Applozic.Mobicomkit.Api.Account.Register;

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
			SendRegistrationToServer(refreshedToken);
		}

		void SendRegistrationToServer(string token)
		{
			// Add custom implementation, as needed.
			var applozicPref = MobiComUserPreference.GetInstance(this);
			if (applozicPref.IsRegistered)
			{
				var registerClient = new RegisterUserClientService(this);
				registerClient.UpdatePushNotificationId(token);
			}
		}
	}
}
