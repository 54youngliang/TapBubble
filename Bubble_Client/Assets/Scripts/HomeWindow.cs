using UnityEngine;
using System.Collections;

public class HomeWindow : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.GetComponent<GestureEvent>().OnLeft = ShowLevelWindow;
	}

	private void ShowLevelWindow()
	{
		var width = AppMain.Instance.uiRoot.manualWidth;
		AppMain.Instance.LevelWindow.transform.localPosition = new Vector3(width, 0, 0);
		AppMain.Instance.LevelWindow.gameObject.SetActive(true);
		TweenX.Add(AppMain.Instance.LevelWindow.gameObject, 0.5f, 0f).OnComplete += ShowLevelWindow;
		
		TweenX.Add(this.gameObject, 0.5f, -width);
	}
	
	private void ShowLeveComplete()
	{
		this.gameObject.SetActive(false);
	}
}
