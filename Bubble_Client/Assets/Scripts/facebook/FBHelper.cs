using UnityEngine;
using System.Collections;

public class FBHelper : MonoBehaviour {

    private static FBHelper _Instance;
    public static FBHelper Instance
    {
        get
        {
            return _Instance;
        }
    }
    void Awake()
    {
        _Instance = this;
        if(FB.IsInitialized)
        {
            FB.ActivateApp();
        }
        else
        {
            FB.Init(SetInit);  
        }
    }

    private void SetInit()
    {
        Debug.Log("SetInit");
        FB.ActivateApp();
        enabled = true; // "enabled" is a property inherited from MonoBehaviour                  
        if (FB.IsLoggedIn)
        {
            Debug.Log("Already logged in");
            OnLoggedIn();
        }
    }

    void OnApplicationPause(bool pauseStatus)
    {
        // Check the pauseStatus to see if we are in the foreground
        // or background
        if (!pauseStatus)
        {
            //app resume
            if (FB.IsInitialized)
            {
                FB.ActivateApp();
            }
            else
            {
                //Handle FB.Init
                FB.Init(SetInit);  
            }
        }
    }

    void LoginCallback(FBResult result)
    {
        Debug.Log("LoginCallback");

        if (FB.IsLoggedIn)
        {
            OnLoggedIn();
            if (_LastFeedParams != null)
            {
                SendFeed(_LastFeedParams);
                _LastFeedParams = null;
            }
        }
    }

    void OnLoggedIn()
    {
        Debug.Log("Logged in. ID: " + FB.UserId);
    }

    private FBFeedParams _LastFeedParams;
    public void Share(FBFeedParams feedParams)
    {
        if (!enabled)
        {
            Debug.Log("FB not init");
            return;
        }
        if (!FB.IsLoggedIn)
        {
            _LastFeedParams = feedParams;
            FB.Login("", LoginCallback);
        }
        else
        {
            SendFeed(feedParams);
        }
    }

    private void SendFeed(FBFeedParams feedParams)
    {
        FB.Feed(
        link: feedParams.link,
        linkName: feedParams.linkName,
        linkCaption: feedParams.linkCaption,
        linkDescription: feedParams.linkDescription,
        picture: feedParams.picture,
        callback: feedParams.callback
    );
    }
}
