using UnityEngine;
using System.Collections;
using System;

public class AssetsManager : MonoBehaviour
{
	private void Start () 
	{
		// load background from asset bundle 
		StartCoroutine(DownloadAsset("file://" + Application.dataPath + "/AssetBundles/asb_Background.unity3d", "background"));
	}

	private IEnumerator DownloadAsset (string url, string name)
	{
		// download the file from the URL
		using (WWW www = new WWW(url))
		{
			yield return www;
			if (www.error != null)
				throw new Exception("WWW download had an error:" + www.error);

			// load and retrieve the AssetBundle
			AssetBundle bundle = www.assetBundle;

			// load the object asynchronously
			AssetBundleRequest request = bundle.LoadAsync(name, typeof(GameObject));
			yield return request;

			// instantiate the object
			GameObject obj = request.asset as GameObject;
			GameObject.Instantiate(obj);

			// Unload the AssetBundles compressed contents to conserve memory
			bundle.Unload(false);
		} 
	}
}