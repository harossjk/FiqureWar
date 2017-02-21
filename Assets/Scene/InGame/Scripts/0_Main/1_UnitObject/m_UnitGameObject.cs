using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class m_UnitGameObject  {

	private static m_UnitGameObject m_unitGameObject = null;
	public static m_UnitGameObject GetInstance()
	{
		if (m_unitGameObject == null)
		{
			m_unitGameObject = new m_UnitGameObject();
		}
		return m_unitGameObject;
	}

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

	public int UnitGameObjectLength()
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

	




}
