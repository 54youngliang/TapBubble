using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {


	public GameObject bubblePrefab;
	public GameObject timeSheepPrefab;
	public GameObject countDown;
	public UILabel missionTitle;
	public GameObject clickReward;
	public GameObject playButton;
	public GameObject nextPlayButton;
	private List<Bubble> bubbleList = new List<Bubble>();
	public GameObject musicButton;
	public GameObject homeButton;
	public UIAtlas countDayAtlas;
	public UIAtlas countNightAtlas;


	float gameTime;
	MissionMeta missionMeta;
	int timeSheepNum;
	UISprite countSprite ;
	UIAtlas uiAtlas;
	GameObject timeSheep;

	void Start(){
		gameTime += Time.deltaTime;
		countSprite = countDown.GetComponent<UISprite> ();
		uiAtlas = countSprite.atlas;
	}

	void Update(){
		if (AppMain.Instance.InGame) {
			gameTime += Time.deltaTime;
			RefreshCountSprite ();
			int restGameTime =(int)( missionMeta.time - gameTime);
			if (restGameTime == 5) {
				AppMain.Instance.AudioController.PlayCountDown();
			}
			ApprearSheep();
			if (gameTime >= missionMeta.time) {
				MissionFailed();
			}
		}
	}

	private void RefreshCountSprite(){
		bool isDay = AppMain.Instance.IsDay ();
		string spriteName = "";
		if (isDay) {
			if (null == countSprite.atlas || !countSprite.atlas.name.Equals("count_d")) {
				//Resources.UnloadAsset(uiAtlas.gameObject);
				uiAtlas = (Resources.Load ("Atlas/count_d") as GameObject).GetComponent<UIAtlas>();
				countSprite.atlas = uiAtlas;
			}	
			spriteName="sun";
		} else {
			if (null == countSprite.atlas || !countSprite.atlas.name.Equals("count_n")) {
			//	Resources.UnloadAsset(uiAtlas.gameObject);
				uiAtlas = (Resources.Load ("Atlas/count_n") as GameObject).GetComponent<UIAtlas>();
				countSprite.atlas = uiAtlas;
			}	
			spriteName="moon";
		}
		double totalTime = missionMeta.time *1.0d;
		double framePerTime = totalTime / 31d;// mei zhen de shi jian
		int sprite = (int)(gameTime / framePerTime) +1;
		if (sprite >= 10) {
			spriteName += ("00" + sprite);
		} else {
			spriteName +=("000"+sprite);
		}
		countSprite.spriteName = spriteName;
	}

	public void AddRestTime(int time){
		this.gameTime -= time;
	}

	public void BeginMission()
	{
		gameTime = 0;
		AppMain.Instance.InGame = true;
		AppMain.Instance.AudioController.StartBgmGame ();
		musicButton.SetActive (true);
		homeButton.SetActive (true);
		countDown.SetActive (true);
		missionTitle.gameObject.SetActive (true);
		int missionId = AppMain.Instance.CurrentLevel;
		this.missionMeta = MissionConfig.getMissionMeta (missionId);
		AppearBubbles (missionMeta, playButton.transform.localPosition);
		countDown.SetActive (true);

	}

	private void ApprearSheep()
	{
		if (timeSheepNum < 1 && missionMeta.missionId >=10 && (int)gameTime >=10) {
			int randomNum = Random.Range (0, 100);
			if(randomNum<10){
				timeSheep = GameObjectUtil.CloneGameObjectWithScale(timeSheepPrefab,this.transform,new Vector3(1.0f,1.0f,1.0f));
				timeSheep.transform.localPosition = new Vector3(-470,Random.Range(-200,200),0);
				Vector2 targetPosition = new Vector2(450,Random.Range(-350,350));
				TimeSheep sheepCon = timeSheep.GetComponent<TimeSheep>();
				TweenXY.Add(timeSheep,6,targetPosition).OnComplete +=sheepCon.DestorySheep;
				timeSheepNum+=1;
			}
		}
	}

	private void AppearBubbles(MissionMeta missionMeta,Vector3 position)
	{
		List<BubbleInit> bubbleInitList = missionMeta.randomBubbleInit ();
		
		foreach (BubbleInit bubbleInit in bubbleInitList) {
			// init bubble
			Debug.Log("Bubbles "+bubbleInit.result+",view:"+bubbleInit.view);
			Vector3 vector = new Vector3(-113,31,0);
			GameObject gameObject = GameObjectUtil.CloneGameObjectWithScale(bubblePrefab,this.transform,new Vector3(1.0f,1.0f,1.0f));
			Vector3 newPosition = new Vector3(position.x+Random.Range(-20,20),position.y+Random.Range(-20,20),0);
			gameObject.transform.localPosition=newPosition;
			gameObject.transform.localPosition=vector;
			Bubble bubble = gameObject.GetComponent<Bubble>();
			bubble.InitSprite();
			bubble.AppearNum(bubbleInit);
		//	TweenXY.Add(gameObject,1f,bubbleInit.localPosition);
			bubbleList.Add(bubble);
			gameObject.GetComponent<TouchEventListener>().onClick=ButtonClick;
			gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-5,8),Random.Range(-5,5),0f));
		}
	}

	private void ButtonClick(GameObject gameObject,Vector2 vector2){
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

	private void MissionComplete()
	{
		EndGame ();
		AppMain.Instance.HomeWindow.MissionComplete (AppMain.Instance.MaxLevel,3);

		int restGameTime =(int)( missionMeta.time - gameTime);
		int star = 0;
		if (restGameTime >= missionMeta.level3) {
			star = 3;
		} else if (restGameTime >= missionMeta.level2) {
			star = 2;
		} else if (restGameTime >= missionMeta.level1) {
			star = 1;
		} 
	
		AppMain.Instance.levelPassedWindow.Show (missionMeta.missionId, star);
		int starAlready = AppMain.Instance.GetStar(missionMeta.missionId);
		if (starAlready < star) {
			AppMain.Instance.SetStar (missionMeta.missionId, star);
		}
		AppMain.Instance.CurrentLevel += 1;
		if (AppMain.Instance.CurrentLevel > AppMain.Instance.MaxLevel) 
		{
			AppMain.Instance.MaxLevel =AppMain.Instance.CurrentLevel;
		}


	}

	private void MissionFailed()
	{
		EndGame ();
		AppMain.Instance.HomeWindow.MissionFailed();
	}

	private void EndGame(){

		AppMain.Instance.AudioController.StopCountDown ();
		AppMain.Instance.InGame = false;
		AppMain.Instance.AudioController.StopBgmGame ();
		musicButton.SetActive (false);
		homeButton.SetActive (false);
		countDown.SetActive (false);
		DestoryAllBubble();
		bubbleList.Clear ();
	}

	public void MissionExit(){
		EndGame ();
		AppMain.Instance.HomeWindow.ShowHomeWindow ();
	}
	
	private void DestoryAllBubble(){
		if (null != timeSheep) {
			Destroy(timeSheep);
		}
		foreach (Bubble bubble in bubbleList) {
			bubble.BeginDestory();
		}
	}

}
