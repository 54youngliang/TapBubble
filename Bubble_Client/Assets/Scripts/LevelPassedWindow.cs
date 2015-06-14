using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelPassedWindow : MonoBehaviour {

	public List<UISprite> StarList;

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
	}

	public void ShowHome()
	{
	}

	public void StartAgain()
	{
	}
}
