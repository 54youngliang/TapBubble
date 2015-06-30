using UnityEngine;
using System.Collections;

public class GameOverWindow : MonoBehaviour {
	
	public UILabel LevelLabel;


	public void ShowHome()
	{
		Debug.Log ("GameOver click home");
		AppMain.Instance.HomeWindow.ShowHomeWindow ();
	}

	public void ShowGameOverWindow()
	{
		LevelLabel.text = AppMain.Instance.CurrentLevel + "";
	}

	public void StartAgain()
	{
		AppMain.Instance.HomeWindow.BeginMission ();
	}

}
