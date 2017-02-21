using UnityEngine;
using System.Collections;

public class c_PauseController  :MonoBehaviour {


	private static c_PauseController c_pauseController = null;

	public static c_PauseController GetInstance()
	{
		if (null == c_pauseController)
		{
			c_pauseController = new c_PauseController();
		}
		return c_pauseController;
	}




}
