using UnityEngine;
using System.Collections;

public class Range_Collider_Check  : MonoBehaviour{

	private c_ControllerList c_controllerList;
	private GameObject m_unitObject;
	private Creature_p m_unitType;
	
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
		if (col.gameObject.tag.Equals("Enemy_Player"))
		{
			if (m_unitType.GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_USER)
				&& m_unitType.GetStatusType().Equals(CommonTypes.StatusType.STATUS_TYPE_SIGHTMOVE)
				&& m_unitType.GetCollisionType().Equals(CommonTypes.CollisionType.COLLISION_TYPE_SIGHTCOLLISION))
			{
				c_controllerList.GetEventController().SendCollisionEvent(m_unitObject, col.gameObject);
			}
	
		}
		else if(col.gameObject.tag.Equals("User_Player"))
		{
			if (m_unitType.GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_ENEMY)
				&& m_unitType.GetStatusType().Equals(CommonTypes.StatusType.STATUS_TYPE_SIGHTMOVE)
				&& m_unitType.GetCollisionType().Equals(CommonTypes.CollisionType.COLLISION_TYPE_SIGHTCOLLISION))
			{

				c_controllerList.GetEventController().SendCollisionEvent(m_unitObject, col.gameObject);
			}
		}
	}

	//range event Occur
	public void RangeCollisionEventCheck(GameObject targetObj, GameObject colObj)
	{
		if (m_unitType == null) return;
		else if (targetObj.GetComponent<Creature_p>().GetUniqueIndex() == m_unitObject.GetComponent<Creature_p>().GetUniqueIndex())
		{
			if (targetObj.GetComponent<Creature_p>().GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_USER) && colObj.tag == "Enemy_Player")
			{

				m_unitType.SetStatusType(CommonTypes.StatusType.STATUS_TYPE_RANGEMOVE);
				m_unitType.SetCollisionType(CommonTypes.CollisionType.COLLISION_TYPE_RANGECOLLISION);
				m_unitType.SetAttackType(CommonTypes.AttackType.ATTACK_TYPE_ENEMY_ATTACK);
				c_controllerList.GetEventController().SendAttackEvent(m_unitObject, colObj.gameObject);
			}
			else if (targetObj.GetComponent<Creature_p>().GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_ENEMY) && colObj.tag == "User_Player")
			{

				m_unitType.SetStatusType(CommonTypes.StatusType.STATUS_TYPE_RANGEMOVE);
				m_unitType.SetCollisionType(CommonTypes.CollisionType.COLLISION_TYPE_RANGECOLLISION);
				m_unitType.SetAttackType(CommonTypes.AttackType.ATTACK_TYPE_USER_ATTACK);
				c_controllerList.GetEventController().SendAttackEvent(m_unitObject, colObj.gameObject);
			}
		}
	}

	void OnDestroy()
	{
		c_controllerList.GetEventController().OnCollisionEvent -= RangeCollisionEventCheck;
	}
}
