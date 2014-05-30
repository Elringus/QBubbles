using UnityEngine;
using System.Collections;

public class SGUI : MonoBehaviour
{
	#region SINGLETON
	private static SGUI _i;
	public static SGUI I
	{
		get
		{
			if (_i == null) _i = FindObjectOfType(typeof(SGUI)) as SGUI;
			return _i;
		}
	}
	private void OnApplicationQuit () { _i = null; }
	#endregion

	public int Points;

	private void OnGUI ()
	{
		// shown on the start scene when loading asset bundles
		if (Application.loadedLevel == 0)
		{
			GUI.Box(new Rect(Screen.width / 2 - 150, Screen.height / 2 + 12.5f, 300, 25), "Loading assets, please wait...");
		}
		// shown in the game
		else
		{
			GUI.Box(new Rect(0, Screen.height - 50, 120, 25), "Points: " + Points);
			GUI.Box(new Rect(0, Screen.height - 25, 120, 25), "Timer: " + Time.timeSinceLevelLoad.ToString("F1"));
		}
	}
}