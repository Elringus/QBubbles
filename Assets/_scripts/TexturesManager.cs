using UnityEngine;
using System.Collections;

public class TexturesManager : MonoBehaviour
{
	#region SINGLETON
	private static TexturesManager _i;
	public static TexturesManager I
	{
		get
		{
			if (_i == null) _i = FindObjectOfType(typeof(TexturesManager)) as TexturesManager;
			return _i;
		}
	}
	private void OnApplicationQuit () { _i = null; }
	#endregion

	// 3 sets of the textures for different types and sizes ([0] — 32, [1] — 64, [2] — 128, [3] — 256)
	public Texture2D[] texturesSet1;
	public Texture2D[] texturesSet2;
	public Texture2D[] texturesSet3;

	private void Awake () 
	{
		texturesSet1 = new Texture2D[4];
		texturesSet2 = new Texture2D[4];
		texturesSet3 = new Texture2D[4];

		GenerateTextures(0);
	}

	// specifies what texture types would be used for different difficulty levels
	public void GenerateTextures (int level)
	{
		switch (level)
		{
			case 1: 
				texturesSet1[0] = DrawCircleTexture(32, Color.cyan, false);
				texturesSet1[1] = DrawCircleTexture(64, Color.cyan, true);
				texturesSet1[2] = DrawCircleTexture(128, Color.cyan, true);
				texturesSet1[3] = DrawCircleTexture(256, Color.cyan, true);

				texturesSet2[0] = DrawCircleTexture(32, Color.red, false);
				texturesSet2[1] = DrawCircleTexture(64, Color.red, true);
				texturesSet2[2] = DrawCircleTexture(128, Color.red, true);
				texturesSet2[3] = DrawCircleTexture(256, Color.red, true);

				texturesSet3[0] = DrawCircleTexture(32, Color.white, false);
				texturesSet3[1] = DrawCircleTexture(64, Color.white, true);
				texturesSet3[2] = DrawCircleTexture(128, Color.white, true);
				texturesSet3[3] = DrawCircleTexture(256, Color.white, true); 
				break;
			case 2:
				texturesSet1[0] = DrawCircleTexture(32, Color.yellow, false);
				texturesSet1[1] = DrawCircleTexture(64, Color.yellow, true);
				texturesSet1[2] = DrawCircleTexture(128, Color.yellow, true);
				texturesSet1[3] = DrawCircleTexture(256, Color.yellow, true);

				texturesSet2[0] = DrawCircleTexture(32, Color.green, false);
				texturesSet2[1] = DrawCircleTexture(64, Color.green, true);
				texturesSet2[2] = DrawCircleTexture(128, Color.green, true);
				texturesSet2[3] = DrawCircleTexture(256, Color.green, true);

				texturesSet3[0] = DrawCircleTexture(32, Color.blue, false);
				texturesSet3[1] = DrawCircleTexture(64, Color.blue, true);
				texturesSet3[2] = DrawCircleTexture(128, Color.blue, true);
				texturesSet3[3] = DrawCircleTexture(256, Color.blue, true);
				break;
			case 3:
				texturesSet1[0] = DrawCircleTexture(32, Color.cyan, false);
				texturesSet1[1] = DrawCircleTexture(64, Color.cyan, true);
				texturesSet1[2] = DrawCircleTexture(128, Color.cyan, true);
				texturesSet1[3] = DrawCircleTexture(256, Color.cyan, true);

				texturesSet2[0] = DrawCircleTexture(32, Color.black, false);
				texturesSet2[1] = DrawCircleTexture(64, Color.black, true);
				texturesSet2[2] = DrawCircleTexture(128, Color.black, true);
				texturesSet2[3] = DrawCircleTexture(256, Color.black, true);

				texturesSet3[0] = DrawCircleTexture(32, Color.magenta, false);
				texturesSet3[1] = DrawCircleTexture(64, Color.magenta, true);
				texturesSet3[2] = DrawCircleTexture(128, Color.magenta, true);
				texturesSet3[3] = DrawCircleTexture(256, Color.magenta, true);
				break;
			default:
				texturesSet1[0] = DrawCircleTexture(32, Color.red, false);
				texturesSet1[1] = DrawCircleTexture(64, Color.red, true);
				texturesSet1[2] = DrawCircleTexture(128, Color.red, true);
				texturesSet1[3] = DrawCircleTexture(256, Color.red, true);

				texturesSet2[0] = DrawCircleTexture(32, Color.green, false);
				texturesSet2[1] = DrawCircleTexture(64, Color.green, true);
				texturesSet2[2] = DrawCircleTexture(128, Color.green, true);
				texturesSet2[3] = DrawCircleTexture(256, Color.green, true);

				texturesSet3[0] = DrawCircleTexture(32, Color.blue, false);
				texturesSet3[1] = DrawCircleTexture(64, Color.blue, true);
				texturesSet3[2] = DrawCircleTexture(128, Color.blue, true);
				texturesSet3[3] = DrawCircleTexture(256, Color.blue, true);
				break;
		}
	}

	// generates a circle texture for bubbles
	private Texture2D DrawCircleTexture (int size, Color32 color, bool gradient)
	{
		Texture2D texture = new Texture2D(size, size, TextureFormat.ARGB32, false);

		int r = size / 2; // radius
		int ox = size / 2, oy = size / 2; // origin

		for (int y = -r; y <= r; y++)
		{
			for (int x = -r; x <= r; x++)
			{
				if (x * x + y * y <= r * r) texture.SetPixel(ox + x, oy + y, new Color32(color.r, color.g, color.b, gradient ? (byte)(r + y) : (byte)255));
				else texture.SetPixel(ox + x, oy + y, new Color32(0, 0, 0, 0));
			}
		}

		texture.Apply();

		return texture;
	}
}