using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelView : MonoBehaviour {

	public UILabel LabelLevel;
	public UISprite BackSprite;
	public System.Action<int> OnItemClick;
	public GameObject Sheep;
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
				string _spriteName = "";
				if(value == 1){
					_spriteName="star_1";
				}else if (value == 2){
					_spriteName="star_2";
				}else if (value == 3){
					_spriteName="star_3";
				}else{
					_spriteName="star_0";
				}
				BackSprite.spriteName=_spriteName;
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
