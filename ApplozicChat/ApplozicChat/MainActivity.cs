﻿using Android.App;
using Android.Widget;
using Android.OS;
using Applozic;
using Firebase.Iid;
using Android.Util;


namespace ApplozicChat
{
	[Activity(Label = "Applozic Chat", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			ApplozicChatManager chatManager = new ApplozicChatManager(this);

			if (!chatManager.ISUserLoggedIn())
			{
				chatManager.RegisterUser("xam01", "Xamarin 01", "123");
			}

			// Get our button from the layout resource,
			// and attach an event to it
			Button launchChatList = FindViewById<Button>(Resource.Id.launch_chat_list);
			Button launchOneToOneChat = FindViewById<Button>(Resource.Id.launch_one_to_one_chat);
			Button logout = FindViewById<Button>(Resource.Id.logout);

			launchChatList.Click += delegate
			{
				Log.Debug("ApplozicChat", "InstanceID token: " + FirebaseInstanceId.Instance.Token);
				chatManager.LaunchChatList();

			};
			launchOneToOneChat.Click += delegate
			{

				chatManager.LaunchChatWithUser("ak02");

			};
			logout.Click += delegate
			{
				chatManager.Logout();
			};

		}
	}
}

