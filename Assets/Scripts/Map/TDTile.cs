using UnityEngine;
using UnityEngine.Tilemaps;

namespace TD
{
    public class TDTile
    {
        private TDTileProps _TileProps;
        private TileBase _TileBase;

        private bool _IsOccupied;
        public bool IsOccupied => _IsOccupied;

        public void SetOccupied()
        {
            _IsOccupied = true;
        }

        public TileBase GetTile()
        {
            return _TileBase;
        }

        public TDTileProps GetProps()
        {
            return _TileProps;
        }

        public void Init(TileBase tile, TDTileProps props)
        {
            _TileBase = tile;
            _TileProps = props;
        }
    }
}
