using Game.Gameplay;
using UnityEngine;

public class DashLogger : MonoBehaviour
{
    private PlayerDash _dash;
    private void OnEnable()                        // 成对：启用就订
    {
        _dash = GetComponent<PlayerDash>();
        _dash.DashStarted += OnDash;               // += 订阅
    }
    private void OnDisable()                       // 禁用就退
    {
        _dash.DashStarted -= OnDash;               // -= 退订（M4 生命周期纪律）
    }
    private void OnDash() => Debug.Log("[DashLogger] 冲刺！");
}
