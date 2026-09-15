using UnityEngine;
using Game.Core;

namespace Gameplay
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private float range = 1.2f;   //中心离自己多远
        [SerializeField] private float radius = 0.8f;  //探测球半径
        [SerializeField] private int damage = 10;      //伤害

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
            }
        }

        //攻击
        private void Attack()
        {
            Vector3 center = transform.position + transform.forward * range;
            //范围内的碰撞体列表
            Collider[] hits = Physics.OverlapSphere(center, radius);

            foreach(Collider c in hits)
            {
                if(c.TryGetComponent<IDamageable>(out var damageable))
                {
                    damageable.TakeDamage(damage);
                }
            }
        }

        //绘制攻击范围
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Vector3 center = transform.position + transform.forward * range;
            Gizmos.DrawWireSphere(center, radius);
        }   
    }
}
