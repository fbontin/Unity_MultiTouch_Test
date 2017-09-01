/*
 * @author Valentin Simonov / http://va.lent.in/
 */

using Assets.TouchScript.Scripts.Layers;
using UnityEngine;

namespace Assets.TouchScript.Examples.Cube.Scripts 
{
    /// <exclude />
    public class Init : MonoBehaviour 
    {
        void Start () {
            var d = GetComponent<LayerDelegate>();
			var go = GameObject.Find("Scene Camera");
            go.GetComponent<TouchLayer>().Delegate = d;
            go = GameObject.Find("Camera");
			go.GetComponent<TouchLayer>().Delegate = d;
        }
    }
}