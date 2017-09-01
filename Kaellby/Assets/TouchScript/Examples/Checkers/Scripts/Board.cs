/*
 * @author Valentin Simonov / http://va.lent.in/
 */

using Assets.TouchScript.Scripts.Gestures.TransformGestures;
using UnityEngine;

namespace Assets.TouchScript.Examples.Checkers.Scripts
{
    /// <exclude />
    public class Board : MonoBehaviour
    {
        private PinnedTransformGesture gesture;

        private void OnEnable()
        {
            gesture = GetComponent<PinnedTransformGesture>();
            gesture.Transformed += transformedHandler;
        }

        private void OnDisable()
        {
            gesture.Transformed -= transformedHandler;
        }

        private void transformedHandler(object sender, System.EventArgs e)
        {
            transform.localRotation *= Quaternion.AngleAxis(gesture.DeltaRotation, gesture.RotationAxis);
        }
    }
}