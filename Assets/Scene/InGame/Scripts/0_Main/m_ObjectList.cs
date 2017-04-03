using UnityEngine;
using System.Collections;
using System;

public class m_ObjectList : MonoBehaviour {

	private m_UnitGameObject m_unitGameObject;// { get { return m_UnitGameObject.GetInstance(); } }
	private m_MapGameObject m_mapGameObject;//{ get { return m_MapGameObject.GetInstance(); } }

	void Awake()
	{
		m_unitGameObject = GetComponent<m_UnitGameObject>();
		m_mapGameObject = GetComponent<m_MapGameObject>();
	}
	public m_UnitGameObject GetUnitGameObject()
	{
		return m_unitGameObject;
	}
	public m_MapGameObject GetMapGameObject()
	{
		return m_mapGameObject;
	}

}
