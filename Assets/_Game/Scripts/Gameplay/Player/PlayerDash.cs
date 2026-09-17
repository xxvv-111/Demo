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

        private void Awake()
        {
            dashSpeed = config.dashSpeed;
            dashTimer = config.dashTimer;
            iFrameTime = config.iFrameTime;
        }

        private void Update()
        {
            //冲刺剩余时间
            _dashTimer -= Time.deltaTime;
            if(InputService.Instance.DashPressedThisFrame && !IsDashing)
            {
                _dashTimer = dashTimer;
                DashStarted?.Invoke();
            }
        }

        //面向鼠标冲刺
        private void LateUpdate()
        {
            if (IsDashing)
                transform.position += transform.forward * (dashSpeed * Time.deltaTime);
        }
    }
}
