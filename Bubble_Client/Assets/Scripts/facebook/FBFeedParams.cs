﻿using UnityEngine;
using System.Collections;
using Facebook;

public class FBFeedParams
{
    /***
    link: "https://example.com/myapp/?storyID=thelarch",
        linkName: "The Larch",
        linkCaption: "I thought up a witty tagline about larches",
        linkDescription: "There are a lot of larch trees around here, aren't there?",
        picture: "https://example.com/myapp/assets/1/larch.jpg"
        //callback: LogCallback
    **/
	public string link="https://play.google.com/store/apps/details?id=com.happyelements.lab.chihiro";
    public string linkName;
    public string linkCaption;
    public string linkDescription;
    public string picture;
    public FacebookDelegate callback = null;


}
