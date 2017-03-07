using System;
using Android.Content;
using Java.Lang;
using Com.Applozic.Mobicomkit.Uiwidgets.Conversation;

namespace Applozic
{
	public class ApplozicChatManager
	{
		Context context;
		public ApplozicChatManager(Context context)
		{
			this.context = context;

		}
		/*
		 * Check if Applozic user is logged in already 
		 * 
		 */

		public bool ISUserLoggedIn()
		{
			var applozicPref = Com.Applozic.Mobicomkit.Api.Account.User.MobiComUserPreference.GetInstance(context);
			return applozicPref.IsRegistered;

		}

		/*
		 */

		public void RegisterUser(string userId, string displayName, string password )
		{
			// Build Applozic users..

			var user = new Com.Applozic.Mobicomkit.Api.Account.User.ApplozicUser();
			user.DisplayName = displayName;
			user.UserId = userId;
			user.Password = password;

			UserLoginListener userLoginListener = new UserLoginListener();
			Java.Lang.Void[] args = null;
			new Com.Applozic.Mobicomkit.Api.Account.User.UserLoginTask(user, userLoginListener, context).Execute(args);
		}

		/**
		 * This is the example of shows
		 * 
		 */
		public void launchChatList()
		{
			context.StartActivity(typeof(Com.Applozic.Mobicomkit.Uiwidgets.Conversation.Activity.ConversationActivity));
		
		}
		/**
		 * 
		 */
		public void launchChatWithUser(string receiverUserId)
		{
			Intent myIntent = new Intent(context, typeof(Com.Applozic.Mobicomkit.Uiwidgets.Conversation.Activity.ConversationActivity));
			myIntent.PutExtra("takeOrder", true);
			myIntent.PutExtra(ConversationUIService.UserId, receiverUserId);
			myIntent.PutExtra(ConversationUIService.DisplayName,"DISPLAY_NAME");

			context.StartActivity(myIntent);
		}
	}

	/**
	 * 
	 * Class for initialising chat..
	 */
	public class UserLoginListener : Java.Lang.Object, Com.Applozic.Mobicomkit.Api.Account.User.UserLoginTask.ITaskListener
	{

		//RegistrationResponse registrationResponse, Context context
		public void OnSuccess(Com.Applozic.Mobicomkit.Api.Account.Register.RegistrationResponse res, Android.Content.Context context)
		{
			System.Console.WriteLine("response from server::" + res.Message);

		}

		public void OnFailure(Com.Applozic.Mobicomkit.Api.Account.Register.RegistrationResponse res, Java.Lang.Exception e)
		{
			System.Console.WriteLine("Exception ::" + e.Message);


		}


	}

}
