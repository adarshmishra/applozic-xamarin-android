
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Applozic;
using Com.Applozic.Mobicomkit.Api.Account.Register;

namespace ApplozicChat
{
	[Activity(Label = "Login" , MainLauncher = true, Icon = "@mipmap/icon")]
	public class LoginActivity : Activity
	{

		private UserLoginListener loginListener;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			loginListener= new UserLoginListener();
			loginListener.OnRegistrationSucessHandler += OnRegistrationSucessHandler;
			loginListener.OnRegistrationFailedHandler += OnRegistrationFailedHandler;

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.login_activity_layout);

			// Get our button from the layout resource,
			// and attach an event to it
			EditText userName = FindViewById<EditText>(Resource.Id.username_input);
			EditText password = FindViewById<EditText>(Resource.Id.password_input);
			Button signIn = FindViewById<Button>(Resource.Id.sign_in_btn);
			ApplozicChatManager chatManager = new ApplozicChatManager(this);


			signIn.Click += delegate
			{
				chatManager.RegisterUser(userName.Text, userName.Text, password.Text, loginListener);
			};

			if (chatManager.ISUserLoggedIn())
			{
				System.Console.WriteLine("Already Registred ::");
				this.StartActivity(typeof(MainActivity));
				this.Finish();
			}


		}


		void OnRegistrationSucessHandler( RegistrationResponse res, Context context)
		{
			System.Console.WriteLine("Successfully got callback in LoginActivity :" + res.Message);
			context.StartActivity(typeof(MainActivity));
			this.Finish();

		}

		void OnRegistrationFailedHandler(RegistrationResponse res, Java.Lang.Exception exception)
		{
			System.Console.WriteLine("Error while doing registrations:" + exception.Message);

			Toast.MakeText(ApplicationContext, "Login Failed : " + exception.Message , ToastLength.Long).Show();
		}

	}

}
