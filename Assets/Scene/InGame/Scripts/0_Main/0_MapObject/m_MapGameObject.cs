using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class m_MapGameObject  {

	private static m_MapGameObject m_mapGameObject = null;
	public static m_MapGameObject GetInstance()
	{
		if (m_mapGameObject == null)
		{
			m_mapGameObject = new m_MapGameObject();
		}
		return m_mapGameObject;
	}

	#region map game object list
	private Dictionary<int, GameObject> m_mapGameObjectList = new Dictionary<int, GameObject>();
	public GameObject GetMapGameObject(int unique_id)
	{
		GameObject objChacker;
		if (!m_mapGameObjectList.TryGetValue(unique_id, out objChacker))
		{
			return null;
		}
		return m_mapGameObjectList[unique_id];
	}
	public void SetMapGameObject(int unique_id ,ref GameObject obj)
	{
		if (obj == null) return;
		m_mapGameObjectList.Add(unique_id, obj);
	}
	#endregion

	#region map tile object list
	private List<GameObject> m_mapTileList = new List<GameObject>();
	public void SetMapTile(ref GameObject tile)
	{
		if (tile == null) return;

		m_mapTileList.Add(tile);
	}
	public GameObject GetMapTile(int index)
	{
		if (m_mapTileList.Count == 0 || (index < 0 || m_mapTileList.Count <= index))
		{
			return null;
		}
		return m_mapTileList[index];
	}
	#endregion


	private GameObject m_heroMapObject = new GameObject();
	public void SetHeroMapTile(ref GameObject obj)
	{
		if (obj == null) return;
		m_heroMapObject = obj;
	}
	public GameObject GetHeroMapTile()
	{
		return m_heroMapObject;
	}


	#region map waypoint list 
	private List<GameObject> wayPointList = new List<GameObject>();
	public void SetWayPoint(ref GameObject wayPoint)
	{
		if (wayPoint == null) return;

		wayPointList.Add(wayPoint);
	}
	public GameObject GetWayPoint(int index)
	{
		if (wayPointList.Count == 0 || (index < 0 || wayPointList.Count <= index))
		{
			return null;
		}
		return wayPointList[index];
	}
	#endregion

}
