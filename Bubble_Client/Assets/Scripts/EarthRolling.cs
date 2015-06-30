using UnityEngine;
using System.Collections;

public class EarthRolling : MonoBehaviour {

	TweenRZ t;

	public float RotationTime = 120f;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("RotationZ", 0f, RotationTime);
	}

	private void RotationZ(){
		t = TweenRZ.Add (this.gameObject, RotationTime, 360f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
