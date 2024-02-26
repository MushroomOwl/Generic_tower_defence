using UnityEngine;
using static TD.EnemiesEventsBus;

namespace TD
{
    public class Player : MonoSingleton<Player>
    {
        [SerializeField] private PlayerEventsBus _PlayerEventsBus;
        [SerializeField] private EnemiesEventsBus _EnemiesEventsBus;

        [SerializeField] private int _Gold;
        [SerializeField] private int _NumLives;

        public static int Gold => _Instance._Gold;
        public static int NumLives => _Instance._NumLives;

        protected override void Awake()
        {
            base.Awake();
            _EnemiesEventsBus.EnemyKilled += (object sender, OnEnemyKilledArgs args) => ChangeGold(args.reward);
            _EnemiesEventsBus.EnemyReachedBase.AddListener(() => ChangeLives(-1));
        }

        private void Start()
        {
            _PlayerEventsBus.OnGoldChanged();
            _PlayerEventsBus.OnLivesChanged();
        }

        public void ChangeGold(int amount)
        {
            _Gold += amount;
            _PlayerEventsBus.OnGoldChanged();
        }

        public void ChangeLives(int amount)
        {
            _NumLives += amount;
            _PlayerEventsBus.OnLivesChanged();
        }
    }
}

