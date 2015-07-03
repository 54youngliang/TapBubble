using UnityEngine;
using System.Collections;

public class HelpButton : MonoBehaviour {

	public GameObject helpInfo;

	private bool active=false;

	public void ChangeStatus(){
		active = !active;
		helpInfo.SetActive (active);
	}

	public void UpdateInfoStaus(bool newStatus){
		active = newStatus;
		helpInfo.SetActive (active);
	}

}
