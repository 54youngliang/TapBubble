using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {


	public GameObject bubblePrefab;
	public GameObject countDown;
	public UILabel missionTitle;
	public GameObject clickReward;
	private List<Bubble> bubbleList = new List<Bubble>();

	
	int restGameTime;
	MissionMeta missionMeta;

	public void BeginMission()
	{

		int missionId = AppMain.Instance.CurrentLevel;
		this.missionMeta = MissionConfig.getMissionMeta (missionId);
		AppearBubbles (missionMeta);
		countDown.SetActive (true);
		restGameTime = missionMeta.time;
		RefreshCountDownTime ();
		InvokeRepeating ("RefreshCountDownTime", 1,1);


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

	private void AppearBubbles(MissionMeta missionMeta)
	{
		List<BubbleInit> bubbleInitList = missionMeta.randomBubbleInit ();
		
		foreach (BubbleInit bubbleInit in bubbleInitList) {
			// init bubble
			GameObject gameObject = GameObjectUtil.CloneGameObjectWithScale(bubblePrefab,this.transform,new Vector3(1.5f,1.5f,1.5f));
			Bubble bubble = gameObject.GetComponent<Bubble>();
			bubble.AppearNum(bubbleInit);
		//	TweenXY.Add(gameObject,1f,bubbleInit.localPosition);
			bubbleList.Add(bubble);
			gameObject.GetComponent<TouchEventListener>().onClick=ButtonClick;
			gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-10,10),Random.Range(-10,10),0f));
		}
	}

	private void ButtonClick(GameObject gameObject,Vector2 vector2){
		Debug.Log ("ButtonClick");
		Bubble bubble = gameObject.GetComponent<Bubble> ();

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
		AppMain.Instance.SetStar (missionMeta.missionId, star);
		if (AppMain.Instance.CurrentLevel > AppMain.Instance.MaxLevel) 
		{
			AppMain.Instance.MaxLevel =AppMain.Instance.CurrentLevel;
		}
		AppMain.Instance.levelPassedWindow.Show (AppMain.Instance.CurrentLevel, star);


	}

	private void MissionFailed()
	{
		CancelInvoke ("RefreshCountDownTime");
		DestoryAllBubble();
		countDown.SetActive (false);
		AppMain.Instance.HomeWindow.MissionFailed();
		bubbleList.Clear ();
	}

	private void DestoryAllBubble(){
		foreach (Bubble bubble in bubbleList) {
			bubble.BeginDestory();
		}
	}

}
