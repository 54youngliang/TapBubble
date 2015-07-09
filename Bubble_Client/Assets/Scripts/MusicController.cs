using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {

	UISprite uiSprite;

	void Start(){
		uiSprite = this.gameObject.GetComponent<UISprite> ();
		UpdateSprite ();
	}

	private void UpdateSprite(){
		bool music = AppMain.Instance.Music;
		if (music) {
			uiSprite.spriteName="music";

		} else {
			uiSprite.spriteName="no_music";
		}
		if (AppMain.Instance.IsDay()) {
			uiSprite.spriteName += "_d";
		} else {
			uiSprite.spriteName += "_n";
		}
		this.gameObject.GetComponent<UIButton> ().normalSprite = uiSprite.spriteName;
	}

	public void Click()
	{
		bool music = AppMain.Instance.Music;
		if (music) {
			// tingzhi yinyue
			AppMain.Instance.AudioController.StopAll();
			AppMain.Instance.Music = false;
		} else {
			AppMain.Instance.Music = true;
			AppMain.Instance.AudioController.StartAll();
		}
		UpdateSprite ();

	}
}
