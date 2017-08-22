using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;  // Most of the utilities we are going to use are contained in the UnityEditor namespace

// We inherit from the AssetPostProcessor class which contains all the exposed variables and event triggers for asset importing pipeline
internal sealed class C3_Model_Importer : AssetPostprocessor 
{
	// Couple of constants used below to be able to change from a single point, you may use direct literals instead of these consts to if you please
	//private const int webTextureSize = 2048;
	//private const int standaloneTextureSize = 4096;
	//private const int iosTextureSize = 1024;
	//private const int androidTextureSize = 1024;


	//-------------Pre Processors

	// This event is raised when a texture asset is imported
	private void OnPreprocessTexture() {

		// I prefix my texture asset's file names with tex, following 3 lines say "if tex is not in the asset file name, do nothing"
		var fileNameIndex = assetPath.LastIndexOf('/');
		var fileName = assetPath.Substring(fileNameIndex + 1);

		if(!fileName.Contains("map_")) 
			return;

		var importer = assetImporter as TextureImporter;

		importer.filterMode = FilterMode.Trilinear;
		importer.anisoLevel = 5;
		//importer.compressionQuality = 100;
	
		importer.wrapMode = TextureWrapMode.Clamp;

		// If you are only using the alpha channel for transparency, uncomment the below line. 
		//importer.alphaIsTransparency = importer.DoesSourceTextureHaveAlpha();
	}

	// This event is raised when a new mesh asset is imported
	private void OnPreprocessModel() {
		
		var fileNameIndex = assetPath.LastIndexOf('/');
		var fileName = assetPath.Substring(fileNameIndex + 1);

		if (!fileName.Contains("map_")) 
			return;
		
		// unbox the assetImporter reference, to a ModelImporter this time
		var importer = assetImporter as ModelImporter;

		importer.animationType = ModelImporterAnimationType.None;
		importer.importAnimation = false;
		importer.normalSmoothingAngle = 90.0f;
		importer.importTangents = ModelImporterTangents.None;
		importer.importNormals = ModelImporterNormals.None;
	}
	 
	// This event is raised every time an audio asset is imported
	private void OnPreprocessAudio() {
	}

	//-------------Post Processors

	// This event is called as soon as the texture asset is imported successfully
	private void OnPostprocessTexture(Texture2D import) {}



	// This event is called as soon as the mesh asset is imported successfully
	private void OnPostprocessModel(GameObject import) 
	{
		if( !import.name.Contains("map_"))
			return;
		
		var firstIndex = assetPath.IndexOf('/') +1;
		//var length = assetPath.LastIndexOf('.') - firstIndex;
		var length = assetPath.LastIndexOf('/') - firstIndex;
		string bundleName = assetPath.Substring(firstIndex, length);

		string level = import.name.Substring( import.name.IndexOf('_')+1 , 2 );
		//Debug.Log( level );

		int layerIndex = 0;
		if (int.TryParse( level, out layerIndex ) )
		{
			//Debug.Log( layerIndex );
			import.layer = layerIndex;
		}

		// Debug.Log( bundleName );

		Vector3 pos;
		pos.x = - import.transform.position.z - 1329000.0f;	
		pos.y = - import.transform.position.y;
		pos.z = - import.transform.position.x - 6172000.0f;


		import.transform.localScale = new Vector3( -import.transform.localScale.x, import.transform.localScale.y, import.transform.localScale.z);
		import.transform.localPosition = new Vector3( 0f, -import.transform.localPosition.y, 0f );

		MeshFilter[] meshFilters = import.GetComponentsInChildren<MeshFilter>();
		foreach (MeshFilter mf in meshFilters) 
		{
			Vector3[] vertices = mf.sharedMesh.vertices;
			for (var i = 0; i < vertices.Length; i++)
			{
				
				vertices[i].z *= -1.0f;
				//vertices[i] = import.transform.TransformPoint( vertices[vertices.Length - i -1] );
			}
			mf.sharedMesh.vertices = vertices;

			//mf.sharedMesh.RecalculateNormals();
			mf.sharedMesh.RecalculateBounds();								 
		}

		MeshRenderer[] meshrends = import.GetComponentsInChildren<MeshRenderer>();
		foreach (MeshRenderer mr in meshrends) 
		{
			mr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
			Material[] mts = mr.sharedMaterials;
			foreach (Material mt in mts){
				mt.color = Color.white;
				mt.SetFloat ( "_Glossiness", 0f);
			}

		}

		//ResetTransform (import);
		import.transform.eulerAngles = new Vector3(-90.0f, -90.0f, 0f);

		import.transform.position = pos;
		//import.transform.eulerAngles = new Vector3(-90.0f, -90.0f, 0f);

		assetImporter.SetAssetBundleNameAndVariant( bundleName, "");

	}


	// This event is called as soon as the audio asset is imported successfully
	private void OnPostprocessAudio(AudioClip import) {}

	private void ResetTransform( GameObject import )
	{
		Vector3 pos;
		pos.x = import.transform.position.x;	
		pos.y = 0f;
		pos.z = import.transform.position.z;

		import.transform.localPosition = new Vector3( 0f, import.transform.localPosition.y, 0f );

		MeshFilter[] meshFilters = import.GetComponentsInChildren<MeshFilter>();
		foreach (MeshFilter mf in meshFilters) 
		{
			Vector3[] vertices = mf.sharedMesh.vertices;
			for (var i = 0; i < vertices.Length; i++)
			{
				vertices[i] = import.transform.TransformPoint( vertices[i] );

			}
			mf.sharedMesh.vertices = vertices;
			mf.sharedMesh.RecalculateNormals();
			mf.sharedMesh.RecalculateBounds();								 
		}

		import.transform.localPosition = pos;
		//import.transform.localRotation = Quaternion.identity;
		import.transform.localScale = Vector3.one;
	}
}
