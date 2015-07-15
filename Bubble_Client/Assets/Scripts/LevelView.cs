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
		_star = AppMain.Instance.GetStar(MissionId);
		UpdateLevelStatus();
	}

	public void BeginMission(){

		AppMain.Instance.CurrentLevel = _missionId;
		this.gameObject.GetComponent<UIButton> ().enabled = false;

		AppMain.Instance.LevelWindow.ShowHome ();
		AppMain.Instance.HomeWindow.FirstBeginMission ();
		this.gameObject.GetComponent<UIButton> ().enabled = true;
	}

	private int _missionId;
	public int MissionId
	{
		get
		{
			return _missionId;
		}
		set
		{
			_missionId = value;
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

	public void UpdateStar(int star){
		_star = star;
		string _spriteName = "";
		if(star == 1){
			_spriteName="star_1";
		}else if (star == 2){
			_spriteName="star_2";
		}else if (star == 3){
			_spriteName="star_3";
		}else{
			_spriteName="star_0";
		}
		BackSprite.spriteName=_spriteName;
	}

	void OnClick()
	{
		int maxLevel = AppMain.Instance.MaxLevel;
		if (_star > 0 || maxLevel == MissionId)
		{
			if (OnItemClick != null)
			{
				OnItemClick(MissionId);
			}
		}
		else
		{
			Debug.Log("no click:" + MissionId);
		}
	}

	public void UpdateView()
	{
		_star = PlayerPrefs.GetInt("star_level_", -1);
	}
}
