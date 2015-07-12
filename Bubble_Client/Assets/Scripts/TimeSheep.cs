using UnityEngine;
using System.Collections;

public class TimeSheep : MonoBehaviour {

	public UIAtlas timeSheepDay;
	public UIAtlas timeSheepNight;

	GameController gameController;
	UISprite sheepSprite;

	void Start(){
		sheepSprite = this.gameObject.GetComponent<UISprite> ();
		gameController = AppMain.Instance.HomeWindow.gameController;
		this.gameObject.GetComponent<TouchEventListener>().onClick=TimeSheepClick;
		StartMove (new Vector2(0,0), new Vector2(0,0));
	}

	public void DestorySheep(){
		Destroy (this.gameObject);
	}

	int movePicNum = 1;
	public void StartMove(Vector2 start,Vector2 end){
		movePicNum = 1;
		InvokeRepeating ("RefreshMove", 0, 0.05f);
	}

	public void StopMove(){
		TweenXY tween = this.gameObject.GetComponent<TweenXY> ();
		tween.enabled = false;
		CancelInvoke ("RefreshMove");
	}

	private void RefreshMove(){
		if (movePicNum > 4) {
			movePicNum=1;
		}
		bool isDay = AppMain.Instance.IsDay ();
		string spriteName = "";
		if (isDay) {
			if (sheepSprite.atlas != timeSheepDay) {
				sheepSprite.atlas = timeSheepDay;
			}
			spriteName="move1_d";
		} else {
			if (sheepSprite.atlas != timeSheepNight) {
				sheepSprite.atlas = timeSheepNight;
			}
			spriteName="move1_n";
		}
		if (movePicNum >= 10) {
			spriteName += ("00" + movePicNum);
		} else {
			spriteName += ("000" + movePicNum);
		}
		sheepSprite.spriteName = spriteName;
		movePicNum += 1;
	}

	int disPicNum = 1;
	private void StartDis(){
		disPicNum = 1;
		InvokeRepeating ("RefreshDis", 0, 0.05f);
	}

	private void RefreshDis(){
		if (disPicNum > 17) {
			disPicNum=1;
			CancelInvoke("RefreshDis");
			Destroy (this.gameObject);
			gameController.AddRestTime(3);
			return;
		}
		bool isDay = AppMain.Instance.IsDay ();
		string spriteName = "";
		if (isDay) {
			if (sheepSprite.atlas != timeSheepDay) {
				sheepSprite.atlas = timeSheepDay;
			}
			spriteName="+3_d";
		} else {
			if (sheepSprite.atlas != timeSheepNight) {
				sheepSprite.atlas = timeSheepNight;
			}
			spriteName="+3_n";
		}
		if (disPicNum >= 10) {
			spriteName += ("00" + disPicNum);
		} else {
			spriteName += ("000" + disPicNum);
		}
		sheepSprite.spriteName = spriteName;
		disPicNum += 1;
	}


	public void TimeSheepClick(GameObject gameObject,Vector2 vector2){
		StopMove ();
		StartDis ();

	}

}
