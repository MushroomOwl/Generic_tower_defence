using System;
using UnityEngine;

namespace TD
{
    [CreateAssetMenu(fileName = "UIEventsBus")]
    public class UIEventsBus : ScriptableObject
    {
        public EventHandler<OnRequestBuildTowerArgs> RequestBuildTower;

        public class OnRequestBuildTowerArgs : EventArgs
        {
            public Vector2 position;
            public TowerProperties towerProps;
        }

        public void OnRequestBuildTower(Vector2 pos, TowerProperties props)
        {
            RequestBuildTower?.Invoke(this, new OnRequestBuildTowerArgs { position = pos, towerProps = props });
        }
    }
}
