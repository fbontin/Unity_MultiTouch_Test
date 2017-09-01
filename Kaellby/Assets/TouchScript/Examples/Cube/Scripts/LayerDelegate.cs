/*
 * @author Valentin Simonov / http://va.lent.in/
 */

using Assets.TouchScript.Scripts.InputSources;
using Assets.TouchScript.Scripts.Layers;
using Assets.TouchScript.Scripts.Pointers;
using UnityEngine;

namespace Assets.TouchScript.Examples.Cube.Scripts
{
    /// <exclude />
    public class LayerDelegate : MonoBehaviour, ILayerDelegate
    {

        public RedirectInput Source;
        public TouchLayer RenderTextureLayer;

        public bool ShouldReceivePointer(TouchLayer layer, IPointer pointer)
        {
            if (layer == RenderTextureLayer)
				return pointer.InputSource == (IInputSource)Source;
			return pointer.InputSource != (IInputSource)Source;
        }
    }
}
