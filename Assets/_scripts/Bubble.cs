using UnityEngine;
using System.Collections;

public class Bubble : MonoBehaviour
{
	#region CACHED_VARS
	private GameObject myGameObject;
	private Transform myTransform;
	private Rigidbody2D myRigidbody;
	#endregion

	private float _size;	// the size of the bubble
	private float _speed;	// the falling speed of the bubble
	private int points;		// how much points player gets for clicking the bubble

	private float size
	{
		get { return _size; }
		set
		{
			_size = value;
			myTransform.localScale = new Vector3(value, value, value);
		}
	}

	private float speed
	{
		get { return _speed; }
		set
		{
			_speed = value;
			myRigidbody.gravityScale = value;
		}
	}

	private void Awake ()
	{
		myGameObject = gameObject;
		myTransform = transform;
		myRigidbody = rigidbody2D;

		size = Random.Range(LevelsManager.I.MinBubbleSizes[LevelsManager.I.CurrentLevel], LevelsManager.I.MaxBubbleSizes[LevelsManager.I.CurrentLevel]);
		speed = LevelsManager.I.BubbleSpeedFactors[LevelsManager.I.CurrentLevel] / size;
		points = (int)(speed * 10);

		myTransform.position = GetSpawnPoint();
	}

	private void Update ()
	{

	}

	private void OnMouseDown ()
	{
		SGUI.I.Points += points;
		Destroy(myGameObject);
	}

	private void OnEnable ()
	{
		
	}

	private void OnDisable ()
	{
		
	}

	private void OnBecameInvisible ()
	{
		Destroy(myGameObject);
	}

	// TODO: check for near bubbles so it wont have a chance to collide with them
	private Vector3 GetSpawnPoint ()
	{
		return Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0 + size * 256 / 2, Screen.width - size * 256 / 2), Screen.height + size * 256 / 2, 10));
	}
}