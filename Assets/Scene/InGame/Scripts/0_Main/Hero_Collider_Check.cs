using UnityEngine;
using System.Collections;

public class Hero_Collider_Check : MonoBehaviour
{

	private c_ControllerList c_controllerList;
	private m_ObjectList m_objectList;
	//	private GameObject m_unitObject;
	private Creature_p m_unitType;
	private MapCreater m_mapcreater;


	// Use this for initialization
	void Start()
	{

		c_controllerList = GameObject.Find("GameControllerManager").GetComponent<c_ControllerList>();
		if (c_controllerList == null) return;

		m_objectList = GameObject.Find("GameObjectManger").GetComponent<m_ObjectList>();
		if (m_objectList == null) return;

		//	m_unitObject = transform.gameObject;

		//	if (m_unitObject == null) return;

		//	m_unitType = m_unitObject.GetComponent<Creature_p>();
		//	if (m_unitType == null) return;

		m_unitType = FindObjectOfType(typeof(Creature_p)) as Creature_p;
		if (m_unitType == null) return;

		m_mapcreater = FindObjectOfType(typeof(MapCreater)) as MapCreater;
		if (m_mapcreater == null) return;

	}

	// Update is called once per frame
	void Update()
	{

	}


	private void OnTriggerEnter(Collider col)
	{
		
		if (col.gameObject.tag.Equals("Enemy_Sight"))
		{
			Debug.Log("충돌");
			m_unitType.SetStatusType(CommonTypes.StatusType.STATUS_TYPE_PAUSE);
			m_mapcreater.HeroMapTile();

		}


	}
}
