using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioController : MonoBehaviour {

	public AudioSource sheep ;
	public AudioSource bgm ;


	void Start(){
		PlayBgm ();
	}

	Dictionary<string,AudioClip> audioClipCache = new Dictionary<string,AudioClip >();

	public void PlaySheep(){
		string clipName = "sheep" + Random.Range (1, 4);
		AudioClip clip =null;
		if (!audioClipCache.ContainsKey (clipName)) {
			clip = Resources.Load ("audio/"+clipName) as AudioClip;
			audioClipCache [clipName] = clip;
		} else {
			clip = audioClipCache[clipName];
		}
		sheep.clip = clip;
		sheep.Play ();
	}

	public void PlayBgm(){
		bgm.Play ();
	}

}
