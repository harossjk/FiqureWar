using UnityEngine;
using System.Collections;

public class c_ScvController : MonoBehaviour
{
	private c_ControllerList c_controllerList;
	private m_ObjectList m_objectList;
	private GameObject m_scvObject;
	private Creature_p m_scvType;
	private GameObject m_mineralPoint;
	private int m_mineral;
	private int m_storage;

	public void Start()
	{
		c_controllerList = GameObject.Find("GameControllerManager").GetComponent<c_ControllerList>();
		if (c_controllerList == null) return;

		m_objectList = GameObject.Find("GameObjectManger").GetComponent<m_ObjectList>();
		if (m_objectList == null) return;

		m_scvObject = transform.gameObject;
		if (m_scvObject == null) return;

		m_scvType = m_scvObject.transform.GetComponent<Creature_p>();
		if (m_scvType == null) return;



	}

	// Update is called once per frame
	public void Update()
	{
		if (m_scvType.GetStatusType().Equals(CommonTypes.StatusType.STATUS_TYPE_SCV_NONE)
			|| m_scvType.GetStatusType().Equals(CommonTypes.StatusType.STATUS_TYPE_SCV_PLAY))
		{
			ScvMineralMove();
		}
		else if (m_scvType.GetStatusType().Equals(CommonTypes.StatusType.STATUS_TYPE_SCV_STOP))
		{
			ScvWork();
		}
		else if (m_scvType.GetStatusType().Equals(CommonTypes.StatusType.STATUS_TYPE_SCV_BACK))
		{
			ScvStartPosition();
		}
	}
	
	private void ScvMineralMove()
	{
		if (m_scvType.GetStatusType().Equals(CommonTypes.StatusType.STATUS_TYPE_SCV_NONE))
		{
			int m_mineral_random_point = Random.Range(-1, 3);
			m_mineralPoint = m_objectList.GetMapGameObject().GetMineralPoint(m_mineral_random_point);
			if (m_mineralPoint == null) return;
			m_scvType.SetStatusType(CommonTypes.StatusType.STATUS_TYPE_SCV_PLAY);
		}
		else if (m_scvType.GetStatusType().Equals(CommonTypes.StatusType.STATUS_TYPE_SCV_PLAY))
		{
			Vector3 targetPos = new Vector3(m_mineralPoint.transform.position.x, m_mineralPoint.transform.position.y, m_mineralPoint.transform.position.z);
			Vector3 movePos = Vector3.MoveTowards(m_scvObject.transform.position, targetPos, Time.deltaTime * 30f);
			m_scvObject.transform.position = movePos;
		}

	}
	private void ScvStartPosition()
	{
		Vector3 ScvStartPoint = m_objectList.GetUnitGameObject().GetScvStartPoint();
		Vector3 ScvBackMove = Vector3.MoveTowards(m_scvObject.transform.position, ScvStartPoint, Time.deltaTime * 30f);
		m_scvObject.transform.position = ScvBackMove;

		if (m_scvObject.transform.position == ScvStartPoint)
		{
			m_scvType.SetStatusType(CommonTypes.StatusType.STATUS_TYPE_SCV_ORIGINPOINT);
			m_storage += m_mineral;

			m_objectList.GetMapGameObject().SetMineralStorage(m_storage);
			m_storage = 0;
		}
		if (m_scvType.GetStatusType().Equals(CommonTypes.StatusType.STATUS_TYPE_SCV_ORIGINPOINT))
		{
			m_scvType.SetStatusType(CommonTypes.StatusType.STATUS_TYPE_SCV_PLAY);
			m_storage = 0;
		}
	}
	private void ScvWork()
	{
		m_mineral++;
		if (m_mineral >= 5)
		{
			m_scvType.SetStatusType(CommonTypes.StatusType.STATUS_TYPE_SCV_BACK);
		}
	}
}
