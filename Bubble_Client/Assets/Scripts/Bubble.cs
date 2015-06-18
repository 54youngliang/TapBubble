using UnityEngine;
using System.Collections;

public class Bubble: MonoBehaviour {
	
	public float BubbleScaleTime = 5f;
	public UISprite uiSprite;
	public UILabel uiLabel;
	public Vector3 targetLocal = new Vector3(0.5f, 0.5f, 0.5f);
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
	
	public void BeginDestory(){
		TweenScale scale = TweenScale.Begin (this.gameObject, BubbleScaleTime, this.transform.localScale*0.5f);
		scale.AddOnFinished(DispearBubble);
		scale.AddOnFinished(PlayParticleSystem);
	}
	
	private void DispearBubble(){
		uiSprite.enabled = false;
		uiLabel.enabled = false;
	}
	
	private void PlayParticleSystem(){
		ParticleSystem ps = this.gameObject.GetComponent<ParticleSystem>();
		ps.GetComponent<Renderer> ().sortingLayerName = "Foreground";
		ps.Play();
		needDestory = true;
		destoryTime = 10;
	}
	
}
