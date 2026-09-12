using UnityEngine;
using System;
using TMPro;

namespace Game.Gameplay
{
    public class PlayerDash : MonoBehaviour
    {
        //事件
        public event Action DashStarted;

        //速度
        [SerializeField] private float dashSpeed = 18f;
        //时间
        [SerializeField] private float dashTimer = 0.18f;
        //无敌时间
        [SerializeField] private float iFrameTime = 0.25f;
        //组件
        private PlayerInput _input;
        // 剩余冲刺时间
        private float _dashTimer;
        //上一帧是否按下
        private bool _wasPressed;
        //是否冲刺
        public bool IsDashing => _dashTimer > 0f;
        //是否无敌
        public bool IsInvulnerable => IsDashing;

        //获取组件
        private void Awake()
        {
            _input = GetComponent<PlayerInput>();
        }

        private void Update()
        {
            //冲刺剩余时间
            _dashTimer -= Time.deltaTime;
            if(_input.DashPressedThisFrame && !_wasPressed && !IsDashing)
            {
                _dashTimer = dashTimer;
                DashStarted?.Invoke();
            }
            _wasPressed = _input.DashPressedThisFrame;
        }

        //面向鼠标冲刺
        private void LateUpdate()
        {
            if (IsDashing)
                transform.position += transform.forward * (dashSpeed * Time.deltaTime);
        }
    }
}
