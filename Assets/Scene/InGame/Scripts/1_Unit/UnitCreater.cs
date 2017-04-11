using UnityEngine;
using System.Collections;

public class UnitCreater : MonoBehaviour
{

	public static int m_uniqueidIndex = 0;
	public static int UniqueIdIndexGenerator()
	{
		return m_uniqueidIndex++;
	}
	private MapCreater m_mapcreater;
	private m_ObjectList m_objectList;
	private Creature_p m_unitPrice;
	void Start()
	{
		m_objectList = GameObject.Find("GameObjectManger").GetComponent<m_ObjectList>();
		if (m_objectList == null) return;
		m_mapcreater = FindObjectOfType(typeof(MapCreater)) as MapCreater;
		if (m_mapcreater == null) return;


		m_unitPrice = gameObject.AddComponent<Creature_p>();
		if (m_unitPrice == null) return;

	}


	public void userCreateUnit()
	{
		int heroPrice = m_unitPrice.GetHeroPrice();
		Debug.Log(heroPrice);
		if (m_objectList.GetMapGameObject().GetMineralStorage() >= heroPrice)
		{
			UnitObjectCreate("User_Unit", UniqueIdIndexGenerator(), "Hero", 1000, 100, 100.0f, 100.0f, CommonTypes.MinionTeam.MINION_TEAM_USER);
			m_objectList.GetMapGameObject().SetMineralStorage(-heroPrice);
			Debug.Log(m_objectList.GetMapGameObject().GetMineralStorage());
		}
	}

	public void enemyCreateUnit()
	{
		UnitObjectCreate("Enemy_Unit", UniqueIdIndexGenerator(), "Hero", 1000, 100, 100.0f, 100.0f, CommonTypes.MinionTeam.MINION_TEAM_ENEMY);
	}
	public void scvCreateUnit()
	{
		int scvPrice = m_unitPrice.GetScvPrice();

		if (m_objectList.GetMapGameObject().GetMineralStorage() >= scvPrice)
		{
			UnitObjectCreate("SCV", UniqueIdIndexGenerator(), "SCV", 1000, 100, 100.0f, 100.0f, CommonTypes.MinionTeam.MINION_TEAM_USER_SCV);
			m_objectList.GetMapGameObject().SetMineralStorage(-scvPrice);

		}

		Vector3 scvPoint = new Vector3(100, -80, -2);
		m_objectList.GetUnitGameObject().SetScvStartPoint(scvPoint);


	}
	/*public void HeroCreate()
	{
			UnitObjectCreate("User_Unit", UniqueIdIndexGenerator(), "Hero", 1000, 100, 100.0f, 100.0f, CommonTypes.MinionTeam.MINION_TEAM_USER_HERO);
			UnitObjectCreate("Enemy_Unit", UniqueIdIndexGenerator(), "Hero", 1000, 100, 100.0f, 100.0f, CommonTypes.MinionTeam.MINION_TEAM_ENEMY_HERO);
	}*/
	// Use this for initialization

	private void UnitObjectCreate(string prefabsName, int unique_id, string unitType, int hp, int attack, float attackSpeed, float moveSpeed, CommonTypes.MinionTeam miniontype)
	{
		string prefabsPath = "prefabs/Unit/" + prefabsName;
		GameObject prefabsLoad = Resources.Load(prefabsPath) as GameObject;
		if (prefabsLoad == null) return;

		GameObject unitProduceObject = Instantiate(prefabsLoad) as GameObject;
		if (unitProduceObject == null) return;

		unitProduceObject.transform.parent = this.transform;
		unitProduceObject.name = prefabsName + "_" + unique_id;

		Creature_p creatureScript = unitProduceObject.transform.GetComponent<Creature_p>();
		if (creatureScript == null) return;

		switch (unitType)
		{
			case "Hero":
				creatureScript.SetUniqueIndex(unique_id);
				creatureScript.SetCreatureType(miniontype);
				creatureScript.SetStatusType(CommonTypes.StatusType.STATUS_TYPE_PLAY);
				creatureScript.SetAttackType(CommonTypes.AttackType.ATTACK_TYPE_NONE);
				creatureScript.SetCollisionType(CommonTypes.CollisionType.COLLISION_TYPE_NONE);
				creatureScript.SetGameStatusType(CommonTypes.GameStatusType.GAMESTATUS_TYPE_NONE);

				unitProduceObject.GetComponent<HeroUnit_c>().SetUserUnitHP(hp);
				unitProduceObject.GetComponent<HeroUnit_c>().SetUserUnitAttack(attack);
				unitProduceObject.GetComponent<HeroUnit_c>().SetUserUnitAttackSpeed(attackSpeed);
				unitProduceObject.GetComponent<HeroUnit_c>().SetUserUnitMoveSpeed(moveSpeed);
				m_objectList.GetUnitGameObject().SetUnitGameObject(unique_id, ref unitProduceObject);
				break;

			case "Nomal":








				break;
			case "SCV":

				creatureScript.SetUniqueIndex(unique_id);
				creatureScript.SetCreatureType(miniontype);
				creatureScript.SetStatusType(CommonTypes.StatusType.STATUS_TYPE_SCV_NONE);
				creatureScript.SetAttackType(CommonTypes.AttackType.ATTACK_TYPE_NONE);
				creatureScript.SetCollisionType(CommonTypes.CollisionType.COLLISION_TYPE_NONE);
				creatureScript.SetGameStatusType(CommonTypes.GameStatusType.GAMESTATUS_TYPE_NONE);
				break;
		}
	}




}
