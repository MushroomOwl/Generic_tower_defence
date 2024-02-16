using System.Collections.Generic;
using UnityEngine;

namespace TD
{
    public abstract class SpawnerBase : EntityWithTimer
    {
        public enum SpawnMode
        {
            Start,
            Loop
        }

        protected abstract GameObject GetSpawnedEntity();

        [SerializeField] private Area2D _Area;
        [SerializeField] private SpawnMode _Mode;
        [SerializeField] private int _SpawnsAtOnce;
        [SerializeField] private float _MaxSpawns;

        [SerializeField] private float _RespawnTime;
        private static string _RespawnTimerName = "rt";

        private List<GameObject> _Spawns;

        private void Awake()
        {
            _Spawns = new List<GameObject>();
        }

        private void Start()
        {
            if (_Mode == SpawnMode.Start)
            {
                SpawnEntities();
                return;
            }

            SpawnEntities();
            AddTimer(_RespawnTimerName, _RespawnTime);
            AddCallback(_RespawnTimerName, SpawnEntities);
        }

        private void Update()
        {
            _Spawns.RemoveAll(v => v == null);
        }

        private void SpawnEntities()
        {
            for (int i = 0; i < _SpawnsAtOnce && _Spawns.Count < _MaxSpawns; i++)
            {
                Vector2 spawnPoint = _Area.GetRandomPointInside();
                GameObject entity = GetSpawnedEntity();
                entity.transform.position = new Vector3(spawnPoint.x, spawnPoint.y, 0);
                _Spawns.Add(entity);
            }
        }
    }
}
