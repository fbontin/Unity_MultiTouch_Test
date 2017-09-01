/*
 * @author Valentin Simonov / http://va.lent.in/
 */

using Assets.TouchScript.Scripts.Gestures;
using UnityEngine;

namespace Assets.TouchScript.Examples.Taps.Scripts
{
    /// <exclude />
    public class Kick : MonoBehaviour
    {
        public float Force = 3f;
        public ParticleSystem Particles;

        private TapGesture gesture;
        private Rigidbody rb;
		private UnityEngine.Camera activeCamera;

        private void OnEnable()
        {
            rb = GetComponent<Rigidbody>();
			activeCamera = GameObject.Find("Scene Camera").GetComponent<UnityEngine.Camera>();
            gesture = GetComponent<TapGesture>();
            gesture.Tapped += tappedHandler;
        }

        private void OnDisable()
        {
            gesture.Tapped -= tappedHandler;
        }

        private void tappedHandler(object sender, System.EventArgs e)
        {
			var ray = activeCamera.ScreenPointToRay(gesture.ScreenPosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.transform == transform)
            {
                rb.AddForceAtPosition(ray.direction*Force, hit.point, ForceMode.Impulse);
                Instantiate(Particles, hit.point, Quaternion.identity);
            }
        }
    }
}