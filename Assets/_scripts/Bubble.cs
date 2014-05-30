using UnityEngine;
using System.Collections;

public class Bubble : MonoBehaviour
{
	#region CACHED_VARS
	private GameObject myGameObject;
	private Transform myTransform;
	private Rigidbody2D myRigidbody;
	private SpriteRenderer mySprite;
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
		mySprite = renderer as SpriteRenderer;
		
		myGameObject.SetActive(false);
	}

	private void Update ()
	{

	}

	// triggers when we clicked the bubble
	private void OnMouseDown ()
	{
		// award the player points for this bubble
		SGUI.I.Points += points;
		// and send it to the inactive bubbles pool
		BubblesManager.I.DeactivateBubble(myGameObject);
	}

	// triggers when we need to spawn a new bubble and this one was the chosen one by the BubbleManager
	private void OnEnable ()
	{
		size = Random.Range(LevelsManager.I.MinBubbleSizes[LevelsManager.I.CurrentLevel], LevelsManager.I.MaxBubbleSizes[LevelsManager.I.CurrentLevel]);
		speed = LevelsManager.I.BubbleSpeedFactors[LevelsManager.I.CurrentLevel] / size;
		points = (int)(speed * 10);

		myTransform.position = GetSpawnPoint();
	}

	private void OnDisable ()
	{
		
	}

	// triggers when the bubble goes off main camera viewport (bubble falls under hor. border of the screen)
	private void OnBecameInvisible ()
	{
		// returning bubble to the inactive pool
		if (BubblesManager.I) BubblesManager.I.DeactivateBubble(myGameObject);
	}

	// calculating a random place to spawn bubble, so it will smoothly fall and won't go off vert. borders
	private Vector3 GetSpawnPoint ()
	{
		float spriteWidth = mySprite.sprite.textureRect.width;
		return Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0 + size * spriteWidth / 2, Screen.width - size * spriteWidth / 2), Screen.height + size * spriteWidth / 2, 10));
	}
}