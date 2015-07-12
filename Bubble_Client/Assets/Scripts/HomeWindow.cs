using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HomeWindow : MonoBehaviour {

	[SerializeField]
	public GameObject background;
	public PlayButton playButton;
	public GameController gameController;
	public GameObject countDown;
	public GameObject buttomButtons;
	public GameObject mapGameObject;
	public GameObject MissionTitle;
	public HelpButton helpButton;
	public GameObject MusicButton;
	public GameObject NameLabel;
	public GameObject NextLevelButton;
	public GameObject Title;
	public GameObject Map;

	private Vector3 playButtonOriginScale ;
	private bool buttonHasScaled=false;
	private bool inGame;


	private Vector3 missionTitleVector=new Vector3 (-114f,108f,0f) ;
	private Vector3 backgroundVector;

	void Start () {
		//PlayerPrefs.DeleteAll ();
		if (AppMain.Instance.HasHelp <= 0) {
			DisplayHelp ();
		} else {
			HiddenHelp();
		}
		playButton.playAnimation.StartNormalPlay ();
		//background.GetComponent<GestureEvent>().OnLeft = ShowLevelWindow;
		playButtonOriginScale = playButton.transform.localScale;
		AppMain.Instance.CurrentLevel = AppMain.Instance.MaxLevel;
		MissionTitle.GetComponent<UILabel>().text = AppMain.Instance.MaxLevel+"";
		backgroundVector = background.transform.localPosition;
	}

	bool displayHelpStatus=false;

	public void ChangeHelpStatus(){
		displayHelpStatus = !displayHelpStatus;
		if (displayHelpStatus) {
			DisplayHelp ();
		} else {
			HiddenHelp();
		}
	}

	public void DisplayHelp(){
		Title.SetActive (false);
		Map.SetActive (false);
		helpButton.UpdateInfoStaus (true);
		AppMain.Instance.HasHelp=1;
		MusicButton.SetActive (false);
		NameLabel.SetActive (false);
		displayHelpStatus = true;
	}

	public void HiddenHelp(){
		Title.SetActive (true);
		Map.SetActive (true);
		helpButton.UpdateInfoStaus (false);
		MusicButton.SetActive (true);
		NameLabel.SetActive (true);
		displayHelpStatus = false;
	}

	public void ShowHomeWindow()
	{
		needHiddenPlayButton = false;
		MusicButton.gameObject.GetComponent<MusicController> ().UpdateSprite ();
		HiddenHelp ();
		playButton.playAnimation.StartNormalPlay ();
		playButton.playLabel.enabled = true;
		playButton.gameObject.SetActive (true);
		NextLevelButton.SetActive (false);
		UpdateGameOverWindowStatus (false);
		UpdatePauseWindow (false);
		UpdateShowLevelComplete (false);
		buttomButtons.SetActive (true);
		mapGameObject.SetActive (false);
		MissionTitle.SetActive(true);
		MissionTitle.GetComponent<UILabel> ().text = AppMain.Instance.CurrentLevel + "";
		MissionTitle.transform.localPosition = missionTitleVector;
		mapGameObject.SetActive (true);
		playButton.transform.localScale = playButtonOriginScale;
		buttonHasScaled = false;
		playButton.GetComponent<UIButton>().enabled = true;
		playButton.gameObject.SetActive (true);
		background.transform.localPosition = backgroundVector;
	}

	public void FirstBeginMission()
	{
		AppMain.Instance.AudioController.PlaySheep ();
		playButton.playAnimation.StopNormalPlay ();
		UpdatePauseWindow (false);
		playButton.GetComponent<UIButton> ().enabled = false;
		buttomButtons.SetActive (false);
		countDown.SetActive (true);
		if (background.transform.localPosition == backgroundVector) {
			backgroundVector = background.transform.localPosition;
			TweenY tween = TweenY.Add (background, 1f, 300f);
			//-138,74
			TweenXY.Add (MissionTitle, 1f, new Vector2 (-138f, 532.57f));
			tween.OnComplete += BeginMission;
		} else {
			if(MissionTitle.transform.localPosition == missionTitleVector){
				TweenXY.Add (MissionTitle, 1f, new Vector2 (-138f, 480f));
			}
			BeginMission();
		}

		mapGameObject.SetActive (false);
	}

	public void BeginMission()
	{
		AppMain.Instance.AudioController.PlaySheep ();
		playButton.playLabel.enabled = false;
		MissionTitle.SetActive (true);
		MissionTitle.GetComponent<UILabel> ().text = AppMain.Instance.CurrentLevel + "";
		UpdateGameOverWindowStatus (false);
		UpdatePauseWindow (false);
		UpdateShowLevelComplete (false);
		AppMain.Instance.levelPassedWindow.gameObject.SetActive (false);
		inGame = true;
		if (buttonHasScaled) {
			NextLevelButton.GetComponent<NextButton>().label.SetActive(false);
			NextLevelButton.GetComponent<PlayAnimation>().StartDisapear();
			gameController.BeginMission();
			needHiddenNextButton=true;
		} else {
			playButton.playAnimation.StartDisapear ();
			gameController.BeginMission ();
			needHiddenPlayButton = true;
			buttonHasScaled = true;
		}

	}

	public void MissionFailed()
	{
		MissionTitle.SetActive(false);
		UpdateGameOverWindowStatus (true);
		inGame = false;
		AppMain.Instance.GameOverWindow.ShowGameOverWindow ();
	}
	
	public void MissionComplete(int level,int star)
	{
		MissionTitle.SetActive(false);
		AppMain.Instance.levelPassedWindow.gameObject.SetActive (true);
		LevelPassedWindow window = AppMain.Instance.levelPassedWindow.GetComponent<LevelPassedWindow> ();
		window.Show (level, star);
		inGame = false;
	}

	// yincang plays
	public bool needHiddenPlayButton=false;
	private void HiddenPlayButton(){
		if (needHiddenPlayButton) {
			if(!playButton.playAnimation.IsPlaying){
				playButton.gameObject.SetActive (false);
				needHiddenPlayButton=false;
			}
		}

	}

	// yincang next
	public bool needHiddenNextButton=false;
	private void HiddenNextButton(){
		if (needHiddenNextButton) {
			if(!NextLevelButton.GetComponent<PlayAnimation>().IsPlaying){
				NextLevelButton.gameObject.SetActive (false);
				needHiddenPlayButton=false;
			}
		}
	}

	void Update(){
		if (needHiddenPlayButton) {
			HiddenPlayButton();
		}
		if (needHiddenNextButton) {
			HiddenNextButton();
		}
	}

	public void ShowLevelWindow()
	{
		if (inGame) 
		{
			return;
		}
		var width = AppMain.Instance.uiRoot.manualWidth;
		AppMain.Instance.LevelWindow.transform.localPosition = new Vector3(width, 0, 0);
		AppMain.Instance.LevelWindow.gameObject.SetActive(true);
		TweenX.Add(AppMain.Instance.LevelWindow.gameObject, 0.5f, 0f);
		TweenX.Add(this.gameObject, 0.5f, -width);
	}

	private void UpdateGameOverWindowStatus(bool active)
	{
		AppMain.Instance.GameOverWindow.gameObject.SetActive (active);
	}
	
	private void UpdatePauseWindow(bool active)
	{
		AppMain.Instance.pauseWindow.gameObject.SetActive (active);
	}
	
	private void UpdateShowLevelComplete(bool active)
	{
		AppMain.Instance.LevelWindow.gameObject.SetActive (active);
	}
}
