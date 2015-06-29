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
	
	public List<BubbleInit> randomBubbleInit(){
		List<BubbleInit> resultList = new List<BubbleInit> ();

		Dictionary<int,int> tmpCache = new Dictionary<int,int >();

		for (int i = 0; i<positiveNum; i++) 
		{
			if(missionId == 1){
				// diyiguan,0~9,10~19……90~99，每个区间只出现1个数。
				int tens = 0;
				for(;;){
					tens = Random.Range(0,10);
					if(!tmpCache.ContainsKey(tens)){
						break;
					}
				}
				resultList.Add(PositiveNumberBubble(tens,tens));
			}else if (missionId == 2){
				//有1个区间出现2组数。
				int tens = 0;
				for(;;){
					tens = Random.Range(0,10);
					if(!tmpCache.ContainsKey(tens)){
						break;
					}else{
						int twiceTimes = 0;
						foreach(int amount in tmpCache.Values){
							if(amount >=2){
								twiceTimes+=1;
							}
						}
						if(twiceTimes < 1){
							break;
						}
					}
				}		
				resultList.Add(PositiveNumberBubble(tens,tens));
			}else if (missionId == 3){
				//有2个区间出现2组数。
				int tens = 0;
				for(;;){
					tens = Random.Range(0,10);
					if(!tmpCache.ContainsKey(tens)){
						break;
					}else{
						int twiceTimes = 0;
						foreach(int amount in tmpCache.Values){
							if(amount >=2){
								twiceTimes+=1;
							}
						}
						if(twiceTimes < 2){
							break;
						}
					}
				}		
				resultList.Add(PositiveNumberBubble(tens,tens));
			}else{
				resultList.Add(PositiveNumberBubble(0,9));
			}

		}
		for (int i =0; i<negativeNum; i++) 
		{
			resultList.Add(NegativeNumberBubble());
		}
		for (int i=0; i<easyOperation; i++) 
		{
			resultList.Add(EasyOpBubble());
		}
		for (int i=0; i<hardOperation; i++) 
		{
			resultList.Add(HardOpBubble());
		}
		for (int i=0; i<radical; i++) 
		{
			resultList.Add(RadicalBubble());
		}

		resultList.Sort ((x,y) => x.result.CompareTo(y.result));
		
		for (int i = 0; i<resultList.Count; i++) {
			BubbleInit temp = (BubbleInit)resultList[i];
			temp.order=i;
		}

		return resultList;
	}

	private static int[] singleDigit={-9,-8,-7,-6,-5,-4,-3,-2,-1,0,1,2,3,4,5,7,8,9};

	private BubbleInit PositiveNumberBubble(int tensMin,int tensMax)
	{
		int tens = Random.Range (tensMin, tensMax+1);
		int num = tens*10 + Random.Range (0, 10);
		BubbleInit init = new BubbleInit ();
		init.result = num;
		init.view = num + "";
		return init;
	}

	private BubbleInit NegativeNumberBubble()
	{
		int num = Random.Range (-99, 0);
		BubbleInit init = new BubbleInit ();
		init.result = num;
		init.view = num + "";
		return init;
	}

	private BubbleInit EasyOpBubble()
	{
		int opNum = Random.Range (0, 5) % 4;
		double result=0;
		string view="";
		if (opNum == 0) {
			// +
			int num1 = Random.Range(-9,10);
			int num2 = Random.Range(-9,10);
			result = num1+num2;
			view = num1+"+"+num2;
		} else if (opNum == 1) {
			// -
			int num1 = Random.Range(0,10);
			int num2 = Random.Range(-9,10);
			result = num1+num2;
			view = num1+"-"+num2;
		} else if (opNum == 2) {
			// *
			int num1 = Random.Range(-9,10);
			int num2 = Random.Range(-9,10);
			result = num1+num2;
			view = num1+"X"+num2;
		} else if (opNum == 3) {
			// ÷
			int num1 = Random.Range(-9,10);
			int num2 = Random.Range(-9,10);
			if(num2 ==0){
				result = double.MaxValue;
			}else{
				result = num1+num2;
			}
			view = num1+"÷"+num2;
		}
		BubbleInit init = new BubbleInit ();
		init.result = result;
		init.view = view;
		return init;
	}

	private BubbleInit HardOpBubble()
	{
		int opNum = Random.Range (0, 5) % 4;
		double result=0;
		string view="";
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
			result = num1+num2;
			view = num1+"-"+num2;
		} else if (opNum == 2) {
			// *
			int num1 = Random.Range(-10,11);
			int num2 = Random.Range(-20,21);
			result = num1+num2;
			view = num1+"X"+num2;
		} else if (opNum == 3) {
			// ÷
			int num1 = Random.Range(-10,11);
			int num2 = Random.Range(-20,21);
			if(num2 ==0){
				result = double.MaxValue;
			}else{
				result = num1+num2;
			}
			view = num1+"÷"+num2;
		}
		BubbleInit init = new BubbleInit ();
		init.result = result;
		init.view = view;
		return init;

	}

	private BubbleInit RadicalBubble()
	{
		float num = Random.Range (0f, 99f);
		double result = Mathf.Sqrt (num);
		string view = "√"+num;
		BubbleInit init = new BubbleInit ();
		init.result = result;
		init.view = view;
		return init;
	}

}
