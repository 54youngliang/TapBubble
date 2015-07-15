using UnityEngine;
using System.Collections;

public class PlayAnimation : MonoBehaviour {

	public UISprite sheepSprite;

	public UIAtlas DayAtlas;
	public UIAtlas NightAtlas;
	
	int nowPic = 1;
	public bool IsPlaying=false;
	
	public void StartNormalPlay(){
		if (!IsPlaying) {
			nowPic =1;
			IsPlaying = true;
			InvokeRepeating ("RefreshNormalPlay", 0, 0.06f);
		}
	}
	
	public void StopNormalPlay(){
		string spriteName;
		if (AppMain.Instance.IsDay ()) {
			if(!sheepSprite.atlas.name.Equals("play_d")){
				sheepSprite.atlas = DayAtlas;
			}
			spriteName="play_d_0001";
		} else {
			if(!sheepSprite.atlas.name.Equals("play_n")){
				sheepSprite.atlas = NightAtlas;
			}
			spriteName="play_n_0001";
		}
		sheepSprite.spriteName = spriteName;
		CancelInvoke ("RefreshNormalPlay");
		IsPlaying = false;
	}
	
	private void RefreshNormalPlay(){
		if (nowPic > 40) {
			nowPic=1;
		}
		string spriteName;
		if (AppMain.Instance.IsDay ()) {
			if(!sheepSprite.atlas.name.Equals("play_d")){
				sheepSprite.atlas = DayAtlas;
			}
			spriteName="play_d_";
		} else {
			if(!sheepSprite.atlas.name.Equals("play_n")){
				sheepSprite.atlas = NightAtlas;
			}
			spriteName="play_n_";
		}
		if (nowPic >= 10) {
			spriteName = spriteName + "00" + nowPic;
		} else {
			spriteName = spriteName + "000" + nowPic;
		}
		sheepSprite.spriteName = spriteName;
		nowPic += 1;
	}
	
	public void StartDisapear(){
		IsPlaying = true;
		if (AppMain.Instance.IsDay ()) {
			if(!sheepSprite.atlas.name.Equals("play_d")){
				sheepSprite.atlas = DayAtlas;
			}
		} else {
			if(!sheepSprite.atlas.name.Equals("play_n")){
				sheepSprite.atlas = NightAtlas;
			}
		}
		disppearPic = 1;
		InvokeRepeating ("RefreshDisppearPlay", 0, 0.06f);
	}

	int disppearPic = 1;
	private void RefreshDisppearPlay(){
		if (disppearPic > 14) {
			disppearPic=1;
			sheepSprite.spriteName = "";
			CancelInvoke ("RefreshDisppearPlay");
			IsPlaying=false;
		}
		string spriteName;
		if (AppMain.Instance.IsDay ()) {
			if(!sheepSprite.atlas.name.Equals("play_d")){
				sheepSprite.atlas = DayAtlas;
			}
			spriteName="next_d";
		} else {
			if(!sheepSprite.atlas.name.Equals("play_n")){
				sheepSprite.atlas = NightAtlas;
			}
			spriteName="next_n";
		}
		if (disppearPic >= 10) {
			spriteName = spriteName + "00" + disppearPic;
		} else {
			spriteName = spriteName + "000" + disppearPic;
		}
		sheepSprite.spriteName = spriteName;
		disppearPic += 1;
	}
}
