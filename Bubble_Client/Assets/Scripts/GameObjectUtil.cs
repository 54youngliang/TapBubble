﻿using UnityEngine;
using System.Collections;

public class GameObjectUtil
{

	public static GameObject CloneGameObject(GameObject prefab, Transform parent)
	{
		var go = GameObject.Instantiate(prefab) as GameObject;
		go.transform.parent = parent;
		go.transform.localPosition = Vector3.zero;
		go.transform.localRotation = Quaternion.identity;
		go.transform.localScale = Vector3.one;
		return go;
	}
}