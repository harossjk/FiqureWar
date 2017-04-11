using UnityEngine;
using System.Collections;
using System;
public class Sight_Collider_Check : MonoBehaviour
{
	private c_ControllerList m_controllerList;

	private GameObject m_unitObject;
	private Creature_p m_unitType;
	private const int HoldZ = -100;

	private Vector3 m_enemeyPosition;
	private Vector3 m_userPosition;
	public Vector3 GetSightNowEnemeyPosition()
	{
		return m_enemeyPosition;
	}
	public Vector3 GetSightNoewUserPosition()
	{
		return m_userPosition;
	}


	void Start()
	{
		m_controllerList = GameObject.Find("GameControllerManager").GetComponent<c_ControllerList>();

		m_unitObject = transform.parent.gameObject;
		if (m_unitObject == null) return;
		m_unitType = m_unitObject.transform.GetComponent<Creature_p>();
		if (m_unitType == null) return;

		m_controllerList.GetEventController().OnCollisionEvent += SightCollisionEventCheck;
	}

	void OnTriggerEnter(Collider col)
	{
		SightStatusType(col);
	}

	//Type setting function that moves the unit from  sight to range
	private void SightStatusType(Collider col)
	{
		if (col.gameObject.tag.Equals("Enemy_Sight")) // user stop
		{
			if (m_unitType.GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_USER))
			{
				m_controllerList.GetEventController().SendCollisionEvent(m_unitObject, col.gameObject);
			}
		}
		else if(col.gameObject.tag.Equals("User_Sight")) // enemy stop
		{
			
			if (m_unitType.GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_ENEMY))
			{

				m_controllerList.GetEventController().SendCollisionEvent(m_unitObject, col.gameObject);
			}
		}

		//else if (m_unitType.GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_USER_HERO))
		//{
		//	c_ControllerList.c_eventHandler.SendCollisionEvent(m_unitObject, col.gameObject);
		//}
	}


	//sight event Occur
	public void SightCollisionEventCheck(GameObject targetObj, GameObject colObj)
	{
		if (targetObj.GetComponent<Creature_p>().GetUniqueIndex() == m_unitObject.GetComponent<Creature_p>().GetUniqueIndex())
		{
			if (colObj.tag == "Enemy_Sight")
			{
				m_enemeyPosition = colObj.transform.position;
				m_unitType.SetStatusType(CommonTypes.StatusType.STATUS_TYPE_SIGHTMOVE);
				m_unitType.SetCollisionType(CommonTypes.CollisionType.COLLISION_TYPE_SIGHTCOLLISION);
			}
			else if(colObj.tag == "User_Sight")
			{

				m_userPosition = colObj.transform.position;
				m_unitType.SetStatusType(CommonTypes.StatusType.STATUS_TYPE_SIGHTMOVE);
				m_unitType.SetCollisionType(CommonTypes.CollisionType.COLLISION_TYPE_SIGHTCOLLISION);
			}
			//else if (targetObj.gameObject.GetComponent<Creature_p>().GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_USER_HERO))
			//{
			//	MapCreater m_mapcreater = FindObjectOfType(typeof(MapCreater)) as MapCreater;
			//	if (m_mapcreater == null) return;
			//	m_unitType.SetGameStatusType(CommonTypes.GameStatusType.GAMESTATUS_TYPE_PAUSE);
			//	if (m_mapcreater.GetIsHeroMakeCheck()) return;
			//	m_mapcreater.HeroMapTile();
			//}
		}
	}

	void OnDestroy()
	{
		m_controllerList.GetEventController().OnCollisionEvent -= SightCollisionEventCheck;
	}
}
