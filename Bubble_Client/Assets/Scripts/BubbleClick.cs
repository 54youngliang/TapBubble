using UnityEngine;
using System.Collections;

public class BubbleClick : MonoBehaviour {

	public int particleDestoryTime = 10;
	public int spriteDestoryTime = 5;
	public Vector3 targetLocal = new Vector3(0.5f, 0.5f, 0.5f);
	private bool needDestory=false;
	private int destoryTime = int.MaxValue;
	private bool particleStarted=false;
	private Vector3 scaleChangeSpeed;


	// Use this for initialization
	void Start () {
		Bubble bubble = new Bubble ();
		bubble.view = "111111";
		appearNum (bubble);
	}


	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			beginDestory();
		}
		destoryUpdate();
	}

	void appearNum(Bubble bubble){
		UILabel uiLabel = this.gameObject.GetComponentInChildren<UILabel> ();
		uiLabel.text = bubble.view;
	}

	public void beginDestory(){
		this.needDestory=true;
		scaleChangeSpeed = (this.gameObject.transform.localScale - targetLocal)/spriteDestoryTime;
	}

	private void destoryUpdate(){
		if (!needDestory) {
			return;
		}

		Vector3 nowVectory = this.gameObject.transform.localScale;
		if (needDestory) {
			nowVectory = nowVectory - scaleChangeSpeed;
			this.gameObject.transform.localScale = nowVectory;
			destoryTime=destoryTime-1;
		}
		if (nowVectory.x <= targetLocal.x && nowVectory.y <= targetLocal.y && nowVectory.z <= targetLocal.z) {
			UISprite sp = this.gameObject.GetComponent<UISprite>();
			sp.enabled=false;
			UILabel uiLabel = this.gameObject.GetComponentInChildren<UILabel> ();
			uiLabel.enabled=false;
			if(!particleStarted){
				ParticleSystem ps = this.gameObject.GetComponent<ParticleSystem>();
				ps.Play();
				particleStarted=true;
			}
			destoryTime = particleDestoryTime;
			destoryTime-=1;
		}
		if(destoryTime <=0){
			Destroy(this.gameObject);
		}
	}

}
