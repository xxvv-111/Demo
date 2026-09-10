using UnityEngine;
namespace Game.Gameplay
{
    public class PlayerMotor : MonoBehaviour
    {
        [SerializeField] private float speed = 6f;
        private PlayerInput _input;
        private void Awake()
        {
            _input = GetComponent<PlayerInput>();
        }

        void Update()
        {
            Vector2 axis = _input.moveAxis;
            Vector3 move = new Vector3(axis.x, 0f, axis.y) * (speed * Time.deltaTime);
            transform.position += move;
        }
    }
}