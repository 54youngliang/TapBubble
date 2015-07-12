using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelWindow : MonoBehaviour {

	public UIScrollView scrollView;
	public GameObject levelItemPrefab;
	public Transform grid;
	//public GameObject levelBgPrefab;
	public GameObject background;
	public GameObject levelProgress;

	private List<LevelView> viewList;

	// Use this for initialization
	void Start () {
		var width = AppMain.Instance.uiRoot.manualWidth;
		this.transform.localPosition = new Vector3(width, 0, 0);
		Init ();
	}
	
	private void Init()
	{
		levelProgress.GetComponent<LevelProgress> ().Apprear ();
		int totalStars = 0;
		int alreadyStars = 0;
		int maxLevel = AppMain.Instance.MaxLevel;
		foreach (MissionMeta missionMeta in MissionConfig.GetAllMissionMeta()) {
			int missionId = missionMeta.missionId;
			totalStars+=3;
			alreadyStars += AppMain.Instance.GetStar(missionId);
			GameObject missionResult = GameObjectUtil.CloneGameObject(levelItemPrefab, background.transform);
			int x = missionMeta.x;
			int y =  missionMeta.y;
			missionResult.transform.localPosition = new Vector3(x,y,0);
			LevelView view = missionResult.GetComponent<LevelView>();
			int level = AppMain.Instance.GetStar(missionId);
			view.Star=level;

			view.OnItemClick = OnLevelClick;
			Debug.Log("xxxxxxx"+maxLevel);
			if(missionId == maxLevel){
				view.Sheep.SetActive(true);
			}else{
				view.Level=missionId;
				view.Sheep.SetActive(false);
			}
		}
		levelProgress.GetComponent<UIProgressBar>().value = (alreadyStars/totalStars);
		levelProgress.GetComponentInChildren<UILabel> ().text = alreadyStars+"/"+totalStars;
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
