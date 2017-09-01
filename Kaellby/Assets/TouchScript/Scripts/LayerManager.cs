/*
 * @author Valentin Simonov / http://va.lent.in/
 */

using Assets.TouchScript.Scripts.Core;
using UnityEngine;

namespace Assets.TouchScript.Scripts
{
    /// <summary>
    /// Facade for current instance of <see cref="ILayerManager"/>.
    /// </summary>
    [HelpURL("http://touchscript.github.io/docs/html/T_TouchScript_LayerManager.htm")]
    public sealed class LayerManager : MonoBehaviour
    {
        /// <summary>
        /// Gets the LayerManager instance.
        /// </summary>
        public static ILayerManager Instance
        {
            get { return LayerManagerInstance.Instance; }
        }
    }
}