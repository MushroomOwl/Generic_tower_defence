using System.Collections.Generic;
using UnityEngine;

namespace TD
{
    public class EnemyPatrolSpawner : SpawnerBase
    {
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private List<EnemyProps> _EnemyPropsList;
        [SerializeField] private AreaPath2D _PatrolPath;

        protected override GameObject GetSpawnedEntity()
        {
            int index = Random.Range(0, _EnemyPropsList.Count);

            Enemy enemy = _enemyPrefab.CloneSelf();
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
