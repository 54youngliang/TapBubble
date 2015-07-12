using UnityEngine;
using System.Collections;

public class DayNight : MonoBehaviour {

	UISprite uiSprite;
	UITexture uiTexture;
	UIButton uiButton;


	void Start(){
		uiSprite = this.gameObject.GetComponent<UISprite> ();
		bool isDay = AppMain.Instance.IsDay ();
		if(null != uiSprite && !isDay){
			string spriteName = uiSprite.spriteName;
			uiSprite.spriteName=spriteName.Replace("_d","_n");
		}
		uiTexture = this.gameObject.GetComponent<UITexture> ();
		if(null != uiTexture && !isDay){
			Texture texture = uiTexture.mainTexture;
			Texture newTexture = Resources.Load("Textures/"+texture.name.Replace("_d","_n")) as Texture;
			uiTexture.mainTexture = newTexture;
		}
		uiButton = this.gameObject.GetComponent<UIButton> ();
		if (null != uiButton && !isDay && null != uiButton.normalSprite) {
			Debug.Log("kkkkk"+uiButton.name);
			uiButton.normalSprite=uiButton.normalSprite.Replace("_d","_n");
		}
	}


}
