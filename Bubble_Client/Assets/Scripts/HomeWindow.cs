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

	private Vector3 playButtonOriginScale ;
	private bool buttonHasScaled=false;
	private bool inGame;


	private Vector3 missionTitleVector=new Vector3 (-110f,50f,0f) ;
	private Vector3 backgroundVector;

	void Start () {
		//PlayerPrefs.DeleteAll ();
		int aa = AppMain.Instance.HasHelp;
		if (AppMain.Instance.HasHelp <= 0) {
			helpButton.UpdateInfoStaus (true);
			AppMain.Instance.HasHelp=1;
		} else {
			helpButton.UpdateInfoStaus (false);
		}

		playButton.playAnimation.StartAnimation ();
		//background.GetComponent<GestureEvent>().OnLeft = ShowLevelWindow;
		playButtonOriginScale = playButton.transform.localScale;
		AppMain.Instance.CurrentLevel = AppMain.Instance.MaxLevel;
		MissionTitle.GetComponent<UILabel>().text = AppMain.Instance.MaxLevel+"";
		backgroundVector = background.transform.localPosition;
	}

	public void ShowHomeWindow()
	{

		playButton.playAnimation.StartAnimation ();
		playButton.playLabel.enabled = true;
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
		playButton.playAnimation.StopAnimation ();
		UpdatePauseWindow (false);
		playButton.GetComponent<UIButton> ().enabled = false;
		buttomButtons.SetActive (false);
		countDown.SetActive (true);
		if (background.transform.localPosition == backgroundVector) {
			backgroundVector = background.transform.localPosition;
			TweenY tween = TweenY.Add (background, 1f, 300f);
			//-138,74
			TweenXY titleTween = TweenXY.Add (MissionTitle, 1f, new Vector2 (-138f, 480f));
			tween.OnComplete += BeginMission;
		} else {
			if(MissionTitle.transform.localPosition == missionTitleVector){
				TweenXY titleTween = TweenXY.Add (MissionTitle, 1f, new Vector2 (-138f, 480f));
			}
			BeginMission();
		}

		mapGameObject.SetActive (false);
	}

	public void BeginMission()
	{
		playButton.playLabel.enabled = false;
		MissionTitle.SetActive (true);
		MissionTitle.GetComponent<UILabel> ().text = AppMain.Instance.CurrentLevel + "";
		UpdateGameOverWindowStatus (false);
		UpdatePauseWindow (false);
		UpdateShowLevelComplete (false);
		inGame = true;
		if (buttonHasScaled) {
			gameController.BeginMission ();
		} else {
			PlayButtonDisappearAndBeginMission();
//			TweenSXY s = TweenSXY.Add(playButton, 0.5f, Vector2.zero);
//			s.OnComplete+=HiddenPlayButton;
//			s.OnComplete+=gameController.BeginMission;
			buttonHasScaled = true;
		}

	}

	public void PlayButtonDisappearAndBeginMission(){
		playButton.playAnimation.StartDisapear ();
		gameController.BeginMission ();
		HiddenPlayButton ();

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

	private void HiddenPlayButton(){
		//playButton.SetActive (false);
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
