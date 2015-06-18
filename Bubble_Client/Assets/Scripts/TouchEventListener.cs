using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TouchEventListener : MonoBehaviour
{
    public delegate void VoidDelegate(GameObject go);
    public delegate void BoolDelegate(GameObject go,bool state);
    public delegate void FloatDelegate(GameObject go,float delta);
    public delegate void VectorDelegate(GameObject go,Vector2 position);
    public delegate void VectorBoolDelegate(GameObject go,Vector2 position,bool state);
    public delegate void VectorVectorDelegate(GameObject go,Vector2 position,Vector2 delta);
    public delegate void ObjectDelegate(GameObject go,GameObject draggedObject);
    public delegate void KeyCodeDelegate(GameObject go,KeyCode key);
    
    public VectorBoolDelegate onHover;
    public VectorBoolDelegate onPress;
    public VectorDelegate onClick;
    public VectorDelegate onDoubleClick;
    public VectorBoolDelegate onSelect;
    public VectorVectorDelegate onDrag;
    public VectorDelegate onDragHover;
    
    void Awake()
    {
        initialize();
    }
    
    /// <summary>
    /// Get or add an event listener to the specified game object.
    /// </summary>    
    
    static public TouchEventListener Get(GameObject gameObject)
    {
        TouchEventListener listener = gameObject.GetComponent<TouchEventListener>();
        if (listener == null)
        {
            listener = gameObject.AddComponent<TouchEventListener>();
        }
        return listener;
    }
    
    void initialize()
    {
        BoxCollider colider = gameObject.GetComponent<BoxCollider>();
        if (colider == null)
        {
           // gameObject.AddComponent<BoxCollider>().size = objectSize();
        }    

    }
	
    Vector2 objectSize()
    {
        Vector3[] localConers = gameObject.GetComponent<UIRect>().localCorners;
        Vector3 bottomLeft = localConers[0];
        Vector3 topRight = localConers[2];
        return new Vector2(topRight.x - bottomLeft.x, topRight.y - bottomLeft.y);
    }
	
    void OnHover(bool isOver)
    {
        touchLog("OnHover", isOver);
		
        if (onHover != null)
        {
            onHover(gameObject, localPositionCurrentTouch(), isOver);
        }
    }
	
    void OnPress(bool pressed)
    {
        touchLog("OnPress", pressed);
		
        if (!pressed && UICamera.currentTouch.current)
        {
            touchLog("Pull Hover", pressed);
        }
        if (onPress != null)
        {
            onPress(gameObject, localPositionCurrentTouch(), pressed);
        }
    }
	
    void OnClick()
    {
        touchLog("OnClick");
		
        if (onClick != null)
        {
            onClick(gameObject, localPositionCurrentTouch());
        }
    }
	
    void OnDoubleClick()
    {
        touchLog("OnDoubleClick");
		
        if (onDoubleClick != null)
        {
            onDoubleClick(gameObject, localPositionCurrentTouch());
        }
    }
	
    void OnSelect(bool selected)
    {
        touchLog("OnSelect", selected);
		
        if (onSelect != null)
        {
            onSelect(gameObject, localPositionCurrentTouch(), selected);
        }
    }
	
    void OnDrag(Vector2 delta)
    {
        touchLog("OnDrag");
		
        if (onDrag != null)
        {
            onDrag(gameObject, localPositionCurrentTouch(), delta);
        }
		
        if (UICamera.currentTouch.current)
        {
            touchLog("DragHover");
			
            if (onDragHover != null)
            {
                onDragHover(gameObject, localPositionCurrentTouch());
            }
        }
    }
	
    Vector2 localPositionCurrentTouch()
    {
        return PositionUtil.ConvertPositionWorldToLocal(UICamera.lastWorldPosition, gameObject);
    }
	
    void touchLog(string eventName, bool value = false)
    {
		//Debug.Log(eventName + "  " + value);
//		LogUtil.DebugLog("event : " + eventName);
//		LogUtil.DebugLog("value : " + value);
//		if(UICamera.currentTouch != null && UICamera.currentTouch.current != null) {
//			LogUtil.DebugLog("CurrentTouch : " + UICamera.currentTouch.current.name);		
//			LogUtil.DebugLog("TouchID : " + UICamera.currentTouchID);
//			LogUtil.DebugLog("Screen Position : " + UICamera.currentTouch.pos);
//			LogUtil.DebugLog("World Position : " + UICamera.currentCamera.ScreenToWorldPoint(UICamera.currentTouch.pos));
//			LogUtil.DebugLog("Local Position : " + localPositionCurrentTouch());
//		}
    }
}
