using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelWindow : MonoBehaviour {

	//public UIScrollView scrollView;
	public GameObject levelItemPrefab;
	public Transform grid;
	//public GameObject levelBgPrefab;
	public GameObject background;

	private List<LevelView> viewList;
	// Use this for initialization
	void Start () {
		var width = AppMain.Instance.uiRoot.manualWidth;
		this.transform.localPosition = new Vector3(width, 0, 0);
		Init ();
	}
	
	private void Init()
	{
//		viewList = new List<LevelView>();
//		for(int i = 0; i < 100; i ++)
//		{
//			//var go = GameObjectUtil.CloneGameObject(levelItemPrefab, scrollView.transform);
//			var item = go.GetComponent<LevelView>();
//			viewList.Add(item);
//			int x = Random.Range(-300, 300);
//			item.transform.localPosition = new Vector3(x, i * 200 - 360, 0);
//			item.Level = i + 1;
//			item.Star = PlayerPrefs.GetInt("star_level_" + i, -1);
//			item.OnItemClick = OnLevelClick;
//		}
//
//		float maxY = viewList[viewList.Count - 1].transform.localPosition.y;
//		int ah = AppMain.Instance.uiRoot.activeHeight;
//		int count = (int)Mathf.Ceil(maxY / ah);
//		for(int i = 0; i < count; i ++)
//		{
//		//	GameObjectUtil.CloneGameObject(levelBgPrefab, grid);
//		}
//		UIGrid uiGrid = grid.GetComponent<UIGrid>();
//		uiGrid.Reposition();
	}

	public void ShowHome()
	{
		Debug.Log ("Map click Home Button");
		var width = AppMain.Instance.uiRoot.manualWidth;
//		this.transform.localPosition = new Vector3(0, 0, 0);
		AppMain.Instance.HomeWindow.ShowHomeWindow ();
		this.gameObject.SetActive(true);
		AppMain.Instance.HomeWindow.gameObject.SetActive(true);
		Tween tween = TweenX.Add(this.gameObject, 0.5f, width);
		tween.OnComplete += OnShowHomeComplete;
		
		TweenX.Add(AppMain.Instance.HomeWindow.gameObject, 0.5f, 0f);
	}

	private void OnShowHomeComplete()
	{
		this.gameObject.SetActive(false);
	}

	private void OnLevelClick(int level)
	{
		AppMain.Instance.CurrentLevel = level;
		AppMain.Instance.HomeWindow.gameObject.transform.localPosition = Vector3.zero;
		AppMain.Instance.HomeWindow.BeginMission ();
		Debug.LogFormat("Click level:{0}" , level);
	}

}
