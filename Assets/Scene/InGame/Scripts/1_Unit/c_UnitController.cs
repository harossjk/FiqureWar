using UnityEngine;
using System.Collections;

public class c_UnitController : MonoBehaviour
{
	private c_ControllerList c_controllerList;
	private m_ObjectList m_objectList;
	private GameObject m_unitObject;
	private Creature_p m_unitType;
	private Sight_Collider_Check m_sightColliderCheck;
	private Range_Collider_Check m_rangeColliderCheck;
	private const int HoldZ = -100;
	private GameObject m_wayPoint;
	private const int Center = 5;

	private Transform m_EnemyAttack;
	private Transform m_UserAttack;
	private int m_calHP = 0;
	
	public void Start()
	{
		c_controllerList = GameObject.Find("GameControllerManager").GetComponent<c_ControllerList>();
		if (c_controllerList == null) return;

		m_objectList = GameObject.Find("GameObjectManger").GetComponent<m_ObjectList>();
		if (m_objectList == null) return;

		m_unitObject = transform.gameObject;
		if (m_unitObject == null) return;

		m_unitType = m_unitObject.transform.GetComponent<Creature_p>();
		if (m_unitType == null) return;

		m_sightColliderCheck = FindObjectOfType(typeof(Sight_Collider_Check)) as Sight_Collider_Check;
		if (m_sightColliderCheck == null) return;

		m_rangeColliderCheck = FindObjectOfType(typeof(Range_Collider_Check)) as Range_Collider_Check;
		if (m_rangeColliderCheck == null) return;

		c_controllerList.GetEventController().OnAttackEvent += AttackEvent;
	}

	// Update is called once per frame
	public void Update()
	{
		if (m_unitType.GetStatusType().Equals(CommonTypes.StatusType.STATUS_TYPE_PLAY))
		{
			UnitWayPointMove();
		}
		else if (m_unitType.GetStatusType().Equals(CommonTypes.StatusType.STATUS_TYPE_SIGHTMOVE)
			&& m_unitType.GetCollisionType().Equals(CommonTypes.CollisionType.COLLISION_TYPE_SIGHTCOLLISION))
		{
			UnitSightMove();
		}
		else if (m_unitType.GetStatusType().Equals(CommonTypes.StatusType.STATUS_TYPE_RANGEMOVE)
			&& m_unitType.GetCollisionType().Equals(CommonTypes.CollisionType.COLLISION_TYPE_RANGECOLLISION))
		{
			Attack();
		}
	}

	private void UnitWayPointMove()
	{
		//Transform colObjTarget = m_sightColliderCheck.GetSightTargetObject();
		//Debug.Log(colObjTarget);
		//if (colObjTarget != null)
		//{
		//	m_unitObject.GetComponent<Creature_p>().SetStatusType(CommonTypes.StatusType.STATUS_TYPE_SIGHTMOVE);
		//	m_unitObject.GetComponent<Creature_p>().SetCollisionType(CommonTypes.CollisionType.COLLISION_TYPE_SIGHTCOLLISION);
		//	return;
		//}

		

		if (m_unitType.GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_USER)
			&& m_unitType.GetStatusType().Equals(CommonTypes.StatusType.STATUS_TYPE_PLAY))
		{
			int curWayPointIndex = m_unitObject.GetComponent<Creature_p>().GetUserCurWayPointIndex();

			if (curWayPointIndex == -1)
			{
				curWayPointIndex++;
				m_unitObject.GetComponent<Creature_p>().SetUserWayPointIndex(curWayPointIndex);
			}
			m_wayPoint = m_objectList.GetMapGameObject().GetWayPoint(curWayPointIndex);
			if (m_wayPoint == null) return;
		}
		else if (m_unitType.GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_ENEMY)
			&& m_unitType.GetStatusType().Equals(CommonTypes.StatusType.STATUS_TYPE_PLAY))
		{
			int Enemy_curWayPointIndex = m_unitObject.GetComponent<Creature_p>().GetEnemyCurWayPointIndex();

			if (Enemy_curWayPointIndex == 5)
			{
				Enemy_curWayPointIndex--;
				m_unitObject.GetComponent<Creature_p>().SetEnemyWayPointIndex(Enemy_curWayPointIndex);
			}
			m_wayPoint = m_objectList.GetMapGameObject().GetWayPoint(Enemy_curWayPointIndex);
			if (m_wayPoint == null) return;
		}

		float unitMoveSpeed = m_unitObject.GetComponent<HeroUnit_c>().GetUserUnitMoveSpeed();
		Vector3 targetPos = new Vector3(m_wayPoint.transform.position.x, m_wayPoint.transform.position.y, HoldZ);
		Vector3 movePos = Vector3.MoveTowards(m_unitObject.transform.position, targetPos, Time.deltaTime * unitMoveSpeed);

		m_unitObject.transform.position = movePos;


	}
	private void UnitSightMove() // sight to Range 
	{
		if (m_unitType.GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_USER)
		  && m_unitType.GetStatusType().Equals(CommonTypes.StatusType.STATUS_TYPE_SIGHTMOVE))
		{
			Transform colObjTarget = m_sightColliderCheck.GetSightTargetObject();
			//if (colObjTarget == null)
			//{
			//	m_unitObject.GetComponent<Creature_p>().SetStatusType(CommonTypes.StatusType.STATUS_TYPE_PLAY);
			//	return;
			// }
			
			Vector3 targetObject = colObjTarget.transform.position;
			float unitMoveSpeed = m_unitObject.GetComponent<HeroUnit_c>().GetUserUnitMoveSpeed();

			Vector3 targetPos = new Vector3(targetObject.x, targetObject.y, HoldZ);
			Vector3 movePos = Vector3.MoveTowards(m_unitObject.transform.position, targetPos, Time.deltaTime * unitMoveSpeed);

			m_unitObject.transform.position = movePos;

		}
		else if (m_unitType.GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_ENEMY)
		  && m_unitType.GetStatusType().Equals(CommonTypes.StatusType.STATUS_TYPE_SIGHTMOVE))
		{
			
			Transform colObjTarget = m_sightColliderCheck.GetSightTargetObject();
			//if (colObjTarget == null) 
			//{
			//	m_unitObject.GetComponent<Creature_p>().SetStatusType(CommonTypes.StatusType.STATUS_TYPE_PLAY);
			//	return;
			//}

			Vector3 targetObject = colObjTarget.transform.position;

			float unitMoveSpeed = m_unitObject.GetComponent<HeroUnit_c>().GetUserUnitMoveSpeed();

			Vector3 targetPos = new Vector3(targetObject.x, targetObject.y, HoldZ);
			Vector3 movePos = Vector3.MoveTowards(m_unitObject.transform.position, targetPos, Time.deltaTime * unitMoveSpeed);
 
			m_unitObject.transform.position = movePos;
		}


	}
	public void Attack()
	{
		if (m_unitObject.GetComponent<Creature_p>().GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_USER))
		{
			if (m_EnemyAttack == null)
			{
				m_unitObject.GetComponent<Creature_p>().SetStatusType(CommonTypes.StatusType.STATUS_TYPE_PLAY);
				return;
			}
	
			int colObjcetHp = m_EnemyAttack.GetComponent<Creature_p>().GetUserUnitHP();
			int userAttack = m_unitObject.GetComponent<Creature_p>().GetUserUnitAttack();
			m_calHP = colObjcetHp - userAttack;

			if (m_calHP > 0)
			{
				m_calHP -= userAttack;
				m_EnemyAttack.GetComponent<Creature_p>().SetUserUnitHP(m_calHP);
				
			}
			if (m_calHP == 0)
			{
				int uniqueIndex = m_EnemyAttack.GetComponent<Creature_p>().GetUniqueIndex();
				GameObject destroyTarget = m_objectList.GetUnitGameObject().GetUnitGameObject(uniqueIndex);

				Destroy(destroyTarget);
				m_objectList.GetUnitGameObject().DeletUnitGameObject(uniqueIndex);
		
				m_unitType.SetStatusType(CommonTypes.StatusType.STATUS_TYPE_PLAY);
				m_unitType.SetAttackType(CommonTypes.AttackType.ATTACK_TYPE_NONE);
				m_unitType.SetCollisionType(CommonTypes.CollisionType.COLLISION_TYPE_NONE);
				m_unitType.SetGameStatusType(CommonTypes.GameStatusType.GAMESTATUS_TYPE_NONE);
		

				
			}
		}
		else if (m_unitObject.GetComponent<Creature_p>().GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_ENEMY))
		{
			if (m_UserAttack == null)
			{
				m_unitObject.GetComponent<Creature_p>().SetStatusType(CommonTypes.StatusType.STATUS_TYPE_PLAY);
				return;
			}

			int colObjcetHp = m_UserAttack.GetComponent<Creature_p>().GetUserUnitHP();
			int userAttack = m_unitObject.GetComponent<Creature_p>().GetUserUnitAttack();
			m_calHP = colObjcetHp - userAttack;


			if (m_calHP > 0)
			{
				m_calHP -= userAttack;
				m_UserAttack.GetComponent<Creature_p>().SetUserUnitHP(m_calHP);
			}
			if (m_calHP == 0)
			{
				int uniqueIndex = m_UserAttack.GetComponent<Creature_p>().GetUniqueIndex();
				GameObject destroyTarget = m_objectList.GetUnitGameObject().GetUnitGameObject(uniqueIndex);

				Destroy(destroyTarget);
				m_objectList.GetUnitGameObject().DeletUnitGameObject(uniqueIndex);

				m_unitType.SetStatusType(CommonTypes.StatusType.STATUS_TYPE_PLAY);
				m_unitType.SetAttackType(CommonTypes.AttackType.ATTACK_TYPE_NONE);
				m_unitType.SetCollisionType(CommonTypes.CollisionType.COLLISION_TYPE_NONE);
				m_unitType.SetGameStatusType(CommonTypes.GameStatusType.GAMESTATUS_TYPE_NONE);

		
			}
		}
	}

	private void AttackEvent(GameObject targetObj, GameObject colObj)
	{
		if (m_unitType == null) return;
		else if (targetObj.GetComponent<Creature_p>().GetUniqueIndex() == m_unitObject.GetComponent<Creature_p>().GetUniqueIndex())
		{
			if (m_unitType.GetComponent<Creature_p>().GetStatusType().Equals(CommonTypes.StatusType.STATUS_TYPE_RANGEMOVE)
			&& m_unitType.GetComponent<Creature_p>().GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_USER)
			&& m_unitType.GetComponent<Creature_p>().GetAttackType().Equals(CommonTypes.AttackType.ATTACK_TYPE_ENEMY_ATTACK))
			{
				
				m_EnemyAttack = colObj.transform.parent;

			}
			else if (m_unitType.GetComponent<Creature_p>().GetStatusType().Equals(CommonTypes.StatusType.STATUS_TYPE_RANGEMOVE)
			&& m_unitType.GetComponent<Creature_p>().GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_ENEMY)
			&& m_unitType.GetComponent<Creature_p>().GetAttackType().Equals(CommonTypes.AttackType.ATTACK_TYPE_USER_ATTACK))
			{
				m_UserAttack = colObj.transform.parent;
			}
		}
	}
}

