using UnityEngine;
using System.Collections;

public class Range_Collider_Check  : MonoBehaviour{

	private GameObject m_unitObject;
	private Creature_p m_unitType;

	void Start()
	{
		m_unitObject = transform.parent.gameObject;
		if (m_unitObject == null) return;
		m_unitType = m_unitObject.transform.GetComponent<Creature_p>();
		if (m_unitObject == null) return;

		c_ControllerList.c_eventHandler.OnCollisionEvent += RangeCollisionEventCheck;
	}

	void OnTriggerEnter(Collider col)
	{
		WayPointMove(col);
		RangeStatusType(col);
	}
	
	private void WayPointMove(Collider col)
	{
		if (col.gameObject.tag.Equals("Path_Way"))
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
	}
	private void RangeStatusType(Collider col)
	{
		if (col.gameObject.tag.Equals("Enemy_Range"))
		{
			if (m_unitType.GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_USER))
			{
				m_unitType.SetStatusType(CommonTypes.StatusType.STATUS_TYPE_RANGEMOVE);
				m_unitType.SetCollisionType(CommonTypes.CollisionType.COLLISION_TYPE_RANGECOLLISION);
				c_ControllerList.c_eventHandler.SendCollisionEvent(m_unitObject, col.gameObject);
				
			}
		}
		else if (col.gameObject.tag.Equals("User_Range"))
		{
			if (m_unitType.GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_ENEMY))
			{
				m_unitType.SetStatusType(CommonTypes.StatusType.STATUS_TYPE_RANGEMOVE);
				m_unitType.SetCollisionType(CommonTypes.CollisionType.COLLISION_TYPE_RANGECOLLISION);
				c_ControllerList.c_eventHandler.SendCollisionEvent(m_unitObject, col.gameObject);
				
			}
		}
	}

	//range event Occur
	public void RangeCollisionEventCheck(GameObject targetObj, GameObject colObj)
	{
		if (colObj.tag == "Enemy_Range")
		{
			if(targetObj.gameObject.GetComponent<Creature_p>().GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_USER))
			{
				targetObj.GetComponent<Creature_p>().SetStatusType(CommonTypes.StatusType.STATUS_TYPE_ATTACK);
			}
		}
		else if (colObj.tag == "User_Range")
		{
			if (targetObj.gameObject.GetComponent<Creature_p>().GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_ENEMY))
			{
				targetObj.GetComponent<Creature_p>().SetStatusType(CommonTypes.StatusType.STATUS_TYPE_ATTACK);
			}
		}

	}
}
