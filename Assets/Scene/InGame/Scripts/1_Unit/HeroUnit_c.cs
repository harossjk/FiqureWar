using UnityEngine;
using System.Collections;

public class HeroUnit_c : Creature_p
{
	//패시브 추가사항

	private int m_strongArm = 0;  //힘
	private int m_agility = 0;	//민
	private int m_intellect = 0;    //지
	
	public int GetStrongArm()
	{
		return m_strongArm;
	}
	public void SetStrongArm(int strongArm)
	{
		m_strongArm = strongArm;
	}


	public int GetAagility()
	{
		return m_agility;
	}
	public void SetAagility(int agility)
	{
		m_agility = agility;
	}

	public int GetIntellect()
	{
		return m_intellect;
	}
	public void SetIntellect(int intellect)
	{
		m_intellect = intellect;
	}

	



}
