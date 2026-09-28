using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "EnemyAIConfig", menuName = "Game/EnemyAIConfig")]
    public class EnemyAIConfig : ScriptableObject
    {
        public string displayName = "拳击手";
        public int hp = 30;
        public float moveSpeed = 2.5f;
        public float attackRange = 1f;
        public int damage = 10;
        public GameObject prefab;//可选,按表生成怪用
        public GameObject hitEffect;//受击特效
    }
}