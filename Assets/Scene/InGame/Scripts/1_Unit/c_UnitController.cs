using UnityEngine;
using System.Collections;

public class c_UnitController : MonoBehaviour
{
	private static c_UnitController c_unitController = null;
	public static c_UnitController GetInstance()
	{
		if (null == c_unitController)
		{
			c_unitController = FindObjectOfType(typeof(c_UnitController)) as c_UnitController;
		}
		return c_unitController;
	}

	private GameObject m_unitObject;
	private const int HoldZ = -100;

	private Creature_p m_unitType;
	private Sight_Collider_Check m_sightColliderCheck;
	private Range_Collider_Check m_rangeColliderCheck;

	private GameObject m_wayPoint;
	private const int Center = 5;

	private MapCreater m_mapcreater;
	private bool isHeroMapNullCheck = false;
	public void SetIsHeroMapNullCheck(bool type) { isHeroMapNullCheck = type; }

	private int m_userHp;
	private int m_userAttack;
	private int m_userCurhp;

	private int m_enemyHp;
	private int m_enemyAttack;
	private int m_enemyCurhp;


	int m_useruniqueid = 0;
	int m_enemyuniqueid = 0;


	void Start()
	{
		m_unitObject = transform.gameObject;
		if (m_unitObject == null) return;

		m_unitType = m_unitObject.GetComponent<Creature_p>();
		if (m_unitType == null) return;

		m_sightColliderCheck = FindObjectOfType(typeof(Sight_Collider_Check)) as Sight_Collider_Check;
		if (m_sightColliderCheck == null) return;

		m_mapcreater = FindObjectOfType(typeof(MapCreater)) as MapCreater;
		if (m_mapcreater == null) return;

		m_rangeColliderCheck = FindObjectOfType(typeof(Range_Collider_Check)) as Range_Collider_Check;
		if (m_rangeColliderCheck == null) return;
		//c_ControllerList.c_eventHandler.OnCollisionEvent += Attack;
	}

	// Update is called once per frame
	void Update()
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
		else if (m_unitType.GetStatusType().Equals(CommonTypes.StatusType.STATUS_TYPE_ATTACK)
			&& m_unitType.GetCollisionType().Equals(CommonTypes.CollisionType.COLLISION_TYPE_RANGECOLLISION))
		{
			Attack();
		}

		if (m_ObjectList.m_mapGameObject.GetHeroMapTile() == null && m_mapcreater.GetIsHeroMakeCheck().Equals(false))
		{
			m_unitType.SetGameStatusType(CommonTypes.GameStatusType.GAMESTATUS_TYPE_NONE);
		}
		
	}


	private void UnitWayPointMove()
	{
		if (m_unitType.GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_USER))
		{
			int curWayPointIndex = m_unitObject.GetComponent<Creature_p>().GetUserCurWayPointIndex();

			if (curWayPointIndex == -1)
			{
				curWayPointIndex++;
				m_unitObject.GetComponent<Creature_p>().SetUserWayPointIndex(curWayPointIndex);
			}
			m_wayPoint = m_ObjectList.m_mapGameObject.GetWayPoint(curWayPointIndex);
			if (m_wayPoint == null) return;
		}
		if (m_unitType.GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_ENEMY))
		{
			int Enemy_curWayPointIndex = m_unitObject.GetComponent<Creature_p>().GetEnemyCurWayPointIndex();

			if (Enemy_curWayPointIndex == 5)
			{
				Enemy_curWayPointIndex--;
				m_unitObject.GetComponent<Creature_p>().SetEnemyWayPointIndex(Enemy_curWayPointIndex);
			}
			m_wayPoint = m_ObjectList.m_mapGameObject.GetWayPoint(Enemy_curWayPointIndex);
			if (m_wayPoint == null) return;
		}
		if (m_unitType.GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_USER_HERO))
		{
			m_wayPoint = m_ObjectList.m_mapGameObject.GetWayPoint(Center);
			if (m_wayPoint == null) return;
		}
		if (m_unitType.GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_ENEMY_HERO))
		{
			m_wayPoint = m_ObjectList.m_mapGameObject.GetWayPoint(Center);
			if (m_wayPoint == null) return;

		}
		if ( m_unitType.GetGameStatusType().Equals(CommonTypes.GameStatusType.GAMESTATUS_TYPE_PAUSE)) return;

		float unitMoveSpeed = m_unitObject.GetComponent<HeroUnit_c>().GetUserUnitMoveSpeed();
		Vector3 targetPos = new Vector3(m_wayPoint.transform.position.x, m_wayPoint.transform.position.y, HoldZ);
		Vector3 movePos = Vector3.MoveTowards(m_unitObject.transform.position, targetPos, Time.deltaTime * unitMoveSpeed);
		m_unitObject.transform.position = movePos;
	}




	private void UnitSightMove() // sight to Range 
	{
		if (m_unitType.GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_USER)
		  && m_sightColliderCheck.GetPrevCollisionType().Equals(CommonTypes.CollisionType.COLLISION_TYPE_SIGHTCOLLISION))
		{
			m_useruniqueid = m_unitType.GetUniqueIndex();

			GameObject targetObject = m_sightColliderCheck.GetPrevUserSightTransFormPosion();
			float unitMoveSpeed = m_unitObject.GetComponent<HeroUnit_c>().GetUserUnitMoveSpeed();

			Vector3 targetPos = new Vector3(targetObject.transform.position.x, targetObject.transform.position.y, HoldZ);
			Vector3 movePos = Vector3.MoveTowards(m_unitObject.transform.position, targetPos, Time.deltaTime * unitMoveSpeed);

			m_unitObject.transform.position = movePos;


		}
		else if (m_unitType.GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_ENEMY) &&
			m_sightColliderCheck.GetPrevCollisionType().Equals(CommonTypes.CollisionType.COLLISION_TYPE_SIGHTCOLLISION))
		{
			m_enemyHp = m_unitType.GetUserUnitHP();
			print("enemy hp : "+m_enemyHp);
			m_enemyuniqueid = m_unitType.GetUniqueIndex();
			print("enemy id : " + m_enemyuniqueid);

			GameObject targetObject = m_sightColliderCheck.GetPrevEnemySightTransFormPosion();
			float unitMoveSpeed = m_unitObject.GetComponent<HeroUnit_c>().GetUserUnitMoveSpeed();

			Vector3 targetPos = new Vector3(targetObject.transform.position.x, targetObject.transform.position.y, HoldZ);
			Vector3 movePos = Vector3.MoveTowards(m_unitObject.transform.position, targetPos, Time.deltaTime * unitMoveSpeed);

			m_unitObject.transform.position = movePos;

		}

	}

	private void Attack()
	{

		if (m_unitType.GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_USER))
		{// 유저 가 공격하고 적이 파괴됨

			int userAttack = m_unitType.GetUserUnitAttack();
			int enemyCurhp = m_enemyHp - userAttack;

			print("enemy hp : " + m_enemyHp);
			print("enemy id : " + m_enemyuniqueid);

			if (m_enemyHp > 0)
			{
				m_enemyHp -= userAttack;
			}
			else if (m_enemyHp == 0)
			{
			
				Destroy(m_ObjectList.m_unitGameObject.GetUnitGameObject(m_enemyuniqueid));
			}
		}
		else if (m_unitType.GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_ENEMY))
		{
		

			// 적이 공격하고 유저가 파괴됨 
			//GameObject targetObject = m_rangeColliderCheck.GetPrevEnemyRangeObject();
			//Debug.Log("MINION_TEAM_ENEMY : " + targetObject.name);


		}


		//hp = m_unitObject.GetComponent<Creature_p>().GetUserUnitHP();
		//attack = m_unitObject.GetComponent<Creature_p>().GetUserUnitAttack();
		//Curhp = hp - attack;
		//
		//m_unitObject.GetComponent<Creature_p>().SetUserUnitHP(Curhp);
		//
		//if (hp > 0)
		//{
		//	int ad = m_unitObject.GetComponent<Creature_p>().GetUserUnitHP();
		//	ad -= attack;
		//}
		//else if (hp == 0)
		//{
		//	Destroy(gameObject);
		//}




	}


}
