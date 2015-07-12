using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissionMeta {

	public int missionId;
	public int positiveNum;
	public int negativeNum;
	public int easyOperation;
	public int hardOperation;
	public int radical;
	public int time;
	public int level1;
	public int level2;
	public int level3;
	public int x;
	public int y;

	private static int[] excludeNum = {6,9,19,16,61,91,18,81,66,99};

	public List<BubbleInit> randomBubbleInit(){
		List<BubbleInit> resultList = new List<BubbleInit> ();
		Dictionary<double,int> tmpCache = new Dictionary<double,int > ();

		if (missionId <4) {
			int tensMax = 0;
			if(missionId == 1){
				tensMax = 0;
			}else if (missionId == 2){
				tensMax = 2;
			}else {
				tensMax = 5;
			}
			for (int i = 1; i<=6; i++) {
				resultList.Add (PositiveNumberBubble (0, tensMax, tmpCache));
			}
		} else {

		
			for (int i = 0; i<positiveNum; i++) {
				resultList.Add (PositiveNumberBubble (0, 9, tmpCache));
			}
			for (int i =0; i<negativeNum; i++) {
				resultList.Add (NegativeNumberBubble (tmpCache));
			}
			for (int i=0; i<easyOperation; i++) {
				resultList.Add (EasyOpBubble (tmpCache));
			}
			for (int i=0; i<hardOperation; i++) {
				resultList.Add (HardOpBubble (tmpCache));
			}
			for (int i=0; i<radical; i++) {
				resultList.Add (RadicalBubble (tmpCache));
			}
		}
		resultList.Sort ((x,y) => x.result.CompareTo(y.result));
		
		for (int i = 0; i<resultList.Count; i++) {
			BubbleInit temp = (BubbleInit)resultList[i];
			temp.order=i;
		}

		return resultList;
	}

	private BubbleInit PositiveNumberBubble(int tensMin,int tensMax,Dictionary<double,int>  alreadyMap)
	{
		int num = 0;
		for (;;) {
		 
			for (;;) {
				int tens = Random.Range (tensMin, tensMax + 1);
				num = tens * 10 + Random.Range (0, 10);
				bool hasExclude = false;
				foreach (int aa in excludeNum) {
					if (aa == num) {
						hasExclude = true;
					}
				}
				if (!hasExclude) {
					break;
				}
			}
			if(alreadyMap.ContainsKey(num)){
				continue;
			}else{
				alreadyMap[num]=1;
				break;
			}
		}

		BubbleInit init = new BubbleInit ();
		init.result = num;
		init.view = num + "";
		return init;
	}

	private BubbleInit NegativeNumberBubble(Dictionary<double,int> alreadyMap)
	{
		int num = 0;
		for (;;) {
			num = Random.Range (-99, 0);
			if(alreadyMap.ContainsKey(num)){
				continue;
			}else{
				alreadyMap[num]=1;
				break;
			}
		}

		BubbleInit init = new BubbleInit ();
		init.result = num;
		init.view = num + "";
		return init;
	}

	private BubbleInit EasyOpBubble(Dictionary<double,int>  alreadyMap)
	{
		int opNum = Random.Range (0, 5) % 4;
		double result=0;
		string view="";
		for (;;) {
			if (opNum == 0) {
				// +
				int num1 = Random.Range(0,10);
				int num2 = Random.Range(0,10);
				result = num1+num2;
				view = num1+"+"+num2;
			} else if (opNum == 1) {
				// -
				int num1 = Random.Range(0,10);
				int num2 = Random.Range(0,10);
				result = num1-num2;
				view = num1+"-"+num2;
			} else if (opNum == 2) {
				// *
				int num1 = Random.Range(0,10);
				int num2 = Random.Range(0,10);
				result = num1*num2;
				view = num1+"X"+num2;
			} else if (opNum == 3) {
				// ÷
				int num1 = Random.Range(0,10);
				int num2 = Random.Range(0,10);
				if(num2 ==0){
					result = double.MaxValue;
				}else{
					result = 1.0f*num1/num2;
				}
				view = num1+"÷"+num2;
			}
			if(alreadyMap.ContainsKey(result)){
				continue;
			}else{
				alreadyMap[result]=1;
				break;
			}
		}

		BubbleInit init = new BubbleInit ();
		init.result = result;
		init.view = view;
		return init;
	}

	private BubbleInit HardOpBubble(Dictionary<double,int>  alreadyMap)
	{
		int opNum = Random.Range (0, 5) % 4;
		double result=0;
		string view="";
		for (;;) {
			if (opNum == 0) {
				// +
				int num1 = Random.Range(-10,11);
				int num2 = Random.Range(-20,21);
				result = num1+num2;
				view = num1+"+"+num2;
			} else if (opNum == 1) {
				// -
				int num1 = Random.Range(-10,11);
				int num2 = Random.Range(-20,21);
				result = num1-num2;
				view = num1+"-"+num2;
			} else if (opNum == 2) {
				// *
				int num1 = Random.Range(-10,11);
				int num2 = Random.Range(-20,21);
				result = num1*num2;
				view = num1+"X"+num2;
			} else if (opNum == 3) {
				// ÷
				int num1 = Random.Range(-10,11);
				int num2 = Random.Range(-20,21);
				if(num2 ==0){
					result = double.MaxValue;
				}else{
					result = 1.0f * num1/num2;
				}
				view = num1+"÷"+num2;
			}
			if(alreadyMap.ContainsKey(result)){
				continue;
			}else{
				alreadyMap[result]=1;
				break;
			}
		}

		BubbleInit init = new BubbleInit ();
		init.result = result;
		init.view = view;
		return init;

	}

	private BubbleInit RadicalBubble(Dictionary<double,int>  alreadyMap)
	{
		double result = 0;
		string view = "";
		for (;;) {
			int num = Random.Range (0, 99);
			result = Mathf.Sqrt (num);
			view = "√"+num;
			if(alreadyMap.ContainsKey(result)){
				continue;
			}else{
				alreadyMap[result]=1;
				break;
			}
		
		}

		BubbleInit init = new BubbleInit ();
		init.result = result;
		init.view = view;
		return init;
	}

}
