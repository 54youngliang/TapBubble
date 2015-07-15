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
	public UILabel progressLabel;

	private List<LevelView> viewList = new List<LevelView>();

	// Use this for initialization
	void Start () {
		var width = AppMain.Instance.uiRoot.manualWidth;
		this.transform.localPosition = new Vector3(width, 0, 0);
		Init ();
	}
	
	public void Init()
	{
		levelProgress.GetComponent<LevelProgress> ().Apprear ();
		int maxLevel = AppMain.Instance.MaxLevel;
		foreach (MissionMeta missionMeta in MissionConfig.GetAllMissionMeta()) {
			int missionId = missionMeta.missionId;
			GameObject missionResult = GameObjectUtil.CloneGameObject(levelItemPrefab, background.transform);
			int x = missionMeta.x;
			int y =  missionMeta.y;
			missionResult.transform.localPosition = new Vector3(x,y,0);
			LevelView view = missionResult.GetComponent<LevelView>();
			//view.OnItemClick = OnLevelClick;
			view.MissionId=missionId;
			viewList.Add(view);
		}

		RefreshStatus ();
	}

	public void RefreshStatus(){
		int maxLevel = AppMain.Instance.MaxLevel;

		int alreadyStars = 0;
		foreach(LevelView view in viewList){
			int missionId = view.MissionId;
			if (missionId > maxLevel) {
				view.GetComponent<UIButton>().enabled=false;
			}else{
				view.GetComponent<UIButton>().enabled=true;
			}
			int missionStar = AppMain.Instance.GetStar(missionId);
			alreadyStars += missionStar;
			Debug.Log("Refresh map star,missionId:"+missionId+",star:"+missionStar);
			view.UpdateStar(missionStar);
			if(missionId == maxLevel){
				view.Sheep.SetActive(true);
			}else{
				view.MissionId=missionId;
				view.Sheep.SetActive(false);
			}
		}

		int totalStars = 0;
		foreach (MissionMeta missionMeta in MissionConfig.GetAllMissionMeta()) {
			int missionId = missionMeta.missionId;
			totalStars+=3;
		}
		
		levelProgress.GetComponent<UIProgressBar>().value = (1.0f*alreadyStars/totalStars);
		progressLabel.text = alreadyStars+"/"+totalStars;

	}

	public void ShowHome()
	{

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
		int maxLevel = AppMain.Instance.MaxLevel;
		if (level > maxLevel) {
			return;
		}
		AppMain.Instance.CurrentLevel = level;
		AppMain.Instance.HomeWindow.gameObject.transform.localPosition = Vector3.zero;
		AppMain.Instance.HomeWindow.BeginMission ();

	}

}
