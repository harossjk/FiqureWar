using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class m_UnitGameObject : MonoBehaviour  {

	private Dictionary<int, GameObject> unit_GameObjectList = new Dictionary<int, GameObject>();
	public GameObject GetUnitGameObject(int unique_id)
	{
		GameObject objChacker;
		if (!unit_GameObjectList.TryGetValue(unique_id, out objChacker))
		{
			return null;
		}
		return unit_GameObjectList[unique_id];
	}
	public void SetUnitGameObject(int unique_id, ref GameObject obj)
	{
		if (obj == null)
		{
			return;
		}
		unit_GameObjectList.Add(unique_id, obj);
	}
	public void DeletUnitGameObject(int unique_id)
	{
		unit_GameObjectList.Remove(unique_id);
	}

	public int GetUnitGameObjectLength()
	{
		return unit_GameObjectList.Count;
	}








	private string MiniGameCheck;
	public string GetMiniGameCheck()
	{
		return MiniGameCheck;
	}
	public void SetMiniGameCheck(string Type)
	{
		MiniGameCheck = Type;
	}

	private Vector3 ScvStartPoint;
	public void SetScvStartPoint(Vector3 Position)
	{
		ScvStartPoint = Position;
	}

	public Vector3 GetScvStartPoint()
	{
		return ScvStartPoint;
	}

}
