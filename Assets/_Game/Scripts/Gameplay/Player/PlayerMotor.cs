using Game.Core;
using UnityEngine;
using Game.Data;
namespace Game.Gameplay
{
    public class PlayerMotor : MonoBehaviour
    {
        //移动速度
        [SerializeField] private PlayerConfig config;
        private float speed = 6f;
        //玩家输入
        private PlayerInput _input;
        private void Awake()
        {
            speed = config.moveSpeed;
        }

        void Update()
        {
            //面向鼠标
            FaceMouse();
            //移动
            Vector2 axis = InputService.Instance.Move;
            Vector3 move = new Vector3(axis.x, 0f, axis.y) * (speed * Time.deltaTime);
            transform.position += move;
        }

        //面向鼠标函数
        private void FaceMouse()
        {
            Plane ground = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(ground.Raycast(ray,out float enter))
            {
                Vector3 aim = ray.GetPoint(enter);
                Vector3 dir=(aim - transform.position).FlattenY();
                if(dir.sqrMagnitude > 0.001f)
                {
                    transform.forward = dir;
                }
            }
        }
    }
}