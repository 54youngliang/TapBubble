using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {


	public GameObject bubblePrefab;
	public GameObject countDown;
	public UILabel missionTitle;
	public GameObject clickReward;
	public GameObject playButton;
	public GameObject nextPlayButton;
	private List<Bubble> bubbleList = new List<Bubble>();
	public GameObject musicButton;
	public GameObject homeButton;

	
	int restGameTime;
	MissionMeta missionMeta;

	public void BeginMission()
	{
		AppMain.Instance.InGame = true;
		Debug.Log (AppMain.Instance.InGame + "-------------");
		AppMain.Instance.AudioController.PlayBgm ();
		musicButton.SetActive (true);
		homeButton.SetActive (true);
		int missionId = AppMain.Instance.CurrentLevel;
		this.missionMeta = MissionConfig.getMissionMeta (missionId);
		AppearBubbles (missionMeta, playButton.transform.localPosition);
		countDown.SetActive (true);
		restGameTime = missionMeta.time;
		RefreshCountDownTime ();
		InvokeRepeating ("RefreshCountDownTime", 1, 1f);

	}

	private void RefreshCountDownTime()
	{
		UILabel uiLabel = countDown.GetComponent<UILabel> ();
		uiLabel.text = restGameTime + "";
		restGameTime -= 1;
		if (restGameTime < 0) {
			MissionFailed();
		}

	}

	private void AppearBubbles(MissionMeta missionMeta,Vector3 position)
	{
		List<BubbleInit> bubbleInitList = missionMeta.randomBubbleInit ();
		
		foreach (BubbleInit bubbleInit in bubbleInitList) {
			// init bubble
			Vector3 vector = new Vector3(-113,31,0);
			GameObject gameObject = GameObjectUtil.CloneGameObjectWithScale(bubblePrefab,this.transform,new Vector3(2.5f,2.5f,2.5f));
			Vector3 newPosition = new Vector3(position.x+Random.Range(-20,20),position.y+Random.Range(-20,20),0);
			gameObject.transform.localPosition=newPosition;
			gameObject.transform.localPosition=vector;
			Bubble bubble = gameObject.GetComponent<Bubble>();
			bubble.AppearNum(bubbleInit);
		//	TweenXY.Add(gameObject,1f,bubbleInit.localPosition);
			bubbleList.Add(bubble);
			gameObject.GetComponent<TouchEventListener>().onClick=ButtonClick;
			gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-5,5),Random.Range(-5,5),0f));
		}
	}

	private void ButtonClick(GameObject gameObject,Vector2 vector2){
		Debug.Log ("ButtonClick");
		Bubble bubble = gameObject.GetComponent<Bubble> ();
		AppMain.Instance.AudioController.PlaySheep ();

		if (bubble.bubbleInit.result == bubbleList [0].bubbleInit.result) {
			bubbleList.Remove (bubble);
			bubble.BeginDestory ();
			if(bubbleList.Count == 0)
			{
				MissionComplete();
			}
		} else {
			MissionFailed();
		}
	}

	private void ClickReward(GameObject gameObject,Vector2 vector2)
	{
		Debug.Log ("click reward");
		this.restGameTime += 10;
		Destroy (gameObject);
	}

	private void MissionComplete()
	{
		AppMain.Instance.InGame = false;
		AppMain.Instance.AudioController.StopBgm ();
		musicButton.SetActive (false);
		homeButton.SetActive (false);
		Debug.Log ("Mission Complete:" + missionMeta.missionId);
		CancelInvoke ("RefreshCountDownTime");
		countDown.SetActive (false);
		AppMain.Instance.HomeWindow.MissionComplete (AppMain.Instance.MaxLevel,3);

		int star = 0;
		if (restGameTime >= missionMeta.level3) {
			star = 3;
		} else if (restGameTime >= missionMeta.level2) {
			star = 2;
		} else if (restGameTime >= missionMeta.level1) {
			star = 1;
		} 
		Debug.Log ("MissionComplete" + missionMeta.missionId + ",star:" + star);
		AppMain.Instance.levelPassedWindow.Show (missionMeta.missionId, star);
		AppMain.Instance.SetStar (missionMeta.missionId, star);
		if (AppMain.Instance.CurrentLevel > AppMain.Instance.MaxLevel) 
		{
			AppMain.Instance.MaxLevel =AppMain.Instance.CurrentLevel;
		}
		AppMain.Instance.CurrentLevel += 1;

	}

	private void MissionFailed()
	{
		AppMain.Instance.InGame = false;
		AppMain.Instance.AudioController.StopBgm ();
		musicButton.SetActive (false);
		homeButton.SetActive (false);
		CancelInvoke ("RefreshCountDownTime");
		DestoryAllBubble();
		countDown.GetComponent<UILabel> ().text = "";
		countDown.SetActive (false);
		AppMain.Instance.HomeWindow.MissionFailed();
		bubbleList.Clear ();
	}

	public void MissionExit(){
		AppMain.Instance.InGame = false;
		AppMain.Instance.AudioController.StopBgm ();
		foreach (Bubble bubble in bubbleList) {
			Destroy(bubble.gameObject);
		}
		CancelInvoke ("RefreshCountDownTime");
		bubbleList.Clear ();
		musicButton.SetActive (false);
		homeButton.SetActive (false);
		countDown.GetComponent<UILabel> ().text = "";
		countDown.SetActive (false);
		AppMain.Instance.HomeWindow.ShowHomeWindow ();
	}

	private void DestoryAllBubble(){
		foreach (Bubble bubble in bubbleList) {
			bubble.BeginDestory();
		}
	}

}
