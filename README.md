
# applozic-xamarin-android
Applozic chat in native Xamarin Android.


# Applozic SDK Integration Steps:

#### STEP 1: Add DLLs as reference:

- Add DLLs as references in .net assemblies present in lib-dll folder.

 Your Solution --> References --> Edit References --> .Net Assembly--> Browse and add all dlls from lib-dll folder.
 

#### STEP 2: Add helper class:

Add Helper class [ApplozicChatManager.cs](https://raw.githubusercontent.com/adarshmishra/applozic-xamarin-android/master/ApplozicChat/ApplozicChat/ApplozicChatManager.cs) in your main project. 

#### STEP 3: AndroidManifest changes 

Add activity, Services, permissions and meta-data required for applozic in your AndroidManifest.xml. 

https://www.applozic.com/docs/android-chat-sdk.html#step-2-androidmanifest

i) Register/Login User:

ApplozicChatManger provide convenient method to register user with Applozic. You need to pass UserLoginListener reference which have login success/failure events callbacks.

```    
private UserLoginListener loginListener = new UserLoginListener();
loginListener.OnRegistrationSucessHandler += OnRegistrationSucessHandler;// login success event
loginListener.OnRegistrationFailedHandler += OnRegistrationFailedHandler; //login failure event

ApplozicChatManager chatManager = new ApplozicChatManager(this);
chatManager.RegisterUser(<USER_ID>, <USER_DISPLAY_NAME>, <PASSWORD>, loginListener);
  
```
Event Callback:

```
void OnRegistrationSucessHandler( RegistrationResponse res, Context context)
{
  System.Console.WriteLine("Successfully got callback in LoginActivity :" + res.Message);
}
```

```
void OnRegistrationFailedHandler(RegistrationResponse res, Java.Lang.Exception exception)
{
  System.Console.WriteLine("Error:" + exception.Message);
}
```


NOTE: IF you need to pass more information while doing registration, you can build Applozic user object and use below method for registration.

```
// Build Applozic users..
var user = new ApplozicUser();
user.DisplayName = displayName;
user.UserId = userId;
user.Password = password;
user.AuthenticationTypeId = new Short("1");

ApplozicChatManager chatManager = new ApplozicChatManager(this);
chatManager.RegisterUser( user,loginListener);
```
#### STEP 4: Launch chat:

Use ApplozicChatManager methods to launch different type of chats:

i) Chat/Conversation List:

```
ApplozicChatManager chatManager = new ApplozicChatManager(this);
chatManager.LaunchChatList();

```

ii) One to One chat:

```
ApplozicChatManager chatManager = new ApplozicChatManager(this);
chatManager.LaunchChatWithUser(<USER_ID>);

```

#### STEP 5: Push Notification Setup:

**a) I do not have pushnotification setup:**

 i) Add ApplozicFirebaseIIDService.cs to your project.

ii) Declare the Receiver in the AndroidManifest.
```
<receiver android:name="com.google.firebase.iid.FirebaseInstanceIdInternalReceiver" 
		android:exported="false" />
		<!-- FCM COnfiguration    -->
		<receiver android:name="com.google.firebase.iid.FirebaseInstanceIdReceiver" 
			android:exported="true" android:permission="com.google.android.c2dm.permission.SEND">
			<intent-filter>
				<action android:name="com.google.android.c2dm.intent.RECEIVE" />
				<action android:name="com.google.android.c2dm.intent.REGISTRATION" />
				<category android:name="${applicationId}" />
			</intent-filter>
	</receiver>
    
```

iii) Add google-service.json file to your project root directory, add to project and set build Build Action to GoogleServicesJson. 

Help Link: https://developer.xamarin.com/guides/android/application_fundamentals/notifications/remote-notifications-with-fcm/#Add_the_Google_Services_JSON_File



**b) I have Pushnotification setup:**

i) Send pushnotification token to Applozic server:

In your FirebaseInstanceIdService implementation class's OnTokenRefresh method send token to Applozic server. 

```
ApplozicChatManager ChatManger = new ApplozicChatManager(this);
ChatManger.SendRegistrationToServer(refreshedToken);

```
**Example:**
```
public override void OnTokenRefresh()
		{
			var refreshedToken = FirebaseInstanceId.Instance.Token;
			Log.Debug(TAG, "Refreshed token: " + refreshedToken);
      //Send token to applozic server..
			ApplozicChatManager ChatManger = new ApplozicChatManager(this);
			ChatManger.SendRegistrationToServer(refreshedToken);
		}
```

ii ) Receving and passing applozic notiifcations to SDK:

In your FirebaseMessagingService implementation class's OnMessageReceived method, check if notification belogs to Applozic.

```
//ADD below as a first code in OnMessageReceived.
if (message.Data.Count >0 )
			{
				if (MobiComPushReceiver.IsMobiComPushNotification(message.Data))
				{
					Log.Info(TAG, "Applozic notification processing...");
					MobiComPushReceiver.ProcessMessageAsync(this, message.Data);
					return;
				}
			}
      
```

**Example***

```
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
```

### UI customisations from settings:

For possible UICustomisations, please vist our Android Documentaion page below:

https://www.applozic.com/docs/android-chat-sdk.html#customization
