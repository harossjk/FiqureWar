using UnityEngine;
using System.Collections;

public class MapFight : MonoBehaviour {

	private m_ObjectList m_objectList;

	private MapCreater m_mapcreater;
	private int com;
	private int user;


	private string strType_Seissors , strType_Rock , strType_Paper; //(1,2,3)
	private int intType_Seissors, intType_Rock, intType_Paper;
	
	public string win, draw, lose;
	
	private int comRandom = 0;
	private bool isCheck = false;


	void Start () {
		m_objectList = GameObject.Find("GameObjectManger").GetComponent<m_ObjectList>();
		if (m_objectList == null) return;

		m_mapcreater = FindObjectOfType(typeof(MapCreater)) as MapCreater;
		if (m_mapcreater == null) return;


		//Destroy(m_ObjectList.m_mapGameObject.GetHeroMapTile(), 30);
		m_mapcreater.SetIsHeroMakeCheck(false);
	}

    void OnGUI()
    {


		GUI.Box(new Rect(70, 80, 45, 30), "enemy");
		GUI.Box(new Rect(70,140,45,30),"user");

        if(GUI.Button(new Rect(20 , 215 , 45, 30),"가위"))
        {
			
			Seissors_Rock_Paper("가위");
		}
        if (GUI.Button(new Rect(70, 215, 45, 30), "바위"))
        {
			
			Seissors_Rock_Paper("바위");
		}
        if (GUI.Button(new Rect(120, 215, 45, 30), "보"))
        {
			
			Seissors_Rock_Paper("보");
		}
    }
	void Update()
	{
		MiniGame();
	}

	private void MiniGame()
	{

		if(isCheck)
		{
			comRandom = Random.Range(1, 4);

			com = comRandom;
			print(comRandom);

			int com_user_formula = user - com;
			if (com_user_formula == 0)
			{
				print("비김");
				m_objectList.GetUnitGameObject().SetMiniGameCheck("draw");

			}
			if (com_user_formula == 1 || com_user_formula == -2)
			{
				print("유저 이김 컴퓨터 짐");
				m_objectList.GetUnitGameObject().SetMiniGameCheck("win");

			}
			if (com_user_formula == -1 || com_user_formula == 2)
			{
				print("유저 짐 컴퓨터 이김");
				m_objectList.GetUnitGameObject().SetMiniGameCheck("lose");

			}
			
		}
		isCheck = false;
		

	}

	private void Seissors_Rock_Paper(string type)
	{
		if(type == "가위")
		{
			strType_Seissors = type;
			intType_Seissors = 1;
			user = intType_Seissors;
			print(strType_Seissors);
			print(user);
		}
		if(type == "바위")
		{
			strType_Rock = type;
			intType_Rock = 2;
			user = intType_Rock;
			print(strType_Rock);
		}
		if (type == "보")
		{
			strType_Paper = type;
			intType_Paper = 3;
			user = intType_Paper;
			print(strType_Paper);
		}

		isCheck = true;
		
	}


}
