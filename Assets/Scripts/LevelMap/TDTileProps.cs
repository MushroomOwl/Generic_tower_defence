using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TD
{
    [CreateAssetMenu(fileName = "tile_")]
    public class TDTileProps : ScriptableObject
    {
        [SerializeField] TDTileType _TileType;
        public TDTileType TileType => _TileType;

        [SerializeField] bool _TowerCanBePlaced;
        public bool TowerCanBePlaced => _TowerCanBePlaced;

        [SerializeField] private TileBase[] _TilesOfType;

        public void AddToDictionary(ref Dictionary<TileBase, TDTileProps> dictionary)
        {
            foreach (var tb in _TilesOfType)
            {
                dictionary.Add(tb, this);
            }
        }
    }
}
