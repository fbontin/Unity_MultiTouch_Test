using System.Collections;
using System.IO;
using UnityEngine;

namespace Assets.Scripts
{
	public class LoadAssetsAsync: MonoBehaviour 
	{
		public string path; // example: C:\3D-Obj\AssetBundles\C3
		public int level = 15;
		public int bundle_start = 8124;
		public int bundle_end = 8124;
		public int asset_start = 13787;
		public int asset_end = 13787;
		private string bundle_path;
		private string asset_name; // exempel: Assets/C3/15/8124/map_15_8124_13787.dae

		void Start ()
		{
			bundle_path = Path.Combine( path, level.ToString() );

			for (int b = bundle_start; b <= bundle_end; b++ )
			{
				print (Path.Combine (bundle_path, b.ToString ()));
				asset_name = "Assets/C3/"+level.ToString() + "/" + b.ToString() + "/map_"+ level.ToString() + "_" + b.ToString() +"_";

				for (int a = asset_start; a <= asset_end; a++ )
				{
					print ("    "+ asset_name + a.ToString() );
					StartCoroutine( LoadAsync(b, a ) );

				}
			}
		}


		IEnumerator LoadAsync(int b, int a )
		{
			var bundleLoadRequest = AssetBundle.LoadFromFileAsync(Path.Combine(bundle_path, b.ToString()) );
			yield return bundleLoadRequest;

			var myLoadedAssetBundle = bundleLoadRequest.assetBundle;
			if (myLoadedAssetBundle == null)
			{
				Debug.Log ("Failed to load AssetBundle: " + Path.Combine (bundle_path, b.ToString() ));
				yield break;
			}


			//asset_name = "Assets/C3/15/8124/map_15_8124_";


			
			var assetLoadRequest = myLoadedAssetBundle.LoadAssetAsync<GameObject>(asset_name + a.ToString () + ".dae" );
			yield return assetLoadRequest;

			Object[] objs = assetLoadRequest.allAssets;

			foreach (Object obj in objs) {
				Instantiate (obj, transform); // instantiate as child to this
			
			}
			myLoadedAssetBundle.Unload(false);
		}

	}
}
