using UnityEngine;
using System.Collections;

public class MapCreater : MonoBehaviour
{
	private static int m_mapIndex = 0;
	private Vector3 m_mapTilestartPos = new Vector3(-150.0f, -250.0f, -1.0f);
	private const int HoldZ = -100;
	private Vector3[] m_wayPoint = new Vector3[6];
	private bool isHeroMapTile = false;

	public void SetIsHeroMakeCheck(bool type){isHeroMapTile = type;}
	public bool GetIsHeroMakeCheck(){return isHeroMapTile;}

	// Use this for initialization
	void Start()
	{
		mapTileCreate();
		mapObjectCreate("Enemy_Map", 0);
		mapObjectCreate("User_Map", 1);
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
		m_ObjectList.m_mapGameObject.SetMapGameObject(unique_id, ref mapProduceObject);
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

		m_wayPoint[0] = new Vector3(-10, -220, HoldZ);
		m_wayPoint[1] = new Vector3(-130, -220, HoldZ);
		m_wayPoint[2] = new Vector3(-130, 0, HoldZ);
		m_wayPoint[3] = new Vector3(-130, 220, HoldZ);
		m_wayPoint[4] = new Vector3(-10, 220, HoldZ);
		m_wayPoint[5] = new Vector3(0, 0, HoldZ);

		for (int k = 0; k < m_wayPoint.Length; k++)
		{
			GameObject wayPos = Instantiate(wayPointprefab) as GameObject;
			
			if (wayPos == null) continue;
			wayPos.name = "m_wayPoint" + k;
			wayPos.transform.position = m_wayPoint[k];
			m_ObjectList.m_mapGameObject.SetWayPoint(ref wayPos);
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
				m_ObjectList.m_mapGameObject.SetMapTile(ref mapTile);

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
		//m_ObjectList.m_mapGameObject.SetHeroMapTile(ref heroMapTile);
		isHeroMapTile = true;
	}

	private static int MapTileIndexGenerator()
	{
		return m_mapIndex++;
	}

}



