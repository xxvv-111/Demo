using UnityEngine;
namespace Game.Gameplay
{
    public class PlayerInput : MonoBehaviour
    {
        public Vector2 moveAxis
        {
            get
            {
                var v = new Vector2(Input.GetAxisRaw("Horizontal"),
                    Input.GetAxisRaw("Vertical"));
                return v.sqrMagnitude > 1f ? v.normalized : v;
            }
        }
        public bool isDashPressed
        {
            get
            {
                return Input.GetKeyDown(KeyCode.Space)||Input.GetMouseButtonDown(1);
            }
        }
    }
}