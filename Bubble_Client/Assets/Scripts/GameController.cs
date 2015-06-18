using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	public HomeWindow homeWindow;
	public GameObject bubblePrefab;
	public GameObject countDown;
	private List<Bubble> bubbleList = new List<Bubble>();

	private MissionConfig missionConfig;
	
	int restGameTime;

	public void BeginMission()
	{
		int missionId = AppMain.Instance.MaxLevel + 1;
		BeginMission (missionId);
	}

	public void BeginMission(int missionId)
	{
		MissionConfig missionConfig = getMissionConfig (missionId);
		this.missionConfig = missionConfig;
		AppearBubbles ();
		countDown.SetActive (true);
		restGameTime = missionConfig.gameTime;
		RefreshCountDownTime ();
		InvokeRepeating ("RefreshCountDownTime", 1,1);
	}


	private void RefreshCountDownTime()
	{
		UILabel uiLabel = countDown.GetComponent<UILabel> ();
		uiLabel.text = restGameTime + "";
		restGameTime -= 1;

	}

	private void AppearBubbles()
	{
		List<BubbleInit> bubbleInitList = missionConfig.randomBubbleList ();
		
		foreach (BubbleInit bubbleInit in bubbleInitList) {
			// init bubble
			GameObject gameObject = GameObjectUtil.CloneGameObjectWithScale(bubblePrefab,this.transform,bubbleInit.localScale);
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

		if (bubble == bubbleList [0]) {
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

	private void MissionComplete()
	{
		Debug.Log ("Mission Complete:" + missionConfig.id);
		CancelInvoke ("RefreshCountDownTime");
		countDown.SetActive (false);
		homeWindow.MissionComplete (AppMain.Instance.MaxLevel,3);
		AppMain.Instance.SetStar (missionConfig.id, 3);
		AppMain.Instance.MaxLevel =missionConfig.id;
	}

	private void MissionFailed()
	{
		CancelInvoke ("RefreshCountDownTime");
		DestoryAllBubble();
		countDown.SetActive (false);
		homeWindow.MissionFailed();
		bubbleList.Clear ();
	}

	private void DestoryAllBubble(){
		foreach (Bubble bubble in bubbleList) {
			bubble.BeginDestory();
		}
	}

	private MissionConfig getMissionConfig(int mission)
	{
		MissionConfig config = new MissionConfig ();
		config.id = mission;
		return config;
	}

}
