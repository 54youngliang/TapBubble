using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioController : MonoBehaviour {

	public AudioSource sheep ;
	public AudioSource bgmGameSource ;
	public AudioSource bgmSource;
	public AudioSource countDown;

	private AudioClip bgm_d;
	private AudioClip bgm_n;
	private AudioClip bgm_g;
	private AudioClip countDownClip;


	void Start()
	{

		bgm_d = Resources.Load ("audio/bgm_d") as AudioClip;
		bgm_n = Resources.Load ("audio/bgm_n") as AudioClip;
		bgm_g = Resources.Load ("audio/bgm_g") as AudioClip;
		countDownClip = Resources.Load ("audio/time") as AudioClip;
		bgmGameSource.clip = bgm_g;
		StartAll ();
		isDay = AppMain.Instance.IsDay ();
	}

	public void PlayCountDown(){
		countDown.clip = countDownClip;
		countDown.Play ();
	}

	public void StopCountDown(){
		countDown.Stop ();
	}

	public void StopAll(){
		if (sheep.isPlaying) {
			sheep.Stop();
		}
		if (bgmSource.isPlaying) {
			bgmSource.Stop();
		}
		if (bgmGameSource.isPlaying) {
			bgmGameSource.Stop();
		}

	}

	public void StartAll(){
		if (!AppMain.Instance.HasMusic()) {
			return;
		}
		if (!bgmSource.isPlaying) {
			if(AppMain.Instance.IsDay()){
				bgmSource.clip = bgm_d;
			}else{
				bgmSource.clip = bgm_n;
			}
			bgmSource.Play();
		}
		if (AppMain.Instance.InGame && !bgmGameSource.isPlaying) {
			bgmGameSource.Play();
		}
		
	}

	Dictionary<string,AudioClip> audioClipCache = new Dictionary<string,AudioClip >();

	public void PlayBgm()
	{
		if (!AppMain.Instance.HasMusic()) {
			return;
		}

		AudioClip targetAudio = null;
		if (AppMain.Instance.IsDay ()) {
			targetAudio = bgm_d;
		} else {
			targetAudio = bgm_n;
		}

		bgmSource.clip = targetAudio;
		bgmSource.Play ();
	}

	public void StartBgmGame(){
		if (!AppMain.Instance.HasMusic()) {
			return;
		}
		if (!bgmGameSource.isPlaying) {
			bgmGameSource.Play ();
		}
	}

	public void StopBgmGame(){
		if (bgmGameSource.isPlaying) {
			bgmGameSource.Stop ();
		}
	}

	public void PlaySheep()
	{
		if (AppMain.Instance.HasMusic()) {
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


	int checkNum = 0;
	bool isDay ;
	void Update(){
		if (checkNum <= 50) {
			checkNum+=1;
		}
		checkNum = 0;
		bool isDayNew = AppMain.Instance.IsDay ();
		if (isDayNew != isDay) {
			StopBgmGame();
			StartBgmGame();
			isDay = isDayNew;
		}
	}

}
