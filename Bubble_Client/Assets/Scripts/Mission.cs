using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mission : MonoBehaviour {

	private List<GameObject> bubbleList;
	private GameObject nextBubble;
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity);
			if (hit.collider != null) {
				if(hit.rigidbody.gameObject == nextBubble){
					// right
				}else{
					// wrong
				}
			}
		}
	}

	void startGame(MissionConfig missionConfig){
		
//		bubbleList = missionConfig.randomBubbleList ();
//		List<GameObject> bubbleList = new List<GameObject> ();
//		foreach (Bubble bubble in bubbleList) {
//			// init bubble
//
//			// init bubble list
//		}
		
	}

	void endGame(){
		foreach (GameObject bubble in bubbleList) {
			BubbleClick click = bubble.GetComponent<BubbleClick>();
			click.beginDestory();
		}
	}
}
