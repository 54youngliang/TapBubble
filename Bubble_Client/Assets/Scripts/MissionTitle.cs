using UnityEngine;
using System.Collections;

public class MissionTitle : MonoBehaviour {

	public UIFont count_d;
	public UIFont count_n;

	void Start(){
		bool isDay = AppMain.Instance.IsDay ();
		UIFont font;
		if (isDay) {
			font= count_d;
		} else {
			font = count_n;
		}
		this.gameObject.GetComponent<UILabel>().bitmapFont=font;
	}
}
