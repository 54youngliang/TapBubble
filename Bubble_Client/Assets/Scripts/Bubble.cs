using UnityEngine;
using System.Collections;

public class Bubble: MonoBehaviour {
	
	public float BubbleScaleTime = 5f;
	public UISprite sheepSprite;
	public UILabel uiLabel;
	public BubbleInit bubbleInit;

	bool needDestory = false;
	int destoryTime = int.MaxValue;

	UIAtlas uiAtlas ;
	
	int forceTime = 20;

	void Update(){
		//RefreshAltas ();
		forceTime -= 1;
		if (forceTime == 0) {
			this.gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (Random.Range (-0.5f, 0.5f), Random.Range (-0.5f, 0.5f)));
			forceTime=20;
		}

		TweenZ.Add (this.gameObject, 2f, 360f);

		if (needDestory) 
		{
			destoryTime-=1;
		}
		if (destoryTime <= 0) 
		{
			Destroy(this.gameObject);
		}
	}
	
	public void AppearNum(BubbleInit bubbleInit){
		this.bubbleInit = bubbleInit;
		uiLabel.text = bubbleInit.view;
	}
	
	int randomType = 0;
	public void BeginDestory(){
		AppMain.Instance.AudioController.PlaySheep ();
		uiLabel.enabled = false;
		float refreshTime = 0.04f;
		randomType = Random.Range (1, 4);
		InvokeRepeating("Disappear",0,refreshTime);

		//TweenScale scale = TweenScale.Begin (this.gameObject, BubbleScaleTime, this.transform.localScale*0.5f);
		//scale.AddOnFinished(DispearBubble);
		//scale.AddOnFinished(PlayParticleSystem);
	}


	int disNum=1;
	
	public void InitSprite(){
		bool isDay = AppMain.Instance.IsDay ();
		string spriteName = "";
		//RefreshAltas ();
		if (isDay) {
			//Resources.UnloadAsset(uiAtlas.gameObject);
			uiAtlas = (Resources.Load ("Atlas/sheep_dis_d") as GameObject).GetComponent<UIAtlas>();
			sheepSprite.atlas = uiAtlas;	
			spriteName="dis1_d0001";
		} else {
			//	Resources.UnloadAsset(uiAtlas.gameObject);
			uiAtlas = (Resources.Load ("Atlas/sheep_dis_n") as GameObject).GetComponent<UIAtlas>();
			sheepSprite.atlas = uiAtlas;
			spriteName="dis1_n0001";
		}
		sheepSprite.spriteName = spriteName;
	}

	private void Disappear(){
		if (disNum > 17) {
			DispearBubble();
			CancelInvoke ("Disappear");
		}

		string spriteName = "";
		if (AppMain.Instance.IsDay ()) {
			spriteName="dis"+randomType+"_d";
		} else {
			spriteName="dis"+randomType+"_n";
		}
		if (disNum >= 10) {
			sheepSprite.spriteName = spriteName + "00"+ disNum;
		} else {
			sheepSprite.spriteName = spriteName + "000"+ disNum;
		}
		disNum += 1;
	}

	private void DispearBubble(){
		sheepSprite.enabled = false;
		Destroy (this.gameObject);
	}
	
//	private void PlayParticleSystem(){
//		ParticleSystem ps = this.gameObject.GetComponent<ParticleSystem>();
//		ps.GetComponent<Renderer> ().sortingLayerName = "Foreground";
//		ps.Play();
//		needDestory = true;
//		destoryTime = 15;
//	}
	
}
