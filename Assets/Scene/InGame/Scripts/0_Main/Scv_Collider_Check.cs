using UnityEngine;
using System.Collections;

public class Scv_Collider_Check : MonoBehaviour
{
	private c_ControllerList c_controllerList;
	private m_ObjectList m_objectList;
	private Vector3 Scvp;
	private GameObject m_unitObject;
	private Creature_p m_unitType;

	// Use this for initialization
	void Start ()
	{
		c_controllerList = GameObject.Find("GameControllerManager").GetComponent<c_ControllerList>();
		if (c_controllerList == null) return;

		m_objectList = GameObject.Find("GameObjectManger").GetComponent<m_ObjectList>();
		if (m_objectList == null) return;

		m_unitObject = transform.gameObject;

		if (m_unitObject == null) return;

		//	m_unitType = m_unitObject.GetComponent<Creature_p>();
		//	if (m_unitType == null) return;

		m_unitType = FindObjectOfType(typeof(Creature_p)) as Creature_p;
		if (m_unitType == null) return;

	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}


/*	private void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag.Equals("User_Mineral"))
		{
			Scvp = m_objectList.GetUnitGameObject().GetScvStartPoint();
			Debug.Log(Scvp+"(1)");
			Vector3 targetPos = new Vector3(Scvp.x, Scvp.y, Scvp.z);
			Debug.Log(targetPos+"(2)");
			Vector3 movePos = Vector3.MoveTowards(m_unitObject.transform.position, targetPos, Time.deltaTime * 100f);
			Debug.Log(movePos + "(3)");
			m_unitObject.transform.position = movePos;
			Debug.Log(m_unitObject.transform.tag+ "(4)");
		}
	}*/
	private void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag.Equals("User_Mineral"))
		{
			m_unitType.SetStatusType(CommonTypes.StatusType.STATUS_TYPE_SCV_BACK);
			Debug.Log(m_unitType.GetStatusType());
		}
	}

}
