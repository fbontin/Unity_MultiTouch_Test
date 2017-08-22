using System.Collections;
using System.IO;
using UnityEngine;

public class LoadAssetBundles: MonoBehaviour 
{
	public string path;
	//public string bundle_name;
	//public string object_name;
	//public string path = "11/";
	public int bundle_start = 495;
	public int bundle_end = 516;

	void Start ()
	{
		//print( Application.dataPath );

		LoadFromFolders (bundle_start, bundle_end);
	}


	public void LoadFromFolders(int b_start, int b_end )
	{
		for (int b = b_start; b <= b_end; b++ )
		{
			var myLoadedAssetBundle = AssetBundle.LoadFromFile( Path.Combine(path, b.ToString() ) );


			if (myLoadedAssetBundle == null) {
				Debug.Log ("Failed to load AssetBundle: " + Path.Combine (path, b.ToString() ));
				//return;
			} else {

				GameObject[] objs = myLoadedAssetBundle.LoadAllAssets<GameObject> ();

				foreach (GameObject obj in objs) {
					Instantiate (obj, transform); // instantiate as child to this
				}

				myLoadedAssetBundle.Unload (false);
			}
			
		}
	}


}
