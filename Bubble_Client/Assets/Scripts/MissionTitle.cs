using UnityEngine;
using System.Collections;

public class MissionTitle : MonoBehaviour {

	public Transform transform;

	void Start(){
		this.transform.localPosition = transform.localPosition;
		bool isDay = AppMain.Instance.IsDay ();
		if (!isDay) {
			this.gameObject.GetComponent<UILabel> ().color = new Color (2 / 255.0f, 135 / 255.0f, 135 / 255.0f);
		}
	}
}
