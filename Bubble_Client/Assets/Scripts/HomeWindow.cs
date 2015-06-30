using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HomeWindow : MonoBehaviour {

	[SerializeField]
	public GameObject background;
	public GameObject playButton;
	public GameController gameController;
	public GameObject countDown;
	public GameObject buttomButtons;
	public GameObject mapGameObject;
	public GameObject MissionTitle;

	private Vector3 playButtonOriginScale ;
	private bool backgroundHasMoved=false;
	private bool buttonHasScaled=false;
	private bool inGame;
	
	void Start () {
		PlayerPrefs.DeleteAll ();
		background.GetComponent<GestureEvent>().OnLeft = ShowLevelWindow;
		playButtonOriginScale = playButton.transform.localScale;
		AppMain.Instance.CurrentLevel = AppMain.Instance.MaxLevel;
		MissionTitle.GetComponent<UILabel>().text = AppMain.Instance.MaxLevel+"";
	}

	public void ShowHomeWindow()
	{
		UpdateGameOverWindowStatus (false);
		UpdatePauseWindow (false);
		UpdateShowLevelComplete (false);
		buttomButtons.SetActive (true);
		mapGameObject.SetActive (false);
		MissionTitle.SetActive(true);
		MissionTitle.GetComponent<UILabel> ().text = AppMain.Instance.CurrentLevel + "";
		MissionTitle.transform.position = new Vector3 (-110f,50f,0f);
		mapGameObject.SetActive (true);
		playButton.transform.localScale = playButtonOriginScale;
		buttonHasScaled = false;
		playButton.GetComponent<UIButton>().enabled = true;
		playButton.SetActive (true);
	}

	public void FirstBeginMission()
	{
		UpdatePauseWindow (false);
		TweenY tween = TweenY.Add (background, 1f, 300f);
		//-138,74
		TweenXY titleTween = TweenXY.Add (MissionTitle, 1f, new Vector2 (-138f, 480f));
		tween.OnComplete += BeginMission;
		backgroundHasMoved = true;
		buttomButtons.SetActive (false);
		mapGameObject.SetActive (false);
	}

	public void BeginMission()
	{
		MissionTitle.SetActive (true);
		MissionTitle.GetComponent<UILabel> ().text = AppMain.Instance.CurrentLevel + "";
		UpdateGameOverWindowStatus (false);
		UpdatePauseWindow (false);
		UpdateShowLevelComplete (false);
		inGame = true;
		countDown.SetActive (true);
		if (buttonHasScaled) {
			gameController.BeginMission ();
		} else {
			TweenSXY s = TweenSXY.Add(playButton, 0.5f, Vector2.zero);
			s.OnComplete+=HiddenPlayButton;
			s.OnComplete+=gameController.BeginMission;
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

	private void HiddenPlayButton(){
		playButton.SetActive (false);
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
		TweenX.Add(AppMain.Instance.LevelWindow.gameObject, 0.5f, 0f).OnComplete += ShowLevelWindow;
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
