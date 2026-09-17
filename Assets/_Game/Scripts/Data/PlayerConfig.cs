using System.IO.Enumeration;
using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName="PlayerConfig",menuName="Game/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [Header("移动")]
        public float moveSpeed = 6f;
        public float dashSpeed = 18f;
        public float dashTimer = 0.18f;
        public float iFrameTime = 0.25f;//无敌时间


        [Header("战斗")]
        public int maxHp = 100;
        public float range = 1.2f;//中心离自己多远
        public float radius = 0.8f;//探测球半径
        public int attackDamage = 10;
    }
}