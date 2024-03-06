using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TD
{
    public class TDMapManager : MonoBehaviour
    {
        [SerializeField] private Tower _TowerPrefab;

        [SerializeField] private GridProps _GridSettings;
        [SerializeField] private Grid _UnityGrid;

        [SerializeField] private Tile _OverlayTileDefault;
        [SerializeField] private Tile _OverlayTileHover;
        private Vector2Int _OverlayHoverCoords;
        private bool _LockOverlay;

        [SerializeField] private Tilemap _Ground;
        [SerializeField] private Tilemap _PassableDecorations;
        [SerializeField] private Tilemap _ImpassableDecorations;
        [SerializeField] private Tilemap _BuildOverlay;

        private Grid2D<TDTile> _Grid;

        [SerializeField] private TDTileProps[] _TilePropsReference;

        private Dictionary<TileBase, TDTileProps> _TileToProps;

        [SerializeField] private GameEvent _OnHideBuilder;
        [SerializeField] private GameEvent _OnShowBuilder;
        [SerializeField] private GameEvent _OnBuilderGridClick;
        [SerializeField] private GameEvent _OnTowerBuilt;

        protected void Initialize()
        {
            _TileToProps = new Dictionary<TileBase, TDTileProps>();

            TDTileProps unspecifiedTileProps = null;
            foreach (var props in _TilePropsReference)
            {
                props.AddToDictionary(ref _TileToProps);
                if (props.TileType == TDTileType.Unspecified)
                {
                    unspecifiedTileProps = props;
                }
            }

            if (unspecifiedTileProps == null)
            {
                throw new NullReferenceException("Missing sriptable object for unspecified tiles");
            }

            _Grid = new Grid2D<TDTile>(_Ground.size.x, _Ground.size.y, _Ground.cellSize.x, _Ground.cellBounds.min, () => new TDTile());

            for (int x = 0; x < _Grid.GetWidth(); x++)
            {
                for (int y = 0; y < _Grid.GetHeight(); y++)
                {
                    var tilebase = _Ground.GetTile(new Vector3Int(x + _Ground.cellBounds.xMin, y + _Ground.cellBounds.yMin, 0));
                    var impassableTile = _ImpassableDecorations.GetTile(new Vector3Int(x + _Ground.cellBounds.xMin, y + _Ground.cellBounds.yMin, 0));
                    if (tilebase == null)
                    {
                        continue;
                    }
                    var tileprops = _TileToProps.ContainsKey(tilebase) ? _TileToProps[tilebase] : unspecifiedTileProps;
                    _Grid[x, y]?.Init(tilebase, tileprops);

                    if (impassableTile != null)
                    {
                        _Grid[x, y]?.SetOccupied();   
                    }
                }
            }

            for (int x = 0; x < _Grid.GetWidth(); x++)
            {
                for (int y = 0; y < _Grid.GetHeight(); y++)
                {
                    TDTile tile = _Grid[x, y];

                    if (tile == null || tile.IsOccupied || !tile.GetProps().TowerCanBePlaced)
                    {
                        _BuildOverlay.SetTile(new Vector3Int(x + _Ground.cellBounds.xMin, y + _Ground.cellBounds.yMin, 0), null);
                    } else
                    {
                        _BuildOverlay.SetTile(new Vector3Int(x + _Ground.cellBounds.xMin, y + _Ground.cellBounds.yMin, 0), _OverlayTileDefault);
                    }
                }
            }

            _BuildOverlay.gameObject.SetActive(false);
            _Grid.DrawGizmos();
        }

        public void ToggleOverlay() {
            if (_BuildOverlay.gameObject.activeSelf)
            {
                _OnHideBuilder?.Raise(this, null);
                _BuildOverlay.gameObject.SetActive(false);
                _LockOverlay = false;
            } else
            {
                _OnShowBuilder?.Raise(this, null);
                _BuildOverlay.gameObject.SetActive(true);
            }
        }
 
        private void Awake()
        {
            _GridSettings.ApplyTo(ref _UnityGrid);
            Initialize();
        }

        private void Update()
        {
            // TODO _LockOverlay at the moment is stub to prevent placing towers in random poins on map
            // Tower building process should be revorked.
            if (!_BuildOverlay.gameObject.activeSelf || _LockOverlay)
            {
                return;
            }

            Vector2 mousePos = Utilities.GetMouse2DWorldPosition();
            Vector2Int coords = _Grid.GetCellGridCoordinates(mousePos);

            if (coords.x < 0 || coords.x >= _Grid.GetWidth() || coords.y < 0 || coords.y > _Grid.GetHeight())
            {
                return;
            }

            TDTile tile = _Grid[coords.x, coords.y];

            if (tile == null || tile.IsOccupied || !tile.GetProps().TowerCanBePlaced)
            {
                return;
            }

            if (_OverlayHoverCoords != null && _OverlayHoverCoords != coords)
            {
                _BuildOverlay.SetTile(new Vector3Int(_OverlayHoverCoords.x + _Ground.cellBounds.xMin, _OverlayHoverCoords.y + _Ground.cellBounds.yMin, 0), _OverlayTileDefault);
                _OverlayHoverCoords = coords;
            }

            _BuildOverlay.SetTile(new Vector3Int(coords.x + _Ground.cellBounds.xMin, coords.y + _Ground.cellBounds.yMin, 0), _OverlayTileHover);
        }

        public void OnMouseClick()
        {
            if (!_BuildOverlay.gameObject.activeSelf || _LockOverlay)
            {
                return;
            }

            Vector2 mousePos = Utilities.GetMouse2DWorldPosition();

            TDTile tile = _Grid.GetCellValueWorld(mousePos);
            if (tile == null)
            {
                return;
            }

            if (tile.IsOccupied)
            {
                return;
            }

            if (!tile.GetProps().TowerCanBePlaced)
            {
                return;
            }

            _LockOverlay = true;
            _OnBuilderGridClick?.Raise(this, _Grid.GetCurrentCellCenterFromInternalPoint(mousePos));
        }

        public void BuildTowerCall(Component sender, object data)
        {
            if (data is not (Vector2, TowerProperties))
            {
                return;
            }

            (Vector2 position, TowerProperties props) = ((Vector2, TowerProperties))data;

            BuildTower(position, props);
        }

        private void BuildTower(Vector2 position, TowerProperties props)
        {
            // NOTE: At the moment position isn't required here - remove from event??

            if (!_BuildOverlay.gameObject.activeSelf)
            {
                return;
            }

            TDTile tile = _Grid[_OverlayHoverCoords.x, _OverlayHoverCoords.y];
            if (tile == null)
            {
                return;
            }

            if (tile == null)
            {
            }

            if (tile.IsOccupied)
            {
                return;
            }

            if (!tile.GetProps().TowerCanBePlaced)
            {
                return;
            }

            tile.SetOccupied();

            Vector2 nearestCellCenter = _Grid.GetCurrentCellCenterFromGridCoords(_OverlayHoverCoords);

            Tower tower = Instantiate(_TowerPrefab, new Vector3(nearestCellCenter.x, nearestCellCenter.y, 0), Quaternion.identity);
            tower.ApplySetup(props);

            _BuildOverlay.SetTile(new Vector3Int(_OverlayHoverCoords.x + _Ground.cellBounds.xMin, _OverlayHoverCoords.y + _Ground.cellBounds.yMin, 0), null);

            _OnTowerBuilt?.Raise(this, props);

            ToggleOverlay();

            _LockOverlay = false;
        }
    }
}
