using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TD
{
    public class GlobalMapManager : MonoBehaviour
    {
        [SerializeField] private GridProps _gridSettings;
        [SerializeField] private Grid _unityGrid;

        [SerializeField] private Tile _openPathTile;

        [SerializeField] private Tilemap _background;
        [SerializeField] private Tilemap _objects;

        [SerializeField] private float _cellSizeModifier;

        private Grid2D<bool> _grid;

        [SerializeField] private List<LevelSelector> _levels;
        [SerializeField] private GameObject _mapStart;

        [SerializeField] private TDTileProps[] _tilePropsReference;
        private Dictionary<TileBase, TDTileProps> _tileToProps;

        private Vector2Int[] _directions = new Vector2Int[4]
        {
            Vector2Int.left,
            Vector2Int.up,
            Vector2Int.down,
            Vector2Int.right,
        };

        protected void Initialize()
        {
            _tileToProps = new Dictionary<TileBase, TDTileProps>();

            TDTileProps unspecifiedTileProps = null;
            foreach (var props in _tilePropsReference)
            {
                props.AddToDictionary(ref _tileToProps);
                if (props.TileType == TDTileType.Unspecified)
                {
                    unspecifiedTileProps = props;
                }
            }

            if (unspecifiedTileProps == null)
            {
                throw new NullReferenceException("Missing sriptable object for unspecified tiles");
            }

            _grid = new Grid2D<bool>(
                _background.size.x, 
                _background.size.y,
                // TODO: Not a good solution here. Fix required.
                _background.cellSize.x * _cellSizeModifier, 
                (Vector3)_background.cellBounds.min * _cellSizeModifier, 
                () => false);

            Dictionary<string, LevelSelector> levelPositions = new Dictionary<string, LevelSelector>();
            foreach (var level in _levels)
            {
                Vector2Int levelCoords = _grid.GetCellGridCoordinates(level.transform.position);
                levelPositions.Add(levelCoords.ToString(), level);
            }

            // NOTE: We're going by the road on global map until we find
            // unavailable level. Once found we'll make it available and that's it.

            Vector2Int coords = _grid.GetCellGridCoordinates(_mapStart.transform.position);
            int maxIterations = 200;
            int iteration = 0;

            while (true)
            {
                iteration++;
                
                if (iteration > maxIterations)
                {
                    Debug.LogWarning("Global map initialization failed due to endless loop");
                    break;
                }

                levelPositions.TryGetValue(coords.ToString(), out var level);
                if (level != null && !level.IsLevelAvailable())
                {
                    level.MakeAvailable();
                    break;
                }

                if (level != null && !level.IsLevelCompleted())
                {
                    break;
                }

                _grid.SetCellValue(coords, true);

                if (level == null)
                {
                    _objects.SetTile(new Vector3Int(coords.x + _background.cellBounds.xMin, coords.y + _background.cellBounds.yMin, 0), _openPathTile);
                }

                var newCoords = GetNextTile(coords);
                if (newCoords == null)
                {
                    break;
                }

                coords = (Vector2Int)newCoords;
            }
        }

        private Vector2Int? GetNextTile(Vector2Int currentTile)
        {
            foreach (Vector2Int direction in _directions)
            {
                Vector2Int nextCoords = currentTile + direction;
                if (!_grid.IsCordsInside(nextCoords))
                {
                    continue;
                }

                var tilebase = _background.GetTile(new Vector3Int(nextCoords.x + _background.cellBounds.xMin, nextCoords.y + _background.cellBounds.yMin, 0));
                _tileToProps.TryGetValue(tilebase, out var tileProps);
                var isRoad = tileProps?.TileType == TDTileType.Road;

                // NOTE: At the moment road have no branching, so it's not necessare to
                // check all available routes. Once we found one - that's it.
                if (!_grid[nextCoords] && isRoad)
                {
                    return nextCoords;    
                }
            }

            return null;
        }

        private void Awake()
        {
            _gridSettings.ApplyTo(ref _unityGrid);
        }

        private void Start()
        {
            Initialize();
        }
    }
}
