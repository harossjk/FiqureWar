using UnityEngine;
using System.Collections;
using System;

public class c_ControllerList : MonoBehaviour
{
	private c_MapController c_mapController;
	private c_ViewController c_viewController;
	private c_PauseController c_pausecontroller;

	//이벤트 컨트롤러 추가 
	private c_EventHandler c_eventHandler;

	void Awake()
	{
		c_mapController = GetComponent<c_MapController>();
		c_viewController = GetComponent<c_ViewController>();
		c_pausecontroller = GetComponent<c_PauseController>();
		c_eventHandler = GetComponent<c_EventHandler>();
	}

	public c_MapController GetMapController()
	{
		return c_mapController;
	}

	public c_ViewController GetViewController()
	{
		return c_viewController;
	}

	public c_PauseController GetPauseController()
	{
		return c_pausecontroller;
	}

	public c_EventHandler GetEventController()
	{
		return c_eventHandler;
	}
}
