using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissionConfig {

	public int id=1;
	public int ballNum=1;
	public int addRate=1;

	public int decrRate=1;
	public int mutlRate=1;
	public int deviRate=1;
	public int reaganRate=1;

	public int[] rateArray = new int[5];

	public List<Bubble> randomBubbleList(){

		string question = "";
		string rightAnswer = "";

		List<Bubble> ballList = new List<Bubble>();

		for(int i = 1;i<=ballNum;i++){
			ballList.Add(randomBubble());
		}

//		ballList.Sort ();
		for (int i = 0; i<ballList.Count; i++) {
			Bubble temp = (Bubble)ballList[i];
			temp.order=i;
		}
		return ballList;
	}

	private Bubble randomBubble(){
		Bubble b = new Bubble ();
		b.result = Random.Range (0, 100);
		b.view = b.result + "";
		return b;
//		int total = 0;
//		int[] tmpRateArray = new int[rateArray.Length];
//		for (int i =0; i<rateArray.Length; i++) {
//			int num = rateArray[i];
//			total +=num;
//			tmpRateArray[i]=total;
//		}
//
//		int randomNum = Random.Range (0, total);
//
//		int functionType = 0;
//		for (int i =0; i<tmpRateArray.Length; i++) {
//			int tmpRateNum = tmpRateArray[i];
//			if(tmpRateNum > randomNum){
//				functionType=i;
//				break;
//			}
//		}
//
//		Bubble bubble = new Bubble ();
//
//		if (functionType == 1) {
//			// jiafa
//			int add1 = Random.Range(add1Min,add1Max);
//			int add2 = Random.Range(add2Min,add2Max);
//			bubble.result=add1+add2;
//			bubble.view=bubble.result+"";
//		} else if (functionType == 2) {
//			int decr1 = Random.Range(decr1Min,decr1Max);
//			int decr2 = Random.Range(decr2Min,decr2Max);
//			// jianfa
//		} else if (functionType == 3) {
//			// chengfa
//		
//		} else if (functionType == 4) {
//			// chufa
//		} else if (functionType == 5) {
//			//reagan
//		}

	}


}
