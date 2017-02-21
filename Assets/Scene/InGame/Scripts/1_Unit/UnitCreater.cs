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



	void Start()
	{
		m_mapcreater = FindObjectOfType(typeof(MapCreater)) as MapCreater;
		if (m_mapcreater == null) return;
	}


	public void userCreateUnit()
	{
		if (m_mapcreater.GetIsHeroMakeCheck()){return;}
		else
		{
			UnitObjectCreate("User_Unit", UniqueIdIndexGenerator(), "Hero", 1000, 100, 100.0f, 100.0f, CommonTypes.MinionTeam.MINION_TEAM_USER);
		}
	}

	public void enemyCreateUnit()
	{
		if (m_mapcreater.GetIsHeroMakeCheck()){return;}
		else
		{ 
			UnitObjectCreate("Enemy_Unit", UniqueIdIndexGenerator(), "Hero", 1000, 100, 100.0f, 100.0f, CommonTypes.MinionTeam.MINION_TEAM_ENEMY);
		}
	}

	public void HeroCreate()
	{
		if (m_mapcreater.GetIsHeroMakeCheck()){return;}
		else
		{
			UnitObjectCreate("User_Unit", UniqueIdIndexGenerator(), "Hero", 1000, 100, 100.0f, 100.0f, CommonTypes.MinionTeam.MINION_TEAM_USER_HERO);
			UnitObjectCreate("Enemy_Unit", UniqueIdIndexGenerator(), "Hero", 1000, 100, 100.0f, 100.0f, CommonTypes.MinionTeam.MINION_TEAM_ENEMY_HERO);
		}
	}
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

		creatureScript.SetUniqueIndex(unique_id);
		creatureScript.SetCreatureType(miniontype);
		creatureScript.SetStatusType(CommonTypes.StatusType.STATUS_TYPE_PLAY);

		switch (unitType)
		{
			case "Hero":
				unitProduceObject.GetComponent<HeroUnit_c>().SetUserUnitHP(hp);
				unitProduceObject.GetComponent<HeroUnit_c>().SetUserUnitAttack(attack);
				unitProduceObject.GetComponent<HeroUnit_c>().SetUserUnitAttackSpeed(attackSpeed);
				unitProduceObject.GetComponent<HeroUnit_c>().SetUserUnitMoveSpeed(moveSpeed);
				m_ObjectList.m_unitGameObject.SetUnitGameObject(unique_id, ref unitProduceObject);
				break;

			case "Nomal":
				break;
			case "SCV":
				break;
		}
	}




}
