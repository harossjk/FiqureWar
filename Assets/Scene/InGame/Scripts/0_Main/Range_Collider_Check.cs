using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Range_Collider_Check  : MonoBehaviour{

	private c_ControllerList c_controllerList;
	private GameObject m_unitObject;
	private Creature_p m_unitType;

	private List<Transform> m_EnemyRangeList = new List<Transform>();
	public List<Transform> GetEnemyList()
	{
		return m_EnemyRangeList;
	}

	public void DeletEneyList(int index)
	{
		m_EnemyRangeList.RemoveAt(index);
	}

	private List<Transform> m_UserRangeList = new List<Transform>();
	public List<Transform> GetUserList()
	{
		return m_UserRangeList;
	}
	public void DeletUserList(int index)
	{
		m_UserRangeList.RemoveAt(index);
	}

	void Start()
	{
		c_controllerList = GameObject.Find("GameControllerManager").GetComponent<c_ControllerList>();
		m_unitObject = transform.parent.gameObject;
		if (m_unitObject == null) return;
		m_unitType = m_unitObject.transform.GetComponent<Creature_p>();
		if (m_unitObject == null) return;

		c_controllerList.GetEventController().OnCollisionEvent += RangeCollisionEventCheck;
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag.Equals("Path_Way"))
		{
			WayPointMove(col);
		}
	
		else if (col.gameObject.tag.Equals("Enemy_Player") || col.gameObject.tag.Equals("User_Player"))
		{
			RangeStatusType(col);
		}
	}

	private void WayPointMove(Collider col)
	{
		if (m_unitType.GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_USER))
		{
			int userCurWayPointIndex = m_unitObject.GetComponent<Creature_p>().GetUserCurWayPointIndex();
			userCurWayPointIndex++;
			if (userCurWayPointIndex == 5) return;
			m_unitObject.GetComponent<Creature_p>().SetUserWayPointIndex(userCurWayPointIndex);
		}

		else if (m_unitType.GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_ENEMY))
		{
			int enemyCurWayPoint = m_unitObject.GetComponent<Creature_p>().GetEnemyCurWayPointIndex();
			enemyCurWayPoint--;
			
			m_unitObject.GetComponent<Creature_p>().SetEnemyWayPointIndex(enemyCurWayPoint);
		}
	}
	private void RangeStatusType(Collider col)
	{
		//상태 타입이 sigth 일때도 범위 충돌 체클 해주어야한다.
		if ((m_unitType.GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_USER) && m_unitType.GetStatusType().Equals(CommonTypes.StatusType.STATUS_TYPE_SIGHTMOVE))
		 || (m_unitType.GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_USER) && m_unitType.GetStatusType().Equals(CommonTypes.StatusType.STATUS_TYPE_RANGEMOVE)))
		{
			c_controllerList.GetEventController().SendCollisionEvent(m_unitObject, col.gameObject);
		}
		else if ((m_unitType.GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_ENEMY) && m_unitType.GetStatusType().Equals(CommonTypes.StatusType.STATUS_TYPE_SIGHTMOVE))
			 || (m_unitType.GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_ENEMY) && m_unitType.GetStatusType().Equals(CommonTypes.StatusType.STATUS_TYPE_RANGEMOVE)))
		{
			c_controllerList.GetEventController().SendCollisionEvent(m_unitObject, col.gameObject);
		}
	}

	//range event Occur
	public void RangeCollisionEventCheck(GameObject targetObj, GameObject colObj)
	{
		if (m_unitType == null) return;



		if (targetObj.GetComponent<Creature_p>().GetUniqueIndex() == m_unitObject.GetComponent<Creature_p>().GetUniqueIndex())
		{
			if (targetObj.GetComponent<Creature_p>().GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_USER) && colObj.tag.Equals("Enemy_Player"))
			{
				m_UserRangeList.Add(colObj.transform.parent);
				m_unitType.SetStatusType(CommonTypes.StatusType.STATUS_TYPE_RANGEMOVE);
				m_unitType.SetAttackType(CommonTypes.AttackType.ATTACK_TYPE_ENEMY_ATTACK);
			
			}
		    else if (targetObj.GetComponent<Creature_p>().GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_ENEMY) && colObj.tag.Equals( "User_Player"))
			{
				m_EnemyRangeList.Add(colObj.transform.parent);
				m_unitType.SetStatusType(CommonTypes.StatusType.STATUS_TYPE_RANGEMOVE);
				m_unitType.SetAttackType(CommonTypes.AttackType.ATTACK_TYPE_USER_ATTACK);
			}
		}
	}

	void OnDestroy()
	{
		c_controllerList.GetEventController().OnCollisionEvent -= RangeCollisionEventCheck;
	}
}
