using UnityEngine;
using System.Collections;

public class PointsText : MonoBehaviour
{
	private bool playing;
	private GUIText text;
	private Transform myTransform;

	private void Awake ()
	{
		text = GetComponent<GUIText>();
		text.enabled = false;
		myTransform = transform;
	}

	private void Update ()
	{
		// making the text to float and change opacity over time
		if (playing)
		{
			myTransform.Translate(new Vector3(Time.deltaTime / 25, Time.deltaTime / 5));
			text.color = Color.Lerp(text.color, new Color32(255, 255, 255, 0), Time.deltaTime * 5);
		}
	}

	// called by the bubble when we clicked him — assigns coords and starts the animation
	public void Play (string points, Vector3 position)
	{
		myTransform.position = position;
		text.text = points;
		playing = true;
		text.enabled = true;
		Invoke("Negate", 1);
	}

	private void Negate ()
	{
		Destroy(gameObject);
	}

}