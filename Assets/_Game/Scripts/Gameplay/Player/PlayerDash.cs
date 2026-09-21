using Game.Core;
using Game.Data;
using System;
using TMPro;
using UnityEngine;

namespace Game.Gameplay
{
    public class PlayerDash : MonoBehaviour
    {
        //事件
        public event Action DashStarted;

        [SerializeField] private PlayerConfig config;
        //速度
        private float dashSpeed;
        //时间
        private float dashTimer;
        //无敌时间
        private float iFrameTime;
        //组件
        // 剩余冲刺时间
        private float _dashTimer;
        //是否冲刺
        public bool IsDashing => _dashTimer > 0f;
        //是否无敌
        public bool IsInvulnerable => IsDashing;
        private Animator _anim;

        private void Awake()
        {
            dashSpeed = config.dashSpeed;
            dashTimer = config.dashTimer;
            iFrameTime = config.iFrameTime;
            _anim = GetComponent<Animator>();
        }

        private void Update()
        {
            //冲刺剩余时间
            _dashTimer -= Time.deltaTime;
            if(InputService.Instance.DashPressedThisFrame && !IsDashing)
            {
                _dashTimer = dashTimer;
                DashStarted?.Invoke();
                if (_anim != null) _anim.SetTrigger("Dash");
            }
        }

        //冲刺
        private void LateUpdate()
        {
            if (IsDashing)
                transform.position += transform.forward * (dashSpeed * Time.deltaTime);
        }
    }
}
