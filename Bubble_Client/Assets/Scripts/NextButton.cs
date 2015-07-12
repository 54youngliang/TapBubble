using UnityEngine;
using System.Collections;

public class NextButton : MonoBehaviour {

	public GameObject label;
	public PlayAnimation playAnimation;

	public void GoNextLevel()
	{
		Debug.Log ("Click to Next Level");
		playAnimation.StopNormalPlay ();
		GameObject earth = AppMain.Instance.HomeWindow.background.GetComponent<Background> ().earth;
		float z = earth.transform.localEulerAngles.z;
		TweenRZ.Add (earth, 1f,(z+36f) ).OnComplete+= Dispear;
	}

	private void Dispear(){
		playAnimation.StartDisapear ();
		this.gameObject.SetActive (false);
		AppMain.Instance.levelPassedWindow.gameObject.SetActive (false);
		AppMain.Instance.HomeWindow.BeginMission ();
	}
}
