using UnityEngine;

namespace TD
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private Transform _SpawnPoint;
        // [SerializeField] private HUD _HUDPrefab;
        [SerializeField] private GameManager _GameManagerPrefab;
        // [SerializeField] private LevelGUI _LevelGUIPrefab;
        [SerializeField] private InputHandler _InputHandlerPrefab;
        // [SerializeField] private Camera _CameraPrefab;
        [SerializeField] private Player _PlayerPrefab;
        [SerializeField] private GameObject _ReferencesPrefab;

        private void Awake()
        {
            Instantiate(_ReferencesPrefab.gameObject);
            // Instantiate(_HUDPrefab);

            Instantiate(_GameManagerPrefab).Init();
            // Instantiate(_LevelGUIPrefab).Init();

            Instantiate(_InputHandlerPrefab).Init();

            // Instantiate(_CameraPrefab, _SpawnPoint.position, Quaternion.identity);
            Instantiate(_PlayerPrefab);
        }
    }
}
