using System.Collections.Generic;
using UnityEngine;

namespace TD
{
    public class EnemyPatrolSpawner : SpawnerBase
    {
        [SerializeField] private Enemy _EnemyPrefab;
        [SerializeField] private List<EnemyProps> _EnemyPropsList;
        [SerializeField] private AreaPath2D _PatrolPath;

        protected override GameObject GetSpawnedEntity()
        {
            int index = Random.Range(0, _EnemyPropsList.Count);

            Enemy enemy = Instantiate(_EnemyPrefab);
            enemy.ApplySetup(_EnemyPropsList[index]);

            AIControllerEnemy enemyAI = enemy.GetComponent<AIControllerEnemy>();
            if (enemyAI != null)
            {
                enemyAI.SetPath(_PatrolPath);
            }

            return enemy.gameObject;
        }
    }
}
