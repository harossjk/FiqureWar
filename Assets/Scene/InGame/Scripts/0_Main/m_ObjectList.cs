using UnityEngine;
using System.Collections;

public static class m_ObjectList  {
	public static m_UnitGameObject m_unitGameObject { get { return m_UnitGameObject.GetInstance(); } }
	public static m_MapGameObject m_mapGameObject { get { return m_MapGameObject.GetInstance(); } }
}
