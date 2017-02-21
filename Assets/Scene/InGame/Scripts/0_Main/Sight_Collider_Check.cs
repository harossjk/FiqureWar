using UnityEngine;
using System.Collections;
using System;
public class Sight_Collider_Check : MonoBehaviour
{
	private GameObject m_unitObject;
	private Creature_p m_unitType;

	private GameObject m_prevUserTransFormPosion;
	private GameObject m_prevEnemyTransFormPosion;
	private CommonTypes.CollisionType m_prevCollisionType;

	public CommonTypes.CollisionType GetPrevCollisionType() { return m_prevCollisionType; }
	public GameObject GetPrevUserSightTransFormPosion() { return m_prevUserTransFormPosion; }
	public GameObject GetPrevEnemySightTransFormPosion() { return m_prevEnemyTransFormPosion; }
	private const int HoldZ = -100;

	void Start()
	{
		m_unitObject = transform.parent.gameObject;
		if (m_unitObject == null) return;
		m_unitType = m_unitObject.transform.GetComponent<Creature_p>();
		if (m_unitType == null) return;
		c_ControllerList.c_eventHandler.OnCollisionEvent += SightCollisionEventCheck;
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
				m_unitType.SetStatusType(CommonTypes.StatusType.STATUS_TYPE_SIGHTMOVE);
				m_unitType.SetCollisionType(CommonTypes.CollisionType.COLLISION_TYPE_SIGHTCOLLISION);
				c_ControllerList.c_eventHandler.SendCollisionEvent(m_unitObject, col.gameObject);
			}
			else if (m_unitType.GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_USER_HERO))
			{
				c_ControllerList.c_eventHandler.SendCollisionEvent(m_unitObject, col.gameObject);
			}
		}

		else if (col.gameObject.tag.Equals("User_Sight")) // enemy stop
		{
			if (m_unitType.GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_ENEMY))
			{
				m_unitType.SetStatusType(CommonTypes.StatusType.STATUS_TYPE_SIGHTMOVE);
				m_unitType.SetCollisionType(CommonTypes.CollisionType.COLLISION_TYPE_SIGHTCOLLISION);
				c_ControllerList.c_eventHandler.SendCollisionEvent(m_unitObject, col.gameObject);
			}
			else if (m_unitType.GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_ENEMY_HERO))
			{
				c_ControllerList.c_eventHandler.SendCollisionEvent(m_unitObject, col.gameObject);
			}
		}
	}

	//sight event Occur
	public void SightCollisionEventCheck(GameObject targetObj, GameObject colObj)
	{
		if (colObj.tag == "Enemy_Sight")
		{
			if (targetObj.gameObject.GetComponent<Creature_p>().GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_USER))
			{
				m_prevEnemyTransFormPosion = targetObj;
				m_prevCollisionType = targetObj.GetComponent<Creature_p>().GetCollisionType();
			}
			else if(targetObj.gameObject.GetComponent<Creature_p>().GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_USER_HERO))
			{
			    MapCreater m_mapcreater = FindObjectOfType(typeof(MapCreater)) as MapCreater;
				if (m_mapcreater == null) return;
				m_unitType.SetGameStatusType(CommonTypes.GameStatusType.GAMESTATUS_TYPE_PAUSE);
				if (m_mapcreater.GetIsHeroMakeCheck()) return;
				m_mapcreater.HeroMapTile();
			}
		}
		else if (colObj.tag == "User_Sight")
		{
			if (targetObj.gameObject.GetComponent<Creature_p>().GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_ENEMY))
			{
				m_prevUserTransFormPosion = targetObj;
				m_prevCollisionType = targetObj.GetComponent<Creature_p>().GetCollisionType();
			}
			else if (targetObj.gameObject.GetComponent<Creature_p>().GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_ENEMY_HERO))
			{
				
			}
		}
	}
}
