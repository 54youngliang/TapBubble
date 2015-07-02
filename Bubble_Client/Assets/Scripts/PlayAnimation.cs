using UnityEngine;
using System.Collections;

public class PlayAnimation : MonoBehaviour {

	public UISprite uiSprite;

	bool hasStarted = false;

	// Use this for initialization
	void Start () {
		StartAnimation ();
	}

	int nowPic = 1;
	public void StartAnimation(){
		if (!hasStarted) {
			nowPic =1;
			hasStarted = true;
			InvokeRepeating ("refresh", 0, 0.06f);
		}
	}

	public void refresh(){
		if (nowPic > 40) {
			nowPic=1;
		}
		uiSprite.spriteName = "play"+nowPic;
		nowPic += 1;
	}

	public void StopAnimation(){
		uiSprite.spriteName = "play" + 1;
		hasStarted = false;
		CancelInvoke ("refresh");

	}

	private int disNum = 1;
	public void StartDisapear(){
		disNum = 1;
		InvokeRepeating ("RefreshDisapear", 0, 0.1f);
	}
	
	private void RefreshDisapear(){
		if(disNum>14){
			CancelInvoke("RefreshDisapear");
		}
		uiSprite.spriteName = "disappear" + disNum;
		disNum += 1;
	}

}
