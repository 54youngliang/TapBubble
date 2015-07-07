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
	private AudioController _AudioController;
	public AudioController AudioController
	{
		get
		{
			return _AudioController;
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

	[SerializeField]
	private GameOverWindow _GameOverWindow;
	public GameOverWindow GameOverWindow
	{
		get
		{
			return _GameOverWindow;
		}
	}

	[SerializeField]
	private LevelPassedWindow _LevelPassedWindow;
	public LevelPassedWindow levelPassedWindow
	{
		get
		{
			return _LevelPassedWindow;
		}
	}
	[SerializeField]
	private PauseWindow _pauseWindow;
	public PauseWindow pauseWindow
	{
		get
		{
			return _pauseWindow;
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


	public int CurrentLevel
	{
		get
		{
			return PlayerPrefs.GetInt("current_level", 1);
		}
		set
		{
			PlayerPrefs.SetInt("current_level", value);
		}
	}

	public int HasHelp
	{
		get
		{
			return PlayerPrefs.GetInt("help");
		}
		set
		{
			PlayerPrefs.SetInt("help", 1);
		}
	}

	void OnApplicationPause(bool isPause)
	{
		//TODO
		if(isPause)
		{
			//pauseWindow.Show(CurrentLevel);
			//pauseWindow.gameObject.SetActive(true);
		}
		else
		{
			//pauseWindow.Show(CurrentLevel);
			//pauseWindow.gameObject.SetActive(true);
		}
	}

	public bool IsDay(){
		int hour = System.DateTime.Now.Hour;
		return false;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
