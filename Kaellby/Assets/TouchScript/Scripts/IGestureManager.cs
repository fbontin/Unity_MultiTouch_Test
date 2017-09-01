/*
 * @author Valentin Simonov / http://va.lent.in/
 */

using Assets.TouchScript.Scripts.Gestures;

namespace Assets.TouchScript.Scripts
{
    /// <summary>
    /// Core manager which controls gesture recognition in hierarchy.
    /// </summary>
    public interface IGestureManager
    {
        /// <summary>
        /// Gets or sets the global gesture delegate.
        /// </summary>
        /// <value> Gesture delegate. </value>
        IGestureDelegate GlobalGestureDelegate { get; set; }
    }
}