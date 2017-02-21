using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour {

	private int m_id;
	public void SetId(int index) { m_id = index; }
	public int GetId() { return m_id; }

	private CommonTypes.eTileType m_type;
	public void SetTileType(CommonTypes.eTileType type) { m_type = type; }
	public CommonTypes.eTileType GetTileType() { return m_type; }

	private List<Vector3> m_wayPointList = new List<Vector3>();

	public void SetWayPoint(Vector3 wayList)
	{
		if (wayList == null) return;
		m_wayPointList.Add(wayList);
	}
	public Vector3 GetWayPoint(int index)
	{
		if (m_wayPointList.Count == 0)
		{
			return Vector3.zero;
		}

		return m_wayPointList[index];
	}
	public int WayPointCount()
	{
		return m_wayPointList.Count;
	}

}
