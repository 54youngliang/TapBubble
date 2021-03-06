﻿using UnityEngine;
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
#if UNITY_EDITOR_64
		return;
#endif
		Debug.Log("-------------0");
		this.enabled = false;
        _Instance = this;
        if(FB.IsInitialized)
        {
			this.enabled = true;
            FB.ActivateApp();
        }
        else
        {
            FB.Init(SetInit);  
        }
		FB.Login("publish_actions", LoginCallback);
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
        if (!pauseStatus && this.enabled)
        {
			Debug.Log("-------------1");
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
        Debug.Log("LoginCallback isLoggedIn:"  + FB.IsLoggedIn + "  error:" + result.Error);
		Debug.Log(result.ToString());

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
		Debug.Log("enabled:" + this.enabled + "  loggin:" + FB.IsLoggedIn);
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
