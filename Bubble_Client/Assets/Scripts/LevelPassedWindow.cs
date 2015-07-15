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
		if (level == 50) {
			
		}

		AppMain.Instance.HomeWindow.needHiddenNextButton = false;
		AppMain.Instance.HomeWindow.NextLevelButton.SetActive (true);
		AppMain.Instance.HomeWindow.NextLevelButton.GetComponent<PlayAnimation> ().StartNormalPlay ();
		AppMain.Instance.HomeWindow.NextLevelButton.GetComponent<NextButton> ().label.SetActive (true);
		if (level == 50) {
			AppMain.Instance.HomeWindow.NextLevelButton.SetActive(false);
		}
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
		AppMain.Instance.CurrentLevel -= 1;
		AppMain.Instance.HomeWindow.NextLevelButton.SetActive (false);
		this.gameObject.SetActive (false);
		AppMain.Instance.HomeWindow.gameController.BeginMission ();
	}

	public void Share(){

		FBFeedParams feed = new FBFeedParams ();
		feed.linkName="Count the sheep";
		feed.linkCaption="caption";
		int missionId = AppMain.Instance.CurrentLevel - 1;
		feed.linkDescription=" I just got "+AppMain.Instance.GetStar(missionId)+" stars in mission "+missionId+"!";
		feed.picture="http://static.kirara.happyelements.cn/Sheep/feed"+AppMain.Instance.GetStar(missionId)+".png";
		FBHelper.Instance.Share (feed);
	}
}
