using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class Sight_Collider_Check : MonoBehaviour
{
	private c_ControllerList c_controllerList;
	private GameObject m_unitObject;
	private Creature_p m_unitType;
	private const int HoldZ = -100;

	private List<Transform> m_EnemySighttList = new List<Transform>();
	public List<Transform> GetEnemyList()
	{
		return m_EnemySighttList;
	}

	public void DeletEneyList(int index)
	{
		m_EnemySighttList.RemoveAt(index);
	}

	private List<Transform> m_UserSightList = new List<Transform>();
	public List<Transform> GetUserList()
	{
		return m_UserSightList;
	}
	public void DeletUserList(int index)
	{
		m_UserSightList.RemoveAt(index);
	}

	void Start()
	{
		c_controllerList = GameObject.Find("GameControllerManager").GetComponent<c_ControllerList>();
		m_unitObject = transform.parent.gameObject;
		if (m_unitObject == null) return;
		m_unitType = m_unitObject.transform.GetComponent<Creature_p>();
		if (m_unitType == null) return;
	
		c_controllerList.GetEventController().OnCollisionEvent += SightCollisionEventCheck;
	}

	void OnTriggerEnter(Collider col)
	{
		SightStatusType(col);
	}

	//Type setting function that moves the unit from  sight to range
	private void SightStatusType(Collider col)
	{
		

		if (col.gameObject.tag.Equals("Enemy_Sight") && m_unitType.GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_USER)) // user stop
		{
		
			c_controllerList.GetEventController().SendCollisionEvent(m_unitObject, col.gameObject);
		}
		else if(col.gameObject.tag.Equals("User_Sight") && m_unitType.GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_ENEMY)) // enemy stop
		{
			c_controllerList.GetEventController().SendCollisionEvent(m_unitObject, col.gameObject);
		}
	}


	//sight event Occur
	public void SightCollisionEventCheck(GameObject targetObj, GameObject colObj)
	{
		//if (targetObj.GetComponent<Creature_p>().GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_USER))
		//{
		//	Debug.Log("이벤트 내부 " + targetObj.name + " , 상태  " + targetObj.GetComponent<Creature_p>().GetStatusType());
		//	Debug.Log("이벤트 내부 " + targetObj.name + "공격 상태 " + targetObj.GetComponent<Creature_p>().GetAttackType());
		//}

		if (m_unitType == null) return;

		//현재 이벤트 발생 객채와 내가 호출한 객채의 유니크 아이디값이 같을때 만 호출
		if (targetObj.GetComponent<Creature_p>().GetUniqueIndex() == m_unitObject.GetComponent<Creature_p>().GetUniqueIndex())
		{
			//현재 내가 적 이면서 충돌된 객체 가 유닛일때
			if (colObj.tag.Equals("User_Sight") && targetObj.GetComponent<Creature_p>().GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_ENEMY))
			{
				//현재 내 시야에 적이 들어왔고 공격을 안한상태 
				if (targetObj.GetComponent<Creature_p>().GetAttackType().Equals(CommonTypes.AttackType.ATTACK_TYPE_NONE))
				{
					m_EnemySighttList.Add(colObj.transform.parent);
					targetObj.GetComponent<Creature_p>().SetStatusType(CommonTypes.StatusType.STATUS_TYPE_SIGHTMOVE);
				}
				//현재 내가 때리고 있어도 시야에 는 추가 해주고 공격은 유지시킨다.
				else if (targetObj.GetComponent<Creature_p>().GetAttackType().Equals(CommonTypes.AttackType.ATTACK_TYPE_USER_ATTACK))
				{
					m_EnemySighttList.Add(colObj.transform.parent);
					targetObj.GetComponent<Creature_p>().SetStatusType(CommonTypes.StatusType.STATUS_TYPE_RANGEMOVE);
				}
			
			}
			//현재 내가 유닛 이면서 충돌된 객체 가 적일때
			else if (colObj.tag.Equals("Enemy_Sight") && targetObj.GetComponent<Creature_p>().GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_USER))
			{
				if (targetObj.GetComponent<Creature_p>().GetAttackType().Equals(CommonTypes.AttackType.ATTACK_TYPE_NONE))
				{
					m_UserSightList.Add(colObj.transform.parent);
					targetObj.GetComponent<Creature_p>().SetStatusType(CommonTypes.StatusType.STATUS_TYPE_SIGHTMOVE);
				}
				else if (targetObj.GetComponent<Creature_p>().GetAttackType().Equals(CommonTypes.AttackType.ATTACK_TYPE_ENEMY_ATTACK))
				{
					m_UserSightList.Add(colObj.transform.parent);
					targetObj.GetComponent<Creature_p>().SetStatusType(CommonTypes.StatusType.STATUS_TYPE_RANGEMOVE);
				}
			
			}
		}
	}

	void OnDestroy()
	{
		c_controllerList.GetEventController().OnCollisionEvent -= SightCollisionEventCheck;
	}
	
}
