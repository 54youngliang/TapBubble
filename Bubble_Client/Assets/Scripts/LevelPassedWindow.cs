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
		AppMain.Instance.HomeWindow.NextLevelButton.GetComponent<PlayAnimation> ().StartAnimation ();
		AppMain.Instance.HomeWindow.NextLevelButton.GetComponent<NextButton> ().label.SetActive (true);
		LevelLabel.text = level.ToString();
		starUISprite.spriteName = "success_"+star;
	}

	public void GoNextLevel()
	{
		Debug.Log ("Click to Next Level");
		GameObject earth = AppMain.Instance.HomeWindow.background.GetComponent<Background> ().earth;
		float z = earth.transform.localEulerAngles.z;
		TweenRZ.Add (earth, 1f,(z+40f) ).OnComplete+= Dispear;
	}

	private void Dispear(){
		playAnimation.StopAnimation ();
		this.gameObject.SetActive (false);
		AppMain.Instance.HomeWindow.BeginMission ();
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
}
