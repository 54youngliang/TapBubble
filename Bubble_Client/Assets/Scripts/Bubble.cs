using UnityEngine;
using System.Collections;

public class Bubble: MonoBehaviour {
	
	public float BubbleScaleTime = 5f;
	public UISprite uiSprite;
	public UILabel uiLabel;
	public BubbleInit bubbleInit;

	bool needDestory = false;
	int destoryTime = int.MaxValue;

	void Update(){
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

	int type = 0;
	public void BeginDestory(){
		uiLabel.enabled = false;
		type = Random.Range (1, 3);
		float refreshTime = 0.02f;
		InvokeRepeating("Disappear",0,refreshTime);

		//TweenScale scale = TweenScale.Begin (this.gameObject, BubbleScaleTime, this.transform.localScale*0.5f);
		//scale.AddOnFinished(DispearBubble);
		//scale.AddOnFinished(PlayParticleSystem);
	}


	int disNum=1;
	private void Disappear(){
			if (disNum > 17) {
				DispearBubble();
				CancelInvoke ("Disappear");
			}
			uiSprite.spriteName = "sheep" + (10000*type + disNum);
			disNum += 1;
	}

	private void DispearBubble(){
		uiSprite.enabled = false;
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
