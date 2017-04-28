using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class m_UnitGameObject : MonoBehaviour  {

	private Dictionary<int, GameObject> unit_GameObjectList = new Dictionary<int, GameObject>();

	public Dictionary<int, GameObject> GetUnitGameObjectList()
	{
		if (unit_GameObjectList.Count == 0) return null;

		return unit_GameObjectList;
	}


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





	/*
	#region 시야 충돌객체 리스트 담기


	private List<Transform> m_EnemySighttList = new List<Transform>();
	public List<Transform> GetEnemySightList()
	{
		return m_EnemySighttList;
	}
	public void SetSightEnemyAddList(Transform colObj)
	{
		if (colObj == null) return;
		m_EnemySighttList.Add(colObj);
	}
	public void DeletEnemySightList(int index)
	{
		m_EnemySighttList.RemoveAt(index);
	}

	private List<Transform> m_UserSightList = new List<Transform>();
	public List<Transform> GetUserSightList()
	{
		return m_UserSightList;
	}
	public void SetSightUserAddList(Transform colObj)
	{
		if (colObj == null) return;
		m_UserSightList.Add(colObj);
	}
	public void DeletUserSightList(int index)
	{
		m_UserSightList.RemoveAt(index);
	}


	



	private List<Transform> m_EnemyRangeList = new List<Transform>();
	public List<Transform> GetEnemyRangeList()
	{
		return m_EnemyRangeList;
	}
	public void SetRangeEnemyAddList(Transform colObj)
	{
		if (colObj == null) return;
		m_EnemyRangeList.Add(colObj);
	}
	public void DeletEnemyRangeList(int index)
	{
		m_EnemyRangeList.RemoveAt(index);
	}

	private List<Transform> m_UserRangeList = new List<Transform>();
	public List<Transform> GetUserRangeList()
	{
		return m_UserRangeList;
	}
	public void SetRangeUserAddList(Transform colObj)
	{
		if (colObj == null) return;
		m_UserRangeList.Add(colObj);
	}
	public void DeletUserRange(int index)
	{
		m_UserRangeList.RemoveAt(index);
	}
	#endregion
	*/

}
