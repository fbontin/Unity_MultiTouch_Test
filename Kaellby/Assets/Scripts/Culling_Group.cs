using UnityEngine;

namespace Assets.Scripts
{
	public class Culling_Group : MonoBehaviour {

		private CullingGroup group;
		public GameObject 	myCamera;
		public float		delta = 2040.0f;
		public int 			amount = 96;

		private float sphereRadius;
		private Vector3 midPos;

		// Use this for initialization
		void Start () 
		{
			group = new CullingGroup();
			if (myCamera)
				group.targetCamera = myCamera.GetComponent<Camera>();
			else
				group.targetCamera = Camera.main;

			sphereRadius = Mathf.Sqrt(2.0f)*delta/2.0f;
			midPos = new Vector3(delta/2.0f, 0f, delta/2.0f);

			SetOutSpheres();

		}

		private void SetOutSpheres()
		{
			//print(transform.childCount);
			BoundingSphere[] spheres = new BoundingSphere[transform.childCount];

			for (int i = 0; i < transform.childCount; i++ )
			{
				spheres[i] = new BoundingSphere(transform.GetChild(i).position + midPos, sphereRadius);

			}

			group.SetBoundingSpheres(spheres);
			group.SetBoundingSphereCount(transform.childCount);
			group.onStateChanged = StateChangedMethod;


		}

		private void StateChangedMethod(CullingGroupEvent evt)
		{
			if(evt.hasBecomeVisible)
			{
				//Debug.LogFormat("Sphere {0} has become visible!", evt.index);
				//transform.GetChild(evt.index).GetComponent<MeshRenderer>().enabled =true;

			}
			if(evt.hasBecomeInvisible)
			{
				//Debug.LogFormat("Sphere {0} has become invisible!", evt.index);
				//transform.GetChild(evt.index).GetComponent<MeshRenderer>().enabled =false;
			}
		}
		

		void OnApplicationQuit() 
		{
			if (group != null)
			{
				group.Dispose();
				group = null;
			}
		}

	}
}
