using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Hp_Bar : MonoBehaviour
{
	public Slider HpBar;
	private Creature_p m_unitType;

	// Use this for initialization
	void Start ()
	{
		m_unitType = gameObject.AddComponent<Creature_p>();
		if (m_unitType == null) return;

		HpBar.maxValue = m_unitType.GetUserUnitHP();
		HpBar.value = HpBar.maxValue;
	}

	// Update is called once per frame
	void Update()
	{
		HpBar.value -= 1.0f;
		Debug.Log(HpBar.value);
		/*HpBar.minValue = 0.0f;
		HpBar.maxValue = 100.0f;*/

	}
}
