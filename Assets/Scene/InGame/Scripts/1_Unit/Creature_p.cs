using UnityEngine;
using System.Collections;

public class Creature_p : MonoBehaviour
{

	#region fixing unit setting const data
	private const int m_unitHP = 1000;
	private const int m_unitAttack = 100;
	private const float m_unitMoveSpeed = 100.0f;
	private const float m_unitAttackSpeed = 50.0f;

	public int GetUnitConstHP()
	{
		return m_unitHP;
	}
	public int GetUnitConstAttack()
	{
		return m_unitAttack;
	}
	public float GetUnitConstMoveSpeed()
	{
		return m_unitMoveSpeed;
	}
	public float GetUnitConstAttackSpeed()
	{
		return m_unitAttackSpeed;
	}

	#endregion

	#region user unit setting 

	private int m_userUnitHP = 0;
	private int m_userUnitAttack = 0;
	private float m_userUnitMoveSpeed = 0;
	private float m_userUnitAttackSpeed = 0;
	private int m_enemyUnitHP = 0;
	private int m_Hero_price = 1;
	private int m_Scv_price = 20;

	public int GetScvPrice()
	{
		return m_Scv_price;
	}
	public int GetHeroPrice()
	{
		return m_Hero_price;
	}
	public int GetEnemyUnitHP()
	{
		return m_enemyUnitHP;
	}
	public void SetEnemyUnitHP(int unitHP)
	{
		m_enemyUnitHP = unitHP;
	}

	public int GetUserUnitHP()
	{
		return m_userUnitHP;
	}
	public void SetUserUnitHP(int unitHP)
	{
		m_userUnitHP = unitHP;
	}

	public int GetUserUnitAttack()
	{
		return m_userUnitAttack;
	}
	public void SetUserUnitAttack(int unitAttack)
	{
		m_userUnitAttack = unitAttack;
	}

	public float GetUserUnitMoveSpeed()
	{
		return m_userUnitMoveSpeed;
	}
	public void SetUserUnitMoveSpeed(float unitMoveSpeed) 
	{
		m_userUnitMoveSpeed = unitMoveSpeed;
	}

	public float GetUserUnitAttackSpeed()
	{
		return m_userUnitAttackSpeed;
	}
	public void SetUserUnitAttackSpeed(float unitAttackSpeed)
	{
		m_userUnitAttackSpeed = unitAttackSpeed;
	}

	#endregion

	#region minionTeam Type

	private int m_prevuniqueId = 0;
	public void SetUniqueIndex(int index) { m_prevuniqueId = index; }
	public int GetUniqueIndex() { return m_prevuniqueId; }

	private CommonTypes.MinionTeam m_type;

	public void SetCreatureType(CommonTypes.MinionTeam type)
	{
		m_type = type;
	}
	public CommonTypes.MinionTeam GetCreatureType()
	{
		return m_type;
	}
	#endregion


	#region waypoint 
	private int m_UsercurWayPointIndex = -1;

	public int GetUserCurWayPointIndex()
	{
		return m_UsercurWayPointIndex;
	}
	public void SetUserWayPointIndex(int index)
	{
		m_UsercurWayPointIndex = index;
	}

	private int m_EnemycurWayPointIndex = 5;

	public int GetEnemyCurWayPointIndex()
	{
		return m_EnemycurWayPointIndex;
	}
	public void SetEnemyWayPointIndex(int index)
	{
		m_EnemycurWayPointIndex = index;
	}


	#endregion

	#region Status
	private CommonTypes.StatusType m_StatusType;

	public void SetStatusType(CommonTypes.StatusType type)
	{
		m_StatusType = type;
	}
	public CommonTypes.StatusType GetStatusType()
	{
		return m_StatusType;
	}

	private CommonTypes.CollisionType m_CollisionType;

	public void SetCollisionType(CommonTypes.CollisionType type)
	{
		m_CollisionType = type;
	}
	public CommonTypes.CollisionType GetCollisionType()
	{
		return m_CollisionType;
	}

	private CommonTypes.GameStatusType m_gameStatusType;
	
	public void SetGameStatusType(CommonTypes.GameStatusType type)
	{
		m_gameStatusType = type;
	}
	public CommonTypes.GameStatusType GetGameStatusType()
	{
		return m_gameStatusType;
	}

	private CommonTypes.AttackType m_attackType;

	public void SetAttackType(CommonTypes.AttackType type)
	{
		m_attackType = type;
	}
	public CommonTypes.AttackType GetAttackType()
	{
		return m_attackType;
	}

	#endregion

}
