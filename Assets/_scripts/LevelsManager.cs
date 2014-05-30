using UnityEngine;
using System.Collections;

public class LevelsManager : MonoBehaviour
{
	#region SINGLETON
	private static LevelsManager _i;
	public static LevelsManager I
	{
		get
		{
			if (_i == null) _i = FindObjectOfType(typeof(LevelsManager)) as LevelsManager;
			return _i;
		}
	}
	private void OnApplicationQuit () { _i = null; }
	#endregion

	public float[] LevelStartTimes;		// at what time (in seconds) to start corresponding levels
	public float[] MinSpawnIntervals;	// the minimum spawn interval (in seconds) of the bubbles in the corresponding levels
	public float[] MaxSpawnIntervals;	// the maximum spawn interval (in seconds) of the bubbles in the corresponding levels
	public float[] MinBubbleSizes;		// mininum bubble size in the corresponding levels
	public float[] MaxBubbleSizes;		// maximum bubble size in the corresponding levels
	public float[] BubbleSpeedFactors;	// the speed of the bubbles will be multiplied by this factor in the corresponding levels

	public int CurrentLevel;			// the current difficulty level running
	private int TotalLevels;			// how many levels in total we have predefined (by the length of the levels parameter arrays)

	private float spawnTimer;			// timer wich we use to determine if it is the time to spawn a bubble
	private float currentSpawnTime;		// assigned at the spawn-time of the bubble 

	private void Awake ()
	{
		if (!GameObject.FindObjectOfType<AssetsManager>()) Application.LoadLevel(0);

		TotalLevels = LevelStartTimes.Length;
		CurrentLevel = 0;
	}

	private void Start ()
	{
		StartCoroutine(Spawner());
	}

	private void Update ()
	{
		// switching difficulty levels and generating texures 
		if (TotalLevels > CurrentLevel + 1 && Time.timeSinceLevelLoad >= LevelStartTimes[CurrentLevel + 1])
		{
			CurrentLevel++;
			TexturesManager.I.GenerateTextures(CurrentLevel);
		}
		spawnTimer += Time.deltaTime;
	}

	private IEnumerator Spawner ()
	{
		while (true)
		{
			if (spawnTimer >= currentSpawnTime)
			{
				spawnTimer = 0;
				currentSpawnTime = Random.Range(MinSpawnIntervals[CurrentLevel], MaxSpawnIntervals[CurrentLevel]);
				BubblesManager.I.ActivateBubble();
			}
			yield return new WaitForSeconds(.1f);
		}
	}
}