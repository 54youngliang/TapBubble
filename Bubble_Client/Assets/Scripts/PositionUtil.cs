using UnityEngine;
using System.Collections;


public static class PositionUtil
{
	public static void LocalPositionAtX(GameObject target, float x)
	{
		Vector3 movePositon = new Vector3(x, originalPosition(target).y, originalPosition(target).z);
		target.transform.localPosition = movePositon;
	}
	
	public static void LocalPositionAtY(GameObject target, float y)
	{
		Vector3 movePositon = new Vector3(originalPosition(target).x, y, originalPosition(target).z);
		target.transform.localPosition = movePositon;
	}
	
	public static void LocalPositionAtZ(GameObject target, float z)
	{
		Vector3 movePositon = new Vector3(originalPosition(target).x, originalPosition(target).y, z);
		target.transform.localPosition = movePositon;
	}
	
	public static void LocalPositionAtXY(GameObject target, float x, float y)
	{
		LocalPositionAtX(target, x);
		LocalPositionAtY(target, y);
        }
        
        public static void LocalPositionAtXZ(GameObject target, float x, float z)
        {
            LocalPositionAtX(target, x);
            LocalPositionAtZ(target, z);
        }
        
        public static void LocalPositionAtYZ(GameObject target, float y, float z)
        {
            LocalPositionAtY(target, y);
            LocalPositionAtZ(target, z);
        }
        
        public static void AddLocalPositionX(GameObject target, float value)
        {
            LocalPositionAtX(target, originalPosition(target).x + value);
        }
        
        public static void AddLocalPositionY(GameObject target, float value)
        {
            LocalPositionAtY(target, originalPosition(target).y + value);
        }
        
        public static void AddLocalPositionZ(GameObject target, float value)
        {
            LocalPositionAtZ(target, originalPosition(target).z + value);
        }
        
        public static Vector2 ConvertPositionWorldToLocal(Vector2 worldPosition, GameObject baseObject)
        {
            return baseObject.transform.InverseTransformPoint(worldPosition);
        }
        
        public static Vector3 ConvertPositionWorldToLocal(Vector3 worldPosition, GameObject baseObject)
        {
            return baseObject.transform.InverseTransformPoint(worldPosition);
        }
        
        public static Vector2 ConvertPositionLocalToWorld(Vector2 localPosition, GameObject baseObject)
        {
            return baseObject.transform.TransformPoint(localPosition);
        }
        
        public static Vector3 ConvertPositionLocalToWorld(Vector3 localPosition, GameObject baseObject)
        {
            return baseObject.transform.TransformPoint(localPosition);
        }
        
        public static Vector2 RandomVector2(Vector2 min, Vector2 max)
        {
            float x = UnityEngine.Random.Range(min.x, max.x);
            float y = UnityEngine.Random.Range(min.y, max.y);
            
            return new Vector2(x, y);
        }
        
        static Vector3 originalPosition(GameObject target)
        {
            return target.transform.localPosition;
        }
    }
