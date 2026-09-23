using Game.Core;
using Game.Data;
using System;
using UnityEngine;

namespace Game.Gameplay
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        //血量事件，血条订阅
        public event Action<int, int> OnHpChanged;

        [SerializeField] private PlayerConfig _config;

        public int MaxHp { get; private set; }
        public int CurHp { get; private set; }

        private Animator _anim;
        private bool _dead;

        //死亡事件
        public event Action Died;

        private void Awake()
        {
            _anim = GetComponent<Animator>();
        }

        void Start()
        {
            MaxHp = _config.maxHp;
            CurHp = MaxHp;
            OnHpChanged?.Invoke(CurHp, MaxHp);
        }

        public void TakeDamage(int damage)
        {
            if (_dead) return;
            CurHp = Mathf.Max(0, CurHp - damage);
            if (CurHp <= 0) Die();
            else _anim.SetTrigger("Hit");
            OnHpChanged?.Invoke(CurHp, MaxHp);
        }

        private void Die()
        {
            _dead = true;
            _anim.SetBool("IsDead", true);
            Died?.Invoke();

            GetComponent<PlayerAttack>()?.OnPlayerDied();
            GetComponent<PlayerMotor>().enabled = false;
            GetComponent<PlayerDash>().enabled = false;
        }

        public bool IsDead=> _dead;
    }
}