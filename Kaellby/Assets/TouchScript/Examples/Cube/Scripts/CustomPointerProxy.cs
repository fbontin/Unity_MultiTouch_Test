/*
 * @author Valentin Simonov / http://va.lent.in/
 */

using Assets.TouchScript.Scripts.Behaviors.Cursors;
using Assets.TouchScript.Scripts.Pointers;

namespace Assets.TouchScript.Examples.Cube.Scripts 
{
    /// <exclude />
    public class CustomPointerProxy : PointerCursor
    {
        protected override void updateOnce(IPointer pointer) {
            if (pointer.InputSource is RedirectInput) Hide();
            
            base.updateOnce(pointer);
        }
    }
}