/*
 * @author Valentin Simonov / http://va.lent.in/
 */

using UnityEngine;

namespace Assets.TouchScript.Examples.Portal.Scripts
{
    /// <exclude />
    public class Vortex : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var planet = other.GetComponent<Planet>();
            if (planet == null) return;
            planet.Fall();
        }
    }
}