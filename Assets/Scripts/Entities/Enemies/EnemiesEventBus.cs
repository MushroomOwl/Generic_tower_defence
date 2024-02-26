using System;
using UnityEngine;
using UnityEngine.Events;

namespace TD
{
    [CreateAssetMenu(fileName = "EnemiesEventsBus")]
    public class EnemiesEventsBus : ScriptableObject
    {
        [SerializeField] private UnityEvent _EnemySpawned;
        public UnityEvent EnemySpawned => _EnemySpawned;

        public EventHandler<OnEnemyKilledArgs> EnemyKilled;

        public class OnEnemyKilledArgs: EventArgs {
            public int reward;
        }

        [SerializeField] private UnityEvent _EnemyReachedBase;
        public UnityEvent EnemyReachedBase => _EnemyReachedBase;

        public void OnEnemySpawned()
        {
            EnemySpawned?.Invoke();
        }

        public void OnEnemyKilled(int reward)
        {
            EnemyKilled?.Invoke(this, new OnEnemyKilledArgs { reward = reward });
        }

        public void OnEnemyReachedBase()
        {
            EnemyReachedBase?.Invoke();
        }
    }
}
