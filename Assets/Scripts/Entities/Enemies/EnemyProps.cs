using UnityEngine;

namespace TD
{
    [CreateAssetMenu(fileName = "enemy_")]
    public class EnemyProps : ScriptableObject
    {
        [SerializeField] private string _Name;
        public string Name => _Name;

        [SerializeField] private RuntimeAnimatorController _AnimController;
        public RuntimeAnimatorController AnimController => _AnimController;

        [SerializeField] private float _AnimSpeed = 1;
        public float AnimSpeed => _AnimSpeed;

        [SerializeField] private Sprite _Sprite;
        public Sprite Sprite => _Sprite;

        [SerializeField] private bool _SpriteFlipped;
        public bool SpriteFlipped => _SpriteFlipped;

        [SerializeField] private float _MovementSpeed = 1;
        public float MovementSpeed => _MovementSpeed;

        [SerializeField] private int _MaxHP = 1;
        public int MaxHP => _MaxHP;

        [SerializeField] private float _Size = 1;
        public float Size => _Size;

        [SerializeField] private int _Reward = 1;
        public int Reward => _Reward;
    }
}
