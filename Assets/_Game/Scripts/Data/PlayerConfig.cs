using System.IO.Enumeration;
using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName="PlayerConfig",menuName="Game/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [Header("移动")]
        public float moveSpeed = 6f;
        public float dashSpeed = 10f;
        public float dashTimer = 0.5f;
        public float dashDelay = 0.1f;
        public float iFrameTime = 0.25f;//无敌时间


        [Header("战斗")]
        public int maxHp = 100;
        public float comboWindow = 1f;
        public float attackRange = 2.5f;//中心离自己多远
        public int[] attackDamage = { 12, 15, 10, 20 };
    }
}