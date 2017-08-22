using System.Collections;
using System.IO;
using UnityEngine;

public class LoadAssetBundlesAsync: MonoBehaviour 
{
	public string path;
	public int bundle_start = 495;
	public int bundle_end = 516;

	void Start ()
	{
		//print( Application.dataPath );

		//LoadFromFolders (bundle_start, bundle_end);
		for (int b = bundle_start; b <= bundle_end; b++ )
		{
			StartCoroutine( LoadFolderAsync(b) );
		}
	}


	IEnumerator LoadFolderAsync(int b)
	{
		//print( Path.Combine(path, b.ToString()) );

		var bundleLoadRequest = AssetBundle.LoadFromFileAsync(Path.Combine(path, b.ToString()) );
		yield return bundleLoadRequest;

		var myLoadedAssetBundle = bundleLoadRequest.assetBundle;
		if (myLoadedAssetBundle == null)
		{
			Debug.Log ("Failed to load AssetBundle: " + Path.Combine (path, b.ToString() ));
			yield break;
		}

		var assetLoadRequest = myLoadedAssetBundle.LoadAllAssetsAsync<GameObject>();
		yield return assetLoadRequest;

		//GameObject prefab = assetLoadRequest.asset as GameObject;
		Object[] objs = assetLoadRequest.allAssets;

		//Instantiate(prefab);
		foreach (Object obj in objs) {
			Instantiate (obj, transform); // instantiate as child to this
		}

		myLoadedAssetBundle.Unload(false);
	}

	IEnumerator LoadAsync(int b, string a)
	{
		//print( Path.Combine(path, b.ToString()) );
		// exempel: Assets/C3/15/8124/map_15_8124_13787.dae

		var bundleLoadRequest = AssetBundle.LoadFromFileAsync(Path.Combine(path, b.ToString()) );
		yield return bundleLoadRequest;

		var myLoadedAssetBundle = bundleLoadRequest.assetBundle;
		if (myLoadedAssetBundle == null)
		{
			Debug.Log ("Failed to load AssetBundle: " + Path.Combine (path, b.ToString() ));
			yield break;
		}

		var assetLoadRequest = myLoadedAssetBundle.LoadAssetAsync<GameObject>(a);
		yield return assetLoadRequest;

		//GameObject prefab = assetLoadRequest.asset as GameObject;
		Object[] objs = assetLoadRequest.allAssets;

		//Instantiate(prefab);
		foreach (Object obj in objs) {
			Instantiate (obj, transform); // instantiate as child to this
		}

		myLoadedAssetBundle.Unload(false);
	}

}
