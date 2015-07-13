using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelPassedWindow : MonoBehaviour {
	
	public HomeWindow homeWindow;
	public UISprite starUISprite;
	public PlayAnimation playAnimation;

	public UILabel LevelLabel;

	public void Show(int level, int star)
	{
		AppMain.Instance.HomeWindow.needHiddenNextButton = false;
		AppMain.Instance.HomeWindow.NextLevelButton.SetActive (true);
		AppMain.Instance.HomeWindow.NextLevelButton.GetComponent<PlayAnimation> ().StartNormalPlay ();
		AppMain.Instance.HomeWindow.NextLevelButton.GetComponent<NextButton> ().label.SetActive (true);
		LevelLabel.text = level.ToString();
		starUISprite.spriteName = "success_"+star;
	}

	public void ShowHome()
	{
		this.gameObject.SetActive (false);
		AppMain.Instance.HomeWindow.ShowHomeWindow ();
	}

	public void StartAgain()
	{
		AppMain.Instance.HomeWindow.NextLevelButton.SetActive (false);
		this.gameObject.SetActive (false);
		AppMain.Instance.HomeWindow.gameController.BeginMission ();
	}

	public void Share(){
		Debug.Log ("send feed");
		FBFeedParams feed = new FBFeedParams ();
		feed.link="share link";
		feed.linkName="link name";
		feed.linkCaption="caption";
		feed.linkDescription="desc";
		feed.picture="pic url";
		FBHelper.Instance.Share (feed);
	}
}
