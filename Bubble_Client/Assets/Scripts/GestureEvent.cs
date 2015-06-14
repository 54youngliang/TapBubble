using UnityEngine;
using System.Collections;
using System;

//[RequireComponent(typeof(TouchEventListener))]
public class GestureEvent : MonoBehaviour {

	public Action OnLeft;
	public Action OnRight;
	public Action OnTop;
	public Action OnBottom;

	void Start()
	{
		TouchEventListener touch = TouchEventListener.Get(this.gameObject);
		touch.onPress += onPress;
	}

	private Vector2 _LastPos;
	void onPress(GameObject go, Vector2 pos, bool pressed)
	{
		if(pressed)
		{
			_LastPos = pos;
		}
		else
		{
			if(_LastPos == null)
			{
				return;
			}
			var dx = pos.x - _LastPos.x;
			var dy = pos.y - _LastPos.y;
			if(Mathf.Abs(dx) > 50 && Mathf.Abs(dy) < 50)
			{
				if (dx > 0 && OnRight != null)
				{
					Debug.Log("OnRight");
					OnRight();
				}
				else if(OnLeft != null)
				{
					Debug.Log("OnLeft");
					OnLeft();
				}
			}
			else if(Mathf.Abs(dx) < 50 && Mathf.Abs(dy) > 50)
			{
				if (dy > 0 && OnTop != null)
				{
					Debug.Log("OnTop");
					OnTop();
				}
				else if(OnBottom != null)
				{
					Debug.Log("OnBottom");
					OnBottom();
				}
			}
		}
	}
}
