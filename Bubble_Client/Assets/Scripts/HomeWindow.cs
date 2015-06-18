using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HomeWindow : MonoBehaviour {

	[SerializeField]
	public GameObject background;
	public UIButton playButton;
	public GameController gameController;
	public GameObject countDown;

	private Vector3 playButtonOriginScale ;
	private bool backgroundHasMoved=false;
	private bool buttonHasScaled=false;
	private bool inGame;
	
	void Start () {
		background.GetComponent<GestureEvent>().OnLeft = ShowLevelWindow;
		playButtonOriginScale = playButton.gameObject.transform.localScale;
	}

	public void ShowHomeWindow()
	{
		UpdateGameOverWindowStatus (false);
		UpdatePauseWindow (false);
		UpdateShowLevelComplete (false);
		playButton.gameObject.transform.localScale = playButtonOriginScale;
		buttonHasScaled = false;
		playButton.enabled = true;
		playButton.gameObject.SetActive (true);
	}

	public void FirstBeginMission()
	{
		UpdatePauseWindow (false);
		TweenY tween = TweenY.Add (background, 1f, 300f);
		tween.OnComplete += BeginMission;
		backgroundHasMoved = true;
	}

	public void BeginMission()
	{
		UpdateGameOverWindowStatus (false);
		UpdatePauseWindow (false);
		UpdateShowLevelComplete (false);
		inGame = true;
		countDown.SetActive (true);
		if (buttonHasScaled) {
			gameController.BeginMission ();
		} else {
			TweenScale scale = TweenScale.Begin (playButton.gameObject, 1f, Vector3.zero);
			playButton.enabled = false;
			scale.AddOnFinished(HiddenPlayButton);
			scale.AddOnFinished(gameController.BeginMission);
			buttonHasScaled = true;
		}
	}

	public void MissionFailed()
	{
		UpdateGameOverWindowStatus (true);
		inGame = false;
	}
	
	public void MissionComplete(int level,int star)
	{
		AppMain.Instance.levelPassedWindow.gameObject.SetActive (true);
		LevelPassedWindow window = AppMain.Instance.levelPassedWindow.GetComponent<LevelPassedWindow> ();
		window.Show (level, star);
		inGame = false;
	}

	private void HiddenPlayButton(){
		playButton.gameObject.SetActive (false);
	}

	private void ShowLevelWindow()
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
