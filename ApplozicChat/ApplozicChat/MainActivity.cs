using Android.App;
using Android.Widget;
using Android.OS;
using Applozic;

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
				chatManager.RegisterUser("XamAndroid", "Adarsh 01", "123");
			}

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button>(Resource.Id.myButton);

			button.Click += delegate
			{

				chatManager.launchChatList();

			};

		}
	}
}

