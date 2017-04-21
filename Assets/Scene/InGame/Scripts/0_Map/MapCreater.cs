using UnityEngine;
using System.Collections;

public class MapCreater : MonoBehaviour
{
	private m_ObjectList m_objectList;

	private static int m_mapIndex = 0;
	private Vector3 m_mapTilestartPos = new Vector3(-150.0f, -250.0f, -1.0f);
	private const int HoldZ = -100;
	private Vector3[] m_wayPoint = new Vector3[6];
	private Vector3[] m_mineralPoint = new Vector3[3];
	private bool isHeroMapTile = false;
	public void SetIsHeroMakeCheck(bool type){isHeroMapTile = type;}
	public bool GetIsHeroMakeCheck(){return isHeroMapTile;}

	// Use this for initialization
	void Start()
	{
		m_objectList = GameObject.Find("GameObjectManger").GetComponent<m_ObjectList>();
		if (m_objectList == null) return;

		mapTileCreate();
		mapObjectCreate("Enemy_Map", 0);
		mapObjectCreate("User_Map", 1);
		MineralCreate();
		
	}

	//Map 객체 생성
	private void mapObjectCreate(string prefabsName, int unique_id)
	{

		string prefabsPath = "prefabs/Map/" + prefabsName;
		GameObject prefabsLoad = Resources.Load(prefabsPath) as GameObject;
		if (prefabsLoad == null) return;
		GameObject mapProduceObject = Instantiate(prefabsLoad) as GameObject;
		if (mapProduceObject == null) return;

		mapProduceObject.transform.parent = this.transform;
		m_objectList.GetMapGameObject().SetMapGameObject(unique_id, ref mapProduceObject);

	}
	private void MineralCreate()
	{
		string prefabsPath = "prefabs/Map/User_Mineral";
		GameObject prefabsLoad = Resources.Load(prefabsPath) as GameObject;
		if (prefabsLoad == null) return;
		m_mineralPoint[0] = new Vector3(-20, -0, -2);
		m_mineralPoint[1] = new Vector3(0, -0, -2);
		m_mineralPoint[2] = new Vector3(20, -0, -2);
		for (int k = 0; k < m_mineralPoint.Length; k++)
		{
			GameObject minerlapos = Instantiate(prefabsLoad) as GameObject;
			if (minerlapos == null) continue;
			minerlapos.transform.position = m_mineralPoint[k];
			minerlapos.name = "minerlapos" + k;
			minerlapos.transform.parent = this.transform;
			m_objectList.GetMapGameObject().SetMineralPoint(ref minerlapos);

		}
	}



	
	//Map 타일 생성
	private void mapTileCreate()
	{
		// prefab load
		string prefabsPath = "prefabs/MapTile/Tile";
		GameObject prefab = Resources.Load(prefabsPath) as GameObject;
		if (prefab == null)
			return;
		string wayPointprefabsPath = "prefabs/MapTile/WayPoint";
		GameObject wayPointprefab = Resources.Load(wayPointprefabsPath) as GameObject;
		if (wayPointprefab == null)
			return;

		m_wayPoint[0] = new Vector3(-10, -160, HoldZ);
		m_wayPoint[1] = new Vector3(-100, -160, HoldZ);
		m_wayPoint[2] = new Vector3(-100, 0, HoldZ);
		m_wayPoint[3] = new Vector3(-100, 160, HoldZ);
		m_wayPoint[4] = new Vector3(-10, 160, HoldZ);
		m_wayPoint[5] = new Vector3(0, 0, HoldZ);

		for (int k = 0; k < m_wayPoint.Length; k++)
		{
			GameObject wayPos = Instantiate(wayPointprefab) as GameObject;
			
			if (wayPos == null) continue;
			wayPos.name = "m_wayPoint" + k;
			wayPos.transform.position = m_wayPoint[k];
			wayPos.transform.parent = this.transform;
			m_objectList.GetMapGameObject().SetWayPoint(ref wayPos);
		}

		for (int i = 0; i < 16; ++i) //가로 16개
		{
			for (int j = 0; j < 26; ++j) //세로 26개
			{
				// GameObject create
				GameObject mapTile = Instantiate(prefab) as GameObject;
				if (mapTile == null)
					continue;
				mapTile.transform.parent = this.transform;


				Tile tileScript = mapTile.transform.GetComponent<Tile>();
				if (tileScript == null)
					continue;

				// Tile Setting
				int index = MapTileIndexGenerator();
				mapTile.transform.name = "Tile_" + index;
				tileScript.SetId(index);
				tileScript.SetTileType(CommonTypes.eTileType.TILE_TYPE_NONE);

				// Set Tile Position
				float tilePosX = m_mapTilestartPos.x + (i * mapTile.transform.localScale.x);
				float tilePosY = m_mapTilestartPos.y + (j * mapTile.transform.localScale.y);
				mapTile.transform.position = new Vector3(tilePosX, tilePosY, m_mapTilestartPos.z);

				// Insert Tile to list
				m_objectList.GetMapGameObject().SetMapTile(ref mapTile);

			}
		}
	}

	public void HeroMapTile()
	{
		string heroMapprefab = "prefabs/MapTile/HeroTile";
		GameObject heroMapTlieprefap = Resources.Load(heroMapprefab) as GameObject;
		if (heroMapTlieprefap == null) return;
		GameObject heroMapTile = Instantiate(heroMapTlieprefap) as GameObject;
		if (heroMapTile == null) return;
		heroMapTile.transform.parent = this.transform;
	//	m_objectList.GetMapGameObject().SetHeroMapTile(heroMapTile);
		isHeroMapTile = true;
		Destroy(heroMapTile, 10);
	}

	private static int MapTileIndexGenerator()
	{
		return m_mapIndex++;
	}

}



