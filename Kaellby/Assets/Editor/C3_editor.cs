using UnityEngine;
using System.Collections;
using UnityEditor;

public class C3_editor : Editor 
{     
	
	
/*
	[MenuItem ("C3_editor/Fix transform")]
	static void Fix_transform() 
	{
		Transform[] selection = Selection.GetTransforms( SelectionMode.Deep );
		
		foreach(Transform child in selection)
		{
				Vector3 pos;
				pos.x = - child.localPosition.z - 1329000.0f;	
				pos.y = - child.localPosition.y;
				pos.z = - child.localPosition.x - 6172000.0f;
				child.localPosition = pos;

				child.transform.eulerAngles = new Vector3(-90.0f, -90.0f, 0f);
				
				Vector3 scale = child.localScale;
				scale.x *= -1.0f;
				//scale.z *= -1.0f;
				child.localScale = scale;
		}
	}
 	
*/

	[MenuItem ("C3_editor/Build AssetBundles")]
	static void BuildAllAssetBundles ()
	{
		BuildPipeline.BuildAssetBundles ("AssetBundles", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
	}


/*
  	[MenuItem ("C3_editor/Flip Z in mesh")]
  	static void FlipZ() 
  	{
  		Object[] objs = Selection.GetFiltered( typeof(GameObject),  SelectionMode.Deep); 
  		
  		foreach (GameObject obj in objs)
  		{
  			MeshFilter[] meshFilters = obj.GetComponentsInChildren<MeshFilter>();
  			foreach (MeshFilter mf in meshFilters) 
  			{
  				Vector3[] vertices = mf.sharedMesh.vertices;
  				for (var i = 0; i < vertices.Length; i++)
  				{
  					vertices[i].z *=-1.0f; 
  				}
 				mf.sharedMesh.vertices = vertices;

  				//mf.sharedMesh.RecalculateNormals();
  				mf.sharedMesh.RecalculateBounds();								 
 			}
  		}
  	} 	
*/
/*
	[MenuItem ("C3_editor/ReverseTriangles")]
	static void ReverseTriangles() 
	{
		Object[] objs = Selection.GetFiltered( typeof(GameObject),  SelectionMode.Deep); 

		foreach (GameObject obj in objs)
		{
			MeshFilter[] meshFilters = obj.GetComponentsInChildren<MeshFilter>();
			foreach (MeshFilter mf in meshFilters) 
			{

				for (var sub = 0; sub < mf.sharedMesh.subMeshCount; sub++)
				{
					int[] indices = mf.sharedMesh.GetIndices(sub);
					int[] newindices = mf.sharedMesh.GetIndices(sub);

					//int[] triangles = mf.sharedMesh.triangles;
					//int[] newTris = mf.sharedMesh.triangles;

					for (var i = 0; i < (indices.Length/3); i++)
					{
						//newindices[i] = indices[indices.Length - i -1]; 
						newindices[i*3 ] = indices[ i*3];
						newindices[i*3 +1] = indices[ i*3 +2];
						newindices[i*3 +2] = indices[ i*3 +1];
					}
					//mf.sharedMesh.triangles = newTris;
					mf.sharedMesh.SetIndices( newindices, MeshTopology.Triangles, sub, false);
				}
				mf.sharedMesh.RecalculateNormals();
				mf.sharedMesh.RecalculateBounds();								 
			}
		}
	} 	
	*/
	/*
	[MenuItem ("C3_editor/Reset transform")]    
	static void ResetTransform()
	{
		Object[] objs = Selection.GetFiltered( typeof(GameObject),  SelectionMode.Deep); 

		foreach (GameObject obj in objs) {
		
			Vector3 pos;
			pos.x = obj.transform.position.x;	
			pos.y = 0f;
			pos.z = obj.transform.position.z;

			obj.transform.localPosition = new Vector3 (0f, obj.transform.localPosition.y, 0f);

			MeshFilter[] meshFilters = obj.GetComponentsInChildren<MeshFilter> ();
			foreach (MeshFilter mf in meshFilters) {
				Vector3[] vertices = mf.sharedMesh.vertices;
				for (var i = 0; i < vertices.Length; i++) {
					vertices [i] = obj.transform.TransformPoint (vertices [i]);

				}
				mf.sharedMesh.vertices = vertices;
				mf.sharedMesh.RecalculateNormals ();
				mf.sharedMesh.RecalculateBounds ();								 
			}

			obj.transform.localPosition = pos;
			obj.transform.localRotation = Quaternion.identity;
			obj.transform.localScale = Vector3.one;
		}
	}
 */
/*
	[MenuItem ("C3_editor/Prefab Routine")]     
	static void PrefabRoutine()     
	{     
		//AssetDatabase.CreateFolder("Assets", "Prefab Folder"); 
		Object prefab = EditorUtility.CreateEmptyPrefab("Assets/Prefab Folder/obj.prefab");
		
		if (Selection.activeObject)         
			EditorUtility.ReplacePrefab(Selection.activeGameObject, prefab); 
		
		AssetDatabase.Refresh(); 		
	} 
*/
} 

