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
		InGame = false;
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
			return level >=50 ? 50:level;
		}
	}

	public void SetStar(int level, int star)
	{
		PlayerPrefs.SetInt("star_level_" + level, star);
	}

	public int GetStar(int level)
	{
		return PlayerPrefs.GetInt("star_level_" + level, 0);
	}


	public int CurrentLevel
	{
		get
		{
			return PlayerPrefs.GetInt("current_level", 1);
		}
		set
		{
			PlayerPrefs.SetInt("current_level", value >=50 ? 50:value);
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

	public bool HasMusic(){
		int musicStatus = PlayerPrefs.GetInt ("music");
		if (musicStatus == 0) {
			PlayerPrefs.SetInt("music",1);
		}
		return musicStatus == 1;
	}

	public void MusicUpdate(bool music){
		int musicStatus = -1;
		if (music) {
			musicStatus =1;
		}
		PlayerPrefs.SetInt ("music", musicStatus);
	}

	public static string KEY_MAX_STAR_REWARD = "max_star_reward";

	public void SetBool(string key,bool value){
		int intValue = value ? 1 : -1;
		PlayerPrefs.SetInt(key,intValue);
	}

	public bool GetValue(string key){
		int value = PlayerPrefs.GetInt (key);
		if (value == 0) {
			PlayerPrefs.SetInt(key,-1);
		}
		return value == 1;
	}

	public bool InGame
	{
		get
		{
			return PlayerPrefs.GetInt("InGame") > 0;
		}
		
		set
		{
			PlayerPrefs.SetInt("InGame",value ? 1 : 0);
		}
		
	}

	public int GetStarsTotal(){
		int total = 0;
		foreach(MissionMeta meta in MissionConfig.GetAllMissionMeta()){
			int missionId = meta.missionId;
			total += GetStar(missionId);
		}
		return total;
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
		if (hour < 6 || hour > 18) {
			return false;
		}
		return false;

	}

	void Start(){
		AppMain.Instance.AudioController.PlayBgm ();
	}

	// Update is called once per frame
	void Update () {
	
	}
}
