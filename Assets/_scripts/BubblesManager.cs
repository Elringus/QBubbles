using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	public GameObject BubblePrefab;				// bubble prototype
	public int BubblePoolCapacity;				// how many bubbles create at the start
	private List<GameObject> activeBubbles;		// currently active bubbles (visible on screen)
	private List<GameObject> inactiveBubbles;	// pool for inactive bubbles

	private void Awake () 
	{
		activeBubbles = new List<GameObject>(BubblePoolCapacity);
		inactiveBubbles = new List<GameObject>(BubblePoolCapacity);

		// instantiate some bubbles at the start, so we can reuse them and won't have to recreate them everytime
		for (int i = 0; i < BubblePoolCapacity; i++)
			inactiveBubbles.Add(GameObject.Instantiate(BubblePrefab) as GameObject);
	}
	
	// attempts to get a bubble from inactive pool and creating new one on fail
	public void ActivateBubble ()
	{
		if (inactiveBubbles.Count == 0) 
			inactiveBubbles.Add(GameObject.Instantiate(BubblePrefab) as GameObject);

		GameObject bubble = inactiveBubbles[inactiveBubbles.Count - 1];
		inactiveBubbles.Remove(bubble);
		activeBubbles.Add(bubble);
		bubble.SetActive(true);
	}

	// returns the bubble to inactive pool
	public void DeactivateBubble (GameObject bubble)
	{
		bubble.SetActive(false);
		activeBubbles.Remove(bubble);
		inactiveBubbles.Add(bubble);
	}

}