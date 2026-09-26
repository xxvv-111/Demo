using Game.Core;
using TMPro;
using UnityEngine;

namespace Game.Gameplay.Enemy
{
    public class EnemyMeleeAI : MonoBehaviour, IDamageable
    {
        public enum EState { Idle, Chase, Attack, Hit, Death }

        [Header("数值")]
        [SerializeField] private float aggroRange = 8f;//追击距离
        [SerializeField] private float attackRange = 1f;//攻击距离
        [SerializeField] private float moveSpeed = 3f;//追击速度
        [SerializeField] private int maxHp = 100;

        [Header("攻击三小段（秒）")]
        [SerializeField] private float windupTime = 1f;//前摇
        [SerializeField] private float recoverTime = 1.5f;//后摇

        private Transform _player;//玩家位置
        private PlayerFSM _playerFsm;//玩家状态机
        private EState _st = EState.Idle; //当前状态

        private float _atkT;//攻击状态已经过了多少秒
        private bool _damageDone;//这一刀是否已经结算过伤害
        private float _stunT;//受击硬直还剩多少秒
        private float _flashT;//受击闪白还剩多少秒
        private int _hp;
        private Renderer _ren;//闪白
        private Color _normalColor;//原本颜色
        private CapsuleCollider _col;
        private Animator _anim;

        private void Awake()
        {
            _hp = maxHp;
            _col = GetComponent<CapsuleCollider>();
            _ren = GetComponentInChildren<Renderer>();//颜色一般在子物体/本体的MeshRenderer上
            _anim = GetComponent<Animator>();
            if (_ren != null) _normalColor = _ren.material.color;
        }

        private void Start()
        {
            _player = GameObject.FindWithTag("Player")?.transform;
            if (_player != null) _playerFsm = _player.GetComponent<PlayerFSM>();
        }

        private void Update()
        {
            //玩家位置
            float distSqr = (_player != null)
                ? (_player.position - transform.position).sqrMagnitude
                : 9999f;

            //受击闪白计时：闪的时间一到，把颜色变回原色
            if (_flashT > 0f)
            {
                _flashT -= Time.deltaTime;
                if (_flashT <= 0f && _ren != null) _ren.material.color = _normalColor;
            }

            //行动决策
            switch (_st)
            {
                case EState.Idle://追击
                    if (distSqr <= aggroRange * aggroRange) SetState(EState.Chase);
                    break;

                case EState.Chase://攻击
                    if (distSqr <= attackRange * attackRange)
                    {
                        //攻击复位
                        _atkT = 0f; _damageDone = false;
                        SetState(EState.Attack);
                        _anim.SetTrigger("Attack");
                        break;
                    }
                    MoveTowardPlayer();//继续追
                    break;

                case EState.Attack:
                    TickAttack();//前摇_判定_后摇
                    break;

                case EState.Hit:
                    _stunT -= Time.deltaTime;//硬直倒数
                    if (_stunT <= 0f) SetState(EState.Chase);//回追击
                    break;

                case EState.Death://死亡
                    break;
            }
        }

        private void MoveTowardPlayer()
        {
            if (_player == null) return;
            Vector3 dir = _player.position - transform.position;
            dir.y = 0f;//只在地面走
            if (dir.sqrMagnitude > 0.01f)
            {
                transform.rotation = Quaternion.LookRotation(dir.normalized);//脸朝玩家
                transform.position += dir.normalized * (moveSpeed * Time.deltaTime);
            }
        }

        //攻击
        private void TickAttack()
        {
            if (_player != null)
            {
                Vector3 toP = _player.position - transform.position; toP.y = 0f;
                if (toP.sqrMagnitude > 0.01f) transform.rotation = Quaternion.LookRotation(toP.normalized);
            }
            _atkT += Time.deltaTime;

            //判定时刻，过前摇结算一次伤害
            if (_atkT >= windupTime && !_damageDone)
            {
                _damageDone = true;//一刀只结算一次
                float d = (_player != null)
                    ? (_player.position - transform.position).sqrMagnitude
                    : 9999f;
                //判定玩家还在攻击距离内吗以及玩家无敌
                if (d <= attackRange * attackRange * 1.2f && _playerFsm != null && !_playerFsm.Invulnerable)
                {
                    _playerFsm.TakeDamage(10);//打中玩家
                }
            }

            //后摇结束，回追击状态
            if (_atkT >= recoverTime) SetState(EState.Chase);
        }

        //被玩家打中时调用
        public void TakeDamage(int dmg)
        {
            if (_st == EState.Death) return;//死亡不再被打
            _hp -= dmg;
            FlashRed();//受击闪一下
            KnockBack();//受击后退一点点
            if (_hp <= 0) { SetState(EState.Death); OnDeath(); }
            else { SetState(EState.Hit); _stunT = 0.4f; }//硬直0.4秒
        }

        private void FlashRed()
        {
            if (_ren != null) { _ren.material.color = Color.red; _flashT = 0.15f; }
        }

        private void KnockBack()
        {
            //受击远离玩家
            transform.position -= transform.forward * 0.4f;
        }

        private void OnDeath()
        {
            _col.enabled = false; //关碰撞
            Destroy(gameObject, 1.5f); //1.5秒后销毁尸体
        }

        private void SetState(EState next) { _st = next; }
    }
}