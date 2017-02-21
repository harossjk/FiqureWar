using UnityEngine;
using System.Collections;

public static class c_ControllerList
{

	public static c_MapController c_mapController { get { return c_MapController.GetInstance(); } }
	public static c_UnitController c_unitController { get { return c_UnitController.GetInstance(); } }
	public static c_ViewController c_viewController { get { return c_ViewController.GetInstance(); } }
	public static c_PauseController c_pausecontroller { get { return c_PauseController.GetInstance(); } }

	//이벤트 컨트롤러 추가 
	public static c_EventHandler c_eventHandler { get { return c_EventHandler.GetInstance(); } }
}
