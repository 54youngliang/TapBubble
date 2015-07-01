using UnityEngine;
using System.Collections;

public class PlayAnimation : MonoBehaviour {

	public UISprite uiSprite;

	bool hasStarted = false;

	// Use this for initialization
	void Start () {
		StartAnimation ();
	}

	public void StartAnimation(){
		if (!hasStarted) {
			hasStarted = true;
			InvokeRepeating ("refresh", 0, 0.06f);
		}
	}

	int nowPic = 1;

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
	
	public void StartDisapear(){
		InvokeRepeating ("RefreshDisapear", 0, 0.1f);
	}

	private int disNum = 1;
	private void RefreshDisapear(){
		if(disNum>14){
			CancelInvoke("RefreshDisapear");
		}
		uiSprite.spriteName = "disappear" + disNum;
		disNum += 1;
	}

}
