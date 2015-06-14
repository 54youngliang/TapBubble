using UnityEngine;
using System.Collections;

public class AppMain : MonoBehaviour {

	[SerializeField]
	private HomeWindow _HomeWindow;

	public HomeWindow HomeWindow
	{
		get
		{
			return _HomeWindow;
		}

	}


	[SerializeField]
	private LevelWindow _LevelWindow;

	public LevelWindow LevelWindow
	{
		get
		{
			return _LevelWindow;
		}
	}

	//[SerializeField]


	private static AppMain _instance;
	public static AppMain Instance
	{
		get
		{
			return _instance;
		}
	}

	private UIRoot _uiRoot;
	public UIRoot uiRoot
	{
		get
		{
			return _uiRoot;
		}
	}
	// Use this for initialization
	void Awake () {
		_instance = this;
		_uiRoot = this.GetComponent<UIRoot>();
	}


	public int MaxLevel
	{
		set
		{
			PlayerPrefs.SetInt("MaxLevel", value);
		}
		get
		{
			var level = PlayerPrefs.GetInt("MaxLevel",1);
			return level;
		}
	}

	public void SetStar(int level, int star)
	{
		PlayerPrefs.SetInt("star_level_" + level, star);
	}

	public int GetStar(int level)
	{
		return PlayerPrefs.GetInt("star_level_" + level, -1);
	}



	// Update is called once per frame
	void Update () {
	
	}
}
