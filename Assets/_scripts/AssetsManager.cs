using UnityEngine;
using System.Collections;
using System;

public class AssetsManager : MonoBehaviour
{
	public static GameObject BubblePrefab;
	public static GameObject VFXPrefab;
	public static GameObject PointsPrefab;

	private void Start () 
	{
		DontDestroyOnLoad(gameObject);
		// load background from asset bundle 
		StartCoroutine(DownloadAsset("file://" + Application.dataPath + "/AssetBundles/bundle.unity3d"));
	}

	private IEnumerator DownloadAsset (string url)
	{
		// download the file from the URL
		using (WWW www = new WWW(url))
		{
			yield return www;
			if (www.error != null)
				throw new Exception("WWW download had an error:" + www.error);

			// load and retrieve the AssetBundle
			AssetBundle bundle = www.assetBundle;

			// load the objects asynchronously
			AssetBundleRequest request = bundle.LoadAsync("background", typeof(GameObject));
			yield return request;
			GameObject obj = request.asset as GameObject;
			GameObject.Instantiate(obj);

			request = bundle.LoadAsync("vfx", typeof(GameObject));
			yield return request;
			VFXPrefab = request.asset as GameObject;

			request = bundle.LoadAsync("bubble", typeof(GameObject));
			yield return request;
			BubblePrefab = request.asset as GameObject;

			request = bundle.LoadAsync("points", typeof(GameObject));
			yield return request;
			PointsPrefab = request.asset as GameObject;

			// Unload the AssetBundles compressed contents to conserve memory
			bundle.Unload(false);

			Application.LoadLevel(1);
		} 
	}
}