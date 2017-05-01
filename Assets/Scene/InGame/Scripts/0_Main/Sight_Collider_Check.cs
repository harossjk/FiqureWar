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

	public bool m_Isplayer = false;


	private List<Transform> m_EnemySighttList = new List<Transform>();
	public List<Transform> GetEnemySightList()
	{
		return m_EnemySighttList;
	}
	public void SightDeletEnemy(int index)
	{
		m_EnemySighttList.RemoveAt(index);
	}

	private List<Transform> m_UserSightList = new List<Transform>();
	public List<Transform> GetUserSightList()
	{
		return m_UserSightList;
	}
	public void SightDeletUser(int index)
	{
		m_UserSightList.RemoveAt(index);
	}

	private Collider m_colObj;

	void Start()
	{
		c_controllerList = GameObject.Find("GameControllerManager").GetComponent<c_ControllerList>();
		if (c_controllerList == null) return;
		m_unitObject = transform.parent.gameObject;
		if (m_unitObject == null) return;
		m_unitType = m_unitObject.transform.GetComponent<Creature_p>();
		if (m_unitType == null) return;
	}

	void OnTriggerEnter(Collider other)
	{
		m_colObj = other;
		if (m_colObj == null) return;
		//충돌 객체와 현재 객체 같을때 (아군 과 아군이 부딧혔을때) 중복자리 위치 옮기기 로직

		if (First_SightColliderCheck()){}

		
	}

	public bool First_SightColliderCheck()
	{
		if (m_unitType == null|| m_colObj== null)
		{
			return false;
		}
		else if (m_unitType.GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_USER) && m_colObj.tag.Equals("Enemy_Sight"))
		{
			// Now is Object User
			m_UserSightList.Add(m_colObj.transform.parent);
			m_unitType.SetStatusType(CommonTypes.StatusType.STATUS_TYPE_PAUSE);
			return true;
		}
		else if (m_unitType.GetCreatureType().Equals(CommonTypes.MinionTeam.MINION_TEAM_ENEMY) && m_colObj.tag.Equals("User_Sight"))
		{
			// Now is Object Enmey
			m_EnemySighttList.Add(m_colObj.transform.parent);
			m_unitType.SetStatusType(CommonTypes.StatusType.STATUS_TYPE_PAUSE);
			return true;
		}

		return true;
	}

	/*


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
	*/

}
