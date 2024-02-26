using UnityEngine;

namespace TD
{
    // TODO: STUB, should be reworked
    internal class EventCleaner: MonoBehaviour
    {
        [SerializeField] private EnemiesEventsBus _EnemiesEventsBus;
        [SerializeField] private PlayerEventsBus _PlayerEventsBus;
        [SerializeField] private UIEventsBus _UIEventsBus;
        [SerializeField] private MapEventsBus _MapEventsBus;
        [SerializeField] private InputBus _InputBus;

        private void OnDestroy()
        {
            _EnemiesEventsBus.EnemyKilled = null;
            _EnemiesEventsBus.EnemySpawned.RemoveAllListeners();
            _EnemiesEventsBus.EnemyReachedBase.RemoveAllListeners();
            _PlayerEventsBus.LivesChanged.RemoveAllListeners();
            _PlayerEventsBus.GoldChanged.RemoveAllListeners();
            _UIEventsBus.RequestBuildTower = null;
            _MapEventsBus.BuilderGridClick = null;
            _MapEventsBus.HideBuilder.RemoveAllListeners();
            _MapEventsBus.ShowBuilder.RemoveAllListeners();
            _InputBus.BuildCall.RemoveAllListeners();
            _InputBus.LMBClick.RemoveAllListeners();
        }
    }
}
