using UnityEngine;
using System.Collections;

public class PauseWindow : MonoBehaviour {

	public UILabel LevelLabel;

	public void Show(int level)
	{
		LevelLabel.text = level.ToString();
	}

	public void ShowHome()
	{
	}
	
	public void Goon()
	{
	}
}
