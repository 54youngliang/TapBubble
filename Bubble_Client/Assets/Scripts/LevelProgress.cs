using UnityEngine;
using System.Collections;

public class LevelProgress : MonoBehaviour {

	public GameObject sheepGameObject;
	public UIAtlas level_progress_receive_tips;
	public UIAtlas level_progress_package;
	public UIAtlas get_packs;
	public UITexture tips;


	UISpriteAnimation spriteAnimation;
	
	void Awake(){
		spriteAnimation = this.gameObject.GetComponentInChildren<UISpriteAnimation> ();

	}

	public void Apprear(){
		int totalStars = AppMain.Instance.GetStarsTotal ();
		if (totalStars == 50) {
			tips.gameObject.SetActive(false);
			if (AppMain.Instance.GetValue (AppMain.KEY_MAX_STAR_REWARD)) {
				spriteAnimation.Pause ();
				UISprite sprite = sheepGameObject.GetComponent<UISprite> ();
				sprite.atlas = get_packs;
				sprite.spriteName = "get packs0040";

			} else {
				UISprite sprite = sheepGameObject.GetComponent<UISprite> ();
				sprite.atlas = level_progress_receive_tips;
				sprite.spriteName="receive_tips0001";
				spriteAnimation.loop = true;
				spriteAnimation.namePrefix="receive_tips000";
				spriteAnimation.Play();
			}
		} else {
			UISprite sprite = sheepGameObject.GetComponent<UISprite> ();
			sprite.atlas = level_progress_package;
			spriteAnimation.loop = true;
			spriteAnimation.namePrefix="package";
			spriteAnimation.Play();
		}
	}

	public void Click(){
		int totalStars = AppMain.Instance.GetStarsTotal ();
		if (totalStars == 50 && !AppMain.Instance.GetValue (AppMain.KEY_MAX_STAR_REWARD)) {
			UISprite sprite = sheepGameObject.GetComponent<UISprite> ();
			sprite.atlas = get_packs;
			spriteAnimation.loop = false;
			spriteAnimation.ResetToBeginning ();
		}
	}


}
