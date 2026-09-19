using Game.Core;
using Game.Data;
using UnityEngine;

namespace Gameplay
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private PlayerConfig config;
        [SerializeField] private LayerMask enemyMask;
        private float comboWindow;//连击间隔
        private float[] range;//中心离自己多远
        private float[] radius;//探测球半径
        private int damage; //伤害

        public event System.Action<Vector3, int> OnHit;//命中事件

        private int _stage;//第几段攻击
        private float _sinceLast;//过去时间

        private void Awake()
        {
            comboWindow = config.comboWindow;
            range = config.range;
            radius = config.radius;
            damage = config.attackDamage;
        }
        void Update()
        {
            _sinceLast += Time.deltaTime;
            if (_sinceLast > comboWindow) _stage = 0;//超时

            if (InputService.Instance.AttackPressedThisFrame)
            {
                DoAttack();
            }
        }

        //攻击
        private void DoAttack()
        {
            int idx = _stage;
            _stage = (_stage + 1) % 3;
            _sinceLast = 0f;

            Vector3 center = transform.position + transform.forward * range[idx];
            //范围内的碰撞体列表
            Collider[] hits = Physics.OverlapSphere(center, radius[idx],enemyMask);

            Debug.Log($"[Attack]第{idx + 1}段，命中{hits.Length}个");

            foreach(Collider col in hits)
            {
                if(col.TryGetComponent<IDamageable>(out var dmg))
                {
                    dmg.TakeDamage(damage);
                    Vector3 point = col.ClosestPoint(center);
                    OnHit?.Invoke(point, config.attackDamage);
                }
            }
        }

        //绘制攻击范围
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Vector3 center = transform.position + transform.forward * config.range[_stage];
            Gizmos.DrawWireSphere(center, config.radius[_stage]);
        }   
    }
}
