using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Storage : MonoBehaviour
{
	private Text mineralScore;
	private m_ObjectList m_objectList;

	// Use this for initialization
	void Start ()
	{
		mineralScore = GetComponent<Text>();
		m_objectList = GameObject.Find("GameObjectManger").GetComponent<m_ObjectList>();
		if (m_objectList == null) return;
	}
	
	// Update is called once per frame
	void Update ()
	{
		mineralScore.text = "Mineral " + m_objectList.GetMapGameObject().GetMineralStorage();



	}

}
