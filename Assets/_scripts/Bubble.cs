using UnityEngine;
using System.Collections;

public class Bubble : MonoBehaviour
{
	#region CACHED_VARS
	// using cached vars so we won't have to call GetComponent() every frame
	private GameObject myGameObject;
	private Transform myTransform;
	private Rigidbody2D myRigidbody;
	private SpriteRenderer mySprite;
	#endregion

	private float _size;		// the size of the bubble
	private float _speed;		// the falling speed of the bubble
	private int points;			// how much points player gets for clicking the bubble
	private float prevTexWidth; // for calculating collider size

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
		
		// many bubbles created at the start for pooling, so we need to disable them for now
		myGameObject.SetActive(false);
	}

	// triggers when we clicked the bubble
	private void OnMouseDown ()
	{
		// award the player points for this bubble
		SGUI.I.Points += points;
		// play VFX
		Destroy(GameObject.Instantiate(AssetsManager.VFXPrefab, myTransform.position, Quaternion.identity), 1);
		// show points text
		PointsText pointsText = ((GameObject)GameObject.Instantiate(AssetsManager.PointsPrefab)).GetComponent<PointsText>();
		pointsText.Play("+" + points.ToString(), Camera.main.WorldToViewportPoint(myTransform.position));
		// and send it to the inactive bubbles pool
		BubblesManager.I.DeactivateBubble(myGameObject);
	}

	// triggers when we need to spawn a new bubble and this one was the chosen one by the BubbleManager
	private void OnEnable ()
	{
		size = Random.Range(LevelsManager.I.MinBubbleSizes[LevelsManager.I.CurrentLevel], LevelsManager.I.MaxBubbleSizes[LevelsManager.I.CurrentLevel]);
		speed = LevelsManager.I.BubbleSpeedFactors[LevelsManager.I.CurrentLevel] / size;
		points = (int)(speed * 10);

		// assigning a random texture for the bubble
		Texture2D texture;
		int rnd = Random.Range(1, 4);
		switch (rnd)
		{
			case 1: texture = TexturesManager.I.texturesSet1[size > .85f ? 3 : size > .5f ? 2 : size > .25f ? 1 : 0]; break;
			case 2: texture = TexturesManager.I.texturesSet2[size > .85f ? 3 : size > .5f ? 2 : size > .25f ? 1 : 0]; break;
			case 3: texture = TexturesManager.I.texturesSet3[size > .85f ? 3 : size > .5f ? 2 : size > .25f ? 1 : 0]; break;
			default: texture = new Texture2D(256, 256); break;
		}
		// calculating new collider size
		prevTexWidth = mySprite.sprite.texture.width;
		mySprite.sprite = Sprite.Create(texture, new Rect(0, 0, 256, 256), new Vector2(.5f, .5f));
		((CircleCollider2D)collider2D).radius *= mySprite.sprite.textureRect.width / prevTexWidth;

		myTransform.position = GetSpawnPoint();
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