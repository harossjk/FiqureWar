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
	private int Mineral;
	private int storage;
	private MapCreater m_mapcreater;

	private Transform m_colObjct;
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

		m_mapcreater = FindObjectOfType(typeof(MapCreater)) as MapCreater;
		if (m_mapcreater == null) return;

		c_controllerList.GetEventController().OnAttackEvent += AttackEvent;
	}

	// Update is called once per frame
	public void Update()
	{
		Debug.Log(m_unitType.name+ ", "+m_unitType.GetStatusType());
		Debug.Log(m_unitType.name + ", " + m_unitType.GetAttackType());

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

		else if (m_unitType.GetStatusType().Equals(CommonTypes.StatusType.STATUS_TYPE_SCV_NONE)
			|| m_unitType.GetStatusType().Equals(CommonTypes.StatusType.STATUS_TYPE_SCV_PLAY))
		{
			ScvMineralMove();
		}
		else if (m_unitType.GetStatusType().Equals(CommonTypes.StatusType.STATUS_TYPE_SCV_STOP))
		{
			ScvWork();
		}

		else if (m_unitType.GetStatusType().Equals(CommonTypes.StatusType.STATUS_TYPE_SCV_BACK))
		{
			ScvStartPosition();
		}


	}

	private void UnitWayPointMove()
	{
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
		else if (m_unitType.GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_USER_HERO)
			&& m_unitType.GetStatusType().Equals(CommonTypes.StatusType.STATUS_TYPE_PLAY))
		{
			m_wayPoint = m_objectList.GetMapGameObject().GetWayPoint(Center);
			if (m_wayPoint == null) return;
		}
		else if (m_unitType.GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_ENEMY_HERO)
			&& m_unitType.GetStatusType().Equals(CommonTypes.StatusType.STATUS_TYPE_PLAY))
		{
			m_wayPoint = m_objectList.GetMapGameObject().GetWayPoint(Center);
			if (m_wayPoint == null) return;

		}
		if (m_unitType.GetGameStatusType().Equals(CommonTypes.GameStatusType.GAMESTATUS_TYPE_PAUSE)) return;

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
			Vector3 targetObject = m_sightColliderCheck.GetSightNowEnemeyPosition();
			float unitMoveSpeed = m_unitObject.GetComponent<HeroUnit_c>().GetUserUnitMoveSpeed();

			Vector3 targetPos = new Vector3(targetObject.x, targetObject.y, HoldZ);
			Vector3 movePos = Vector3.MoveTowards(m_unitObject.transform.position, targetPos, Time.deltaTime * unitMoveSpeed);

			m_unitObject.transform.position = movePos;

		}
		//else if (m_unitType.GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_ENEMY)
		//	&& m_unitType.GetStatusType().Equals(CommonTypes.StatusType.STATUS_TYPE_SIGHTMOVE)
		//	&& m_sightColliderCheck.GetPrevCollisionType().Equals(CommonTypes.CollisionType.COLLISION_TYPE_SIGHTCOLLISION))
		//{
		//	GameObject targetObject = m_sightColliderCheck.GetPrevEnemySightTransFormPosion();
		//	float unitMoveSpeed = m_unitObject.GetComponent<HeroUnit_c>().GetUserUnitMoveSpeed();
		//
		//	Vector3 targetPos = new Vector3(targetObject.transform.position.x, targetObject.transform.position.y, HoldZ);
		//	Vector3 movePos = Vector3.MoveTowards(m_unitObject.transform.position, targetPos, Time.deltaTime * unitMoveSpeed);
		//
		//	m_unitObject.transform.position = movePos;
		//
		//}

	}

	private GameObject m_mineralPoint;
	private void ScvMineralMove()
	{
		if (m_unitType.GetStatusType().Equals(CommonTypes.StatusType.STATUS_TYPE_SCV_NONE))
		{
			int m_mineral_random_point = Random.Range(-1, 3);
			m_mineralPoint = m_objectList.GetMapGameObject().GetMineralPoint(m_mineral_random_point);
			if (m_mineralPoint == null) return;
			m_unitType.SetStatusType(CommonTypes.StatusType.STATUS_TYPE_SCV_PLAY);
		}
		else if (m_unitType.GetStatusType().Equals(CommonTypes.StatusType.STATUS_TYPE_SCV_PLAY))
		{
			Vector3 targetPos = new Vector3(m_mineralPoint.transform.position.x, m_mineralPoint.transform.position.y, m_mineralPoint.transform.position.z);
			Vector3 movePos = Vector3.MoveTowards(m_unitObject.transform.position, targetPos, Time.deltaTime * 30f);
			m_unitObject.transform.position = movePos;
		}

	}
	private void ScvStartPosition()
	{
		Vector3 ScvStartPoint = m_objectList.GetUnitGameObject().GetScvStartPoint();
		Vector3 ScvBackMove = Vector3.MoveTowards(m_unitObject.transform.position, ScvStartPoint, Time.deltaTime * 30f);
		m_unitObject.transform.position = ScvBackMove;

		if (m_unitObject.transform.position == ScvStartPoint)
		{
			m_unitType.SetStatusType(CommonTypes.StatusType.STATUS_TYPE_SCV_ORIGINPOINT);
			storage += Mineral;

			m_objectList.GetMapGameObject().SetMineralStorage(storage);
			storage = 0;
		}
		if (m_unitType.GetStatusType().Equals(CommonTypes.StatusType.STATUS_TYPE_SCV_ORIGINPOINT))
		{
			m_unitType.SetStatusType(CommonTypes.StatusType.STATUS_TYPE_SCV_PLAY);
			Mineral = 0;
		}

	}
	private void ScvWork()
	{

		Mineral++;
		if (Mineral >= 5)
		{
			m_unitType.SetStatusType(CommonTypes.StatusType.STATUS_TYPE_SCV_BACK);


		}
	}



	public void Attack()
	{
		if (m_colObjct == null) return;
		int colObjcetHp = m_colObjct.GetComponent<Creature_p>().GetUserUnitHP();
		int userAttack = m_unitObject.GetComponent<Creature_p>().GetUserUnitAttack();
		m_calHP = colObjcetHp - userAttack;

		if (m_calHP > 0)
		{
			m_calHP -= userAttack;
			m_colObjct.GetComponent<Creature_p>().SetUserUnitHP(m_calHP);
		}
		if (m_calHP == 0)
		{
			int uniqueIndex = m_colObjct.GetComponent<Creature_p>().GetUniqueIndex();
			GameObject destroyTarget = m_objectList.GetUnitGameObject().GetUnitGameObject(uniqueIndex);

			Destroy(destroyTarget);
			m_objectList.GetUnitGameObject().DeletUnitGameObject(uniqueIndex);
			m_unitType.SetStatusType(CommonTypes.StatusType.STATUS_TYPE_PLAY);

		}
	}

	private void AttackEvent(GameObject targetObj, GameObject colObj)
	{
		if (m_unitType == null) return;
		if (targetObj.GetComponent<Creature_p>().GetUniqueIndex() == m_unitObject.GetComponent<Creature_p>().GetUniqueIndex())
		{
			if (m_unitType.GetComponent<Creature_p>().GetStatusType().Equals(CommonTypes.StatusType.STATUS_TYPE_RANGEMOVE)
			&& m_unitType.GetComponent<Creature_p>().GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_USER)
			&& m_unitType.GetComponent<Creature_p>().GetAttackType().Equals(CommonTypes.AttackType.ATTACK_TYPE_ENEMY_ATTACK))
			{
				m_colObjct = colObj.transform.parent;
			}
		}
	}
}

