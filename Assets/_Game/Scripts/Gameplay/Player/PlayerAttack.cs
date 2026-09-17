using Game.Core;
using Game.Data;
using UnityEngine;

namespace Gameplay
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private PlayerConfig config;
        private float range;//中心离自己多远
        private float radius;//探测球半径
        private int damage; //伤害

        private void Awake()
        {
            range = config.range;
            radius = config.radius;
            damage = config.attackDamage;
        }
        void Update()
        {
            if (InputService.Instance.AttackPressedThisFrame)
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
