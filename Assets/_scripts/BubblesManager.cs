using UnityEngine;
using System.Collections;

public class BubblesManager : MonoBehaviour
{
	#region SINGLETON
	private static BubblesManager _i;
	public static BubblesManager I
	{
		get
		{
			if (_i == null) _i = FindObjectOfType(typeof(BubblesManager)) as BubblesManager;
			return _i;
		}
	}
	private void OnApplicationQuit () { _i = null; }
	#endregion

	public GameObject BubblePrefab;

	private void Awake () 
	{
    	
	}

	private void Update () 
	{
    	
	}
	
	public void SpawnBubble ()
	{
		Bubble bubble = ((GameObject)GameObject.Instantiate(BubblePrefab)).GetComponent<Bubble>();
	}
}