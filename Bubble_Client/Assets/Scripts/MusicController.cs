using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {

	UISprite uiSprite;

	void Start(){
		uiSprite = this.gameObject.GetComponent<UISprite> ();
		UpdateSprite ();
	}

	public void UpdateSprite(){
		bool music = AppMain.Instance.HasMusic();
		string spriteName = "";
		if (music) {
			spriteName="music";
		} else {
			spriteName="no_music";
		}
		if (AppMain.Instance.IsDay()) {
			spriteName += "_d";
		} else {
			spriteName += "_n";
		}
		uiSprite.spriteName = spriteName;
		Debug.Log ("Music ===" + spriteName);
		this.gameObject.GetComponent<UIButton> ().normalSprite = uiSprite.spriteName;
	}

	public void Click()
	{
		bool music = AppMain.Instance.HasMusic();
		if (music) {
			// tingzhi yinyue
			AppMain.Instance.AudioController.StopAll();
			AppMain.Instance.MusicUpdate(false);
		} else {
			AppMain.Instance.MusicUpdate(true);
			AppMain.Instance.AudioController.StartAll();
		}
		UpdateSprite ();

	}
}
