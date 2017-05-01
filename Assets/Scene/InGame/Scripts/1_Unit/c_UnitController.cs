using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class c_UnitController : MonoBehaviour
{
	private c_ControllerList c_controllerList;
	private m_ObjectList m_objectList;

	private GameObject m_unitObject;
	private Creature_p m_unitType;
	private const int HoldZ = -100;
	private GameObject m_wayPoint;
	private const int Center = 5;


	private int m_calHP = 0;
	private int m_ListLastNum = 0;
	private Sight_Collider_Check sightCollsion;

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

		sightCollsion = m_unitObject.transform.FindChild("0_Sight").GetComponent<Sight_Collider_Check>();
		if (sightCollsion == null) return;
	}

	// Update is called once per frame
	public void Update()
	{
		//nomal waypoint move 
		UnitToWayPointMove();
		First_SightToRangeMove();
	}

	private void UnitToWayPointMove()
	{
		if (m_unitType.GetStatusType().Equals(CommonTypes.StatusType.STATUS_TYPE_PLAY))
		{
			if (m_unitType.GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_USER))
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
			else if (m_unitType.GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_ENEMY))
			{
				int Enemy_curWayPointIndex = m_unitObject.GetComponent<Creature_p>().GetEnemyCurWayPointIndex();

				if (Enemy_curWayPointIndex == 4)
				{
					Enemy_curWayPointIndex--;
					m_unitObject.GetComponent<Creature_p>().SetEnemyWayPointIndex(Enemy_curWayPointIndex);
				}
				m_wayPoint = m_objectList.GetMapGameObject().GetWayPoint(Enemy_curWayPointIndex);
				if (m_wayPoint == null) return;
			}
			Vector3 targetPos = new Vector3(m_wayPoint.transform.position.x, m_wayPoint.transform.position.y, HoldZ);
			Moving(targetPos);
		}
	}
	private bool First_SightToRangeMove()
	{
		if (m_unitType.GetStatusType().Equals(CommonTypes.StatusType.STATUS_TYPE_PAUSE)
		&& sightCollsion.First_SightColliderCheck())
		{
			List<Transform> sightList = null;
			const int arrayFirstIndex = 0;

			if (m_unitType.GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_USER))
			{
				sightList = sightCollsion.GetUserSightList();
				Vector3 targetPos = sightList[arrayFirstIndex].transform.position;
				Moving(targetPos);
				return true;
			}
			else if (m_unitType.GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_ENEMY))
			{
				sightList = sightCollsion.GetEnemySightList();
				Vector3 targetPos = sightList[arrayFirstIndex].transform.position;
				Moving(targetPos);
				return true;
			}
		}

			


		return true;
	}
	private void Moving(Vector3 targetPos)
	{
		Vector3 prevTargetPos = new Vector3(targetPos.x, targetPos.y, HoldZ);
		float currentSpeed = m_unitObject.GetComponent<HeroUnit_c>().GetUserUnitMoveSpeed();
		Vector3 deltaPostion = Vector3.MoveTowards(m_unitObject.transform.position, prevTargetPos, Time.deltaTime * currentSpeed);
		m_unitObject.transform.position = deltaPostion;
	}



	/*
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

			if (Enemy_curWayPointIndex == 4)
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



	private void UnitSightMove(CommonTypes.MinionTeam type) 
	{
		Transform nowSightObj = null;
		List<Transform> colObjSightList =null;

		if (type.Equals(CommonTypes.MinionTeam.MINION_TEAM_USER))
		{
			nowSightObj = m_unitObject.transform.gameObject.transform.FindChild("0_Sight");
			if (nowSightObj == null) return;

			colObjSightList = nowSightObj.GetComponent<Sight_Collider_Check>().GetUserList();
			if (colObjSightList == null) return;
		}
		else if (type.Equals(CommonTypes.MinionTeam.MINION_TEAM_ENEMY))
		{
			nowSightObj = m_unitObject.transform.gameObject.transform.FindChild("0_Sight");
			if (nowSightObj == null) return;
		
			colObjSightList = nowSightObj.GetComponent<Sight_Collider_Check>().GetEnemyList();
			if (colObjSightList == null) return;
		}

		if (ListNullOrValueCheck(colObjSightList).Equals(false))
		{
			m_unitType.SetStatusType(CommonTypes.StatusType.STATUS_TYPE_PLAY);
			return;
		}

		if (m_unitType.GetAttackType().Equals(CommonTypes.AttackType.ATTACK_TYPE_USER_ATTACK) 
		||  m_unitType.GetAttackType().Equals(CommonTypes.AttackType.ATTACK_TYPE_ENEMY_ATTACK)) return;

		Vector3 targetObject = colObjSightList[m_ListLastNum].transform.position;

		float unitMoveSpeed = m_unitObject.GetComponent<HeroUnit_c>().GetUserUnitMoveSpeed();
		Vector3 targetPos = new Vector3(targetObject.x, targetObject.y, HoldZ);
		Vector3 movePos = Vector3.MoveTowards(m_unitObject.transform.position, targetPos, Time.deltaTime * unitMoveSpeed);

		m_unitObject.transform.position = movePos;
	}

	private void Attack(CommonTypes.MinionTeam type)
	{
		Transform nowObj = null;
		List<Transform> colObjList = null;

		if (type.Equals(CommonTypes.MinionTeam.MINION_TEAM_USER))
		{
			nowObj = m_unitObject.transform.gameObject.transform.FindChild("1_Range");
			if (nowObj == null) return;
		
			colObjList = nowObj.GetComponent<Range_Collider_Check>().GetUserList();
			if (colObjList == null) return;
		}
		else if (type.Equals(CommonTypes.MinionTeam.MINION_TEAM_ENEMY))
		{
			nowObj = m_unitObject.transform.gameObject.transform.FindChild("1_Range");
			if (nowObj == null) return;
		
			colObjList = nowObj.GetComponent<Range_Collider_Check>().GetEnemyList();
			if (colObjList == null) return;
		}

		if(ListNullOrValueCheck(colObjList).Equals(false))
		{
			m_unitType.SetStatusType(CommonTypes.StatusType.STATUS_TYPE_SIGHTMOVE);
			return;
		}
		

		int colObjcetHp = colObjList[m_ListLastNum].GetComponent<Creature_p>().GetUserUnitHP();
		int userAttack = m_unitObject.GetComponent<Creature_p>().GetUserUnitAttack();
		m_calHP = colObjcetHp - userAttack;

		GameObject hpobject = colObjList[m_ListLastNum].transform.FindChild("Hp_Bar").transform.gameObject;
		if (hpobject == null) return;

		Slider HpBar = hpobject.transform.GetComponent<Slider>();
		if (HpBar == null) return;

		if (m_calHP > 0)
		{
			m_calHP -= userAttack;
			colObjList[m_ListLastNum].GetComponent<Creature_p>().SetUserUnitHP(m_calHP);
			HpBar.value = Mathf.MoveTowards(colObjcetHp, m_calHP, 1);
		}
		if (m_calHP == 0)
		{
			DestroyObjcet(colObjList, type);
		}
	}

	


	private void DestroyObjcet(List<Transform> colObjList , CommonTypes.MinionTeam type)
	{
		int uniqueIndex = colObjList[m_ListLastNum].GetComponent<Creature_p>().GetUniqueIndex();
		GameObject destroyTarget = m_objectList.GetUnitGameObject().GetUnitGameObject(uniqueIndex);
		m_objectList.GetUnitGameObject().DeletUnitGameObject(uniqueIndex);
		GameObjectAllSearch_OR_TypeChange(colObjList);
		Destroy(destroyTarget);
	}

	private void GameObjectAllSearch_OR_TypeChange(List<Transform> colObjList)
	{
		Dictionary<int, GameObject> unitList = m_objectList.GetUnitGameObject().GetUnitGameObjectList();
		foreach (KeyValuePair<int, GameObject> kv in unitList)
		{
			kv.Value.gameObject.transform.FindChild("0_Sight").GetComponent<Sight_Collider_Check>().DeletUserList(m_ListLastNum);
			kv.Value.gameObject.transform.FindChild("1_Range").GetComponent<Range_Collider_Check>().DeletUserList(m_ListLastNum);
			
			if (colObjList.Count > 0)
			{
				kv.Value.gameObject.GetComponent<Creature_p>().SetStatusType(CommonTypes.StatusType.STATUS_TYPE_RANGEMOVE);
				kv.Value.gameObject.GetComponent<Creature_p>().SetAttackType(CommonTypes.AttackType.ATTACK_TYPE_ENEMY_ATTACK);
			}
			else if (colObjList.Count == 0)
			{
				kv.Value.gameObject.GetComponent<Creature_p>().SetStatusType(CommonTypes.StatusType.STATUS_TYPE_SIGHTMOVE);
				kv.Value.gameObject.GetComponent<Creature_p>().SetAttackType(CommonTypes.AttackType.ATTACK_TYPE_NONE);
			}
			kv.Value.gameObject.GetComponent<Creature_p>().SetGameStatusType(CommonTypes.GameStatusType.GAMESTATUS_TYPE_NONE);
		}

		/*

		if (m_unitObject.GetComponent<Creature_p>().GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_USER))
		{
			unitList = m_objectList.GetUnitGameObject().GetUnitGameObjectList();
			foreach (KeyValuePair<int, GameObject> kv in unitList)
			{
				kv.Value.gameObject.transform.FindChild("0_Sight").GetComponent<Sight_Collider_Check>().DeletUserList(m_ListLastNum);
				kv.Value.gameObject.transform.FindChild("1_Range").GetComponent<Range_Collider_Check>().DeletUserList(m_ListLastNum);

				if (colObjList.Count > 0)
				{
					kv.Value.gameObject.GetComponent<Creature_p>().SetStatusType(CommonTypes.StatusType.STATUS_TYPE_RANGEMOVE);
					kv.Value.gameObject.GetComponent<Creature_p>().SetAttackType(CommonTypes.AttackType.ATTACK_TYPE_ENEMY_ATTACK);
				}
				else if (colObjList.Count == 0)
				{
					kv.Value.gameObject.GetComponent<Creature_p>().SetStatusType(CommonTypes.StatusType.STATUS_TYPE_SIGHTMOVE);
					kv.Value.gameObject.GetComponent<Creature_p>().SetAttackType(CommonTypes.AttackType.ATTACK_TYPE_NONE);
				}
				kv.Value.gameObject.GetComponent<Creature_p>().SetGameStatusType(CommonTypes.GameStatusType.GAMESTATUS_TYPE_NONE);
			}

		}
		else if (m_unitObject.GetComponent<Creature_p>().GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_ENEMY))
		{
			unitList = m_objectList.GetUnitGameObject().GetUnitGameObjectList();
			foreach (KeyValuePair<int, GameObject> kv in unitList)
			{
				kv.Value.gameObject.transform.FindChild("0_Sight").GetComponent<Sight_Collider_Check>().DeletUserList(m_ListLastNum);
				kv.Value.gameObject.transform.FindChild("1_Range").GetComponent<Range_Collider_Check>().DeletUserList(m_ListLastNum);

				if (colObjList.Count > 0)
				{
					kv.Value.gameObject.GetComponent<Creature_p>().SetStatusType(CommonTypes.StatusType.STATUS_TYPE_RANGEMOVE);
					kv.Value.gameObject.GetComponent<Creature_p>().SetAttackType(CommonTypes.AttackType.ATTACK_TYPE_USER_ATTACK);
				}
				else if (colObjList.Count == 0)
				{
					kv.Value.gameObject.GetComponent<Creature_p>().SetStatusType(CommonTypes.StatusType.STATUS_TYPE_SIGHTMOVE);
					kv.Value.gameObject.GetComponent<Creature_p>().SetAttackType(CommonTypes.AttackType.ATTACK_TYPE_NONE);
				}

				kv.Value.gameObject.GetComponent<Creature_p>().SetGameStatusType(CommonTypes.GameStatusType.GAMESTATUS_TYPE_NONE);
			}
		}
		
	}

	private bool ListNullOrValueCheck(List<Transform> colObjList)
	{
		if (colObjList == null || colObjList.Count == 0) return false;
		else
		{
			foreach (Transform i in colObjList)
			{
				if (i == null) return false;
			}
		}
		return true;
	}
	*/
}

