using UnityEngine;
using System.Collections;

public class PauseWindow : MonoBehaviour {

	public UILabel LevelLabel;

	void Start()
	{
		LevelLabel.text = AppMain.Instance.MaxLevel+"";
	}

	public void Show(int level)
	{
		LevelLabel.text = level.ToString();
	}

	public void ShowHome()
	{

	}
	
	public void BeginMission()
	{
		AppMain.Instance.HomeWindow.FirstBeginMission ();
	}
}
