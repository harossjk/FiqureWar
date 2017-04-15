using UnityEngine;
using System.Collections;

public class c_HeroController : MonoBehaviour
{

	private c_ControllerList c_controllerList;
	private m_ObjectList m_objectList;
	private GameObject m_heroUnitObject;
	private Creature_p m_heroUnitType;
	private const int HoldZ = -100;
	private GameObject m_wayPoint;
	private const int Center = 5;
	private MapCreater m_mapcreater;



	public void Start()
	{
		c_controllerList = GameObject.Find("GameControllerManager").GetComponent<c_ControllerList>();
		if (c_controllerList == null) return;

		m_objectList = GameObject.Find("GameObjectManger").GetComponent<m_ObjectList>();
		if (m_objectList == null) return;

		m_heroUnitObject = transform.gameObject;
		if (m_heroUnitObject == null) return;

		m_heroUnitType = m_heroUnitObject.transform.GetComponent<Creature_p>();
		if (m_heroUnitType == null) return;

		m_mapcreater = FindObjectOfType(typeof(MapCreater)) as MapCreater;
		if (m_mapcreater == null) return;

	}

	// Update is called once per frame
	public void Update()
	{
		if (m_heroUnitType.GetStatusType().Equals(CommonTypes.StatusType.STATUS_TYPE_PLAY))
		{
			HeroWayPointMove();
		}
	}

	private void HeroWayPointMove()
	{
	
		if (m_heroUnitType.GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_USER_HERO)
			&& m_heroUnitType.GetStatusType().Equals(CommonTypes.StatusType.STATUS_TYPE_PLAY))
		{
			m_wayPoint = m_objectList.GetMapGameObject().GetWayPoint(Center);
			if (m_wayPoint == null) return;
		}
		else if (m_heroUnitType.GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_ENEMY_HERO)
			&& m_heroUnitType.GetStatusType().Equals(CommonTypes.StatusType.STATUS_TYPE_PLAY))
		{
			m_wayPoint = m_objectList.GetMapGameObject().GetWayPoint(Center);
			if (m_wayPoint == null) return;

		}
		if (m_heroUnitType.GetGameStatusType().Equals(CommonTypes.GameStatusType.GAMESTATUS_TYPE_PAUSE)) return;

		float unitMoveSpeed = m_heroUnitObject.GetComponent<HeroUnit_c>().GetUserUnitMoveSpeed();
		Vector3 targetPos = new Vector3(m_wayPoint.transform.position.x, m_wayPoint.transform.position.y, HoldZ);
		Vector3 movePos = Vector3.MoveTowards(m_heroUnitObject.transform.position, targetPos, Time.deltaTime * unitMoveSpeed);
		m_heroUnitObject.transform.position = movePos;
	}

}


	

