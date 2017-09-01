/*
 * @author Valentin Simonov / http://va.lent.in/
 */

using System.Collections;
using UnityEngine;

namespace Assets.TouchScript.Examples._misc.Scripts
{
    /// <exclude />
    public class KillMe : MonoBehaviour
    {
        public float Delay = 1f;

        private IEnumerator Start()
        {
            if (Delay != 0) yield return new WaitForSeconds(Delay);
            Destroy(gameObject);
        }
    }
}