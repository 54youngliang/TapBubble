using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelPassedWindow : MonoBehaviour {

	public List<UISprite> StarList;
	public HomeWindow homeWindow;

	public UILabel LevelLabel;

	public void Show(int level, int star)
	{
		LevelLabel.text = level.ToString();
		for(int i = 0; i < StarList.Count; i ++)
		{
			StarList[i].enabled = i < star;
		}
	}

	public void GoNextLevel()
	{
		Debug.Log ("Click to Next Level");
		this.gameObject.SetActive (false);
		AppMain.Instance.HomeWindow.BeginMission ();
	}

	public void ShowHome()
	{
	}

	public void StartAgain()
	{
		this.gameObject.SetActive (false);
		AppMain.Instance.HomeWindow.gameController.BeginMission ();
	}
}
