using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour
{
	private void Awake () 
	{
		// scaling sprite so it will cover the entire screen
		SpriteRenderer spr = renderer as SpriteRenderer;
		float diff = Screen.height / spr.sprite.textureRect.height * 1.5f;
		transform.localScale = new Vector3(diff, diff, 1);
		transform.position = new Vector3(0, 0, 10);
	}

}