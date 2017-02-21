using UnityEngine;
using System.Collections;
using System;

public class c_EventHandler  {


	private static c_EventHandler c_eventHandler = null;
	public static c_EventHandler GetInstance()
	{
		if (null == c_eventHandler)
		{
			c_eventHandler = new c_EventHandler();
		}
		return c_eventHandler;
	}

	public delegate void CollisionEventHandler(GameObject targetObj, GameObject colObj);

	public event CollisionEventHandler OnCollisionEvent;
	public void SendCollisionEvent(GameObject targetObj, GameObject colObj)
	{
		OnCollisionEvent(targetObj, colObj);
	}

	//	public delegate void AttackEventHandler(GameObject targetObj, GameObject colObj);
	//public event AttackEventHandler OnAttackEvent;
	//
	//public void SendAttackEvent(GameObject targetObj, GameObject colObj)
	//{
	//	OnAttackEvent(targetObj, colObj);
	//}



}
