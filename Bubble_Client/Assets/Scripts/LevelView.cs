using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelView : MonoBehaviour {

	public UILabel LabelLevel;
	public List<GameObject> StarList;
	public System.Action<int> OnItemClick;
	// Use this for initialization
	void Start () {
	
	}

	void OnEnable()
	{
		Star = AppMain.Instance.GetStar(Level);
		UpdateLevelStatus();
	}

	private int _level;
	public int Level
	{
		get
		{
			return _level;
		}
		set
		{
			_level = value;
			LabelLevel.text = "" + value;
			UpdateLevelStatus();
		}
	}

	private void UpdateLevelStatus()
	{
		int maxLevel = AppMain.Instance.MaxLevel;
		//TODO
	}

	private int _star;
	public int Star
	{
		get
		{
			return _star;
		}
		set
		{
			if (_star != value)
			{
				_star = value;
				for(int i = 0; i < StarList.Count; i ++)
				{
					StarList[i].SetActive(i < value);
				}
			}
		}
	}

	void OnClick()
	{
		int maxLevel = AppMain.Instance.MaxLevel;
		if (Star > 0 || maxLevel == Level)
		{
			if (OnItemClick != null)
			{
				OnItemClick(Level);
			}
		}
		else
		{
			Debug.Log("no click:" + Level);
		}
	}

	public void UpdateView()
	{
		Star = PlayerPrefs.GetInt("star_level_", -1);
	}
}
