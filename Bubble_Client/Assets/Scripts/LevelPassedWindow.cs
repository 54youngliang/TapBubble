using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelPassedWindow : MonoBehaviour {
	
	public HomeWindow homeWindow;
	public UISprite starUISprite;

	public UILabel LevelLabel;

	public void Show(int level, int star)
	{
		LevelLabel.text = level.ToString();
		starUISprite.spriteName = "result_star_"+star;
	}

	public void GoNextLevel()
	{
		Debug.Log ("Click to Next Level");
		this.gameObject.SetActive (false);
		GameObject earth = AppMain.Instance.HomeWindow.background.GetComponent<Background> ().earth;
		float z = earth.transform.localEulerAngles.z;
		TweenRZ.Add (earth, 1f,(z+40f) ).OnComplete+= AppMain.Instance.HomeWindow.BeginMission;
	}

	public void ShowHome()
	{
		this.gameObject.SetActive (false);
		AppMain.Instance.HomeWindow.ShowHomeWindow ();
	}

	public void StartAgain()
	{
		this.gameObject.SetActive (false);
		AppMain.Instance.HomeWindow.gameController.BeginMission ();
	}
}
