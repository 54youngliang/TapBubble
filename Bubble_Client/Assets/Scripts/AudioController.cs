using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioController : MonoBehaviour {

	public AudioSource sheep ;
	public AudioSource bgm ;

	void Start()
	{

	}

	public void Stop(){
		if (null != sheep) {
			sheep.Stop();
		}
		if (null != bgm) {
			bgm.Stop();
		}
	}

	Dictionary<string,AudioClip> audioClipCache = new Dictionary<string,AudioClip >();

	public void PlayBgm()
	{
		Debug.Log (AppMain.Instance.Music + ")))))");
		if (AppMain.Instance.Music) {
			bgm.clip = Resources.Load ("audio/bmg1") as AudioClip;
			bgm.Play ();
		}
	}

	public void StopBgm()
	{
		bgm.Stop ();
	}

	public void PlaySheep()
	{
		if (AppMain.Instance.Music) {
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
	}

}
