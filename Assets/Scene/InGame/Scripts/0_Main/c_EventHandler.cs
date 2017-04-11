using UnityEngine;
using System.Collections;
using System;

public class c_EventHandler : MonoBehaviour {


	public delegate void CollisionEventHandler(GameObject targetObj, GameObject colObj);

	public event CollisionEventHandler OnCollisionEvent;
	public void SendCollisionEvent(GameObject targetObj, GameObject colObj)
	{
		if(OnCollisionEvent != null)
		{
			OnCollisionEvent(targetObj, colObj);
		}
		return;
	}

	public event AttackEventHandler OnAttackEvent;
	public delegate void AttackEventHandler(GameObject targetObj, GameObject colObj);

	public void SendAttackEvent(GameObject targetObj, GameObject colObj)
	{
		if (OnAttackEvent != null)
		{
			OnAttackEvent(targetObj, colObj);
		}
		return;
	}
}
