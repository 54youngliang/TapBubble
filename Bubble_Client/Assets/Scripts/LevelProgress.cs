using UnityEngine;
using System.Collections;

public class LevelProgress : MonoBehaviour {

	public GameObject sheepGameObject;
	public UIAtlas level_progress_receive_tips;
	public UIAtlas level_progress_package;
	public UIAtlas get_packs;
	public UITexture tips;
	public GameObject backGameObject;


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
		totalStars = 150;
		AppMain.Instance.SetBool (AppMain.KEY_MAX_STAR_REWARD, false);
		if (totalStars == 150 && !AppMain.Instance.GetValue (AppMain.KEY_MAX_STAR_REWARD)) {
			UISprite sprite = sheepGameObject.GetComponent<UISprite> ();
			sprite.atlas = get_packs;
			sprite.spriteName="get packs0001";
			spriteAnimation.loop = false;
			spriteAnimation.namePrefix="get packs00";
			spriteAnimation.ResetToBeginning ();
			spriteAnimation.Play();
			spriteAnimation.framesPerSecond=25;
			backGameObject.SetActive(true);
		}
	}

	public IEnumerator Download(){
		Debug.Log ("send feed");
		FBFeedParams feed = new FBFeedParams ();
		feed.link="share link";
		feed.linkName="link name";
		feed.linkCaption="caption";
		feed.linkDescription="desc";
		feed.picture="pic";
		FBHelper.Instance.Share (feed);
		WWW www = new WWW ("");
		yield return www;
		backGameObject.SetActive (false);
	}




}
