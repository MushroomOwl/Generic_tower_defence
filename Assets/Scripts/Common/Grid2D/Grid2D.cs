using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

namespace TD
{
    public class Grid2D<TCellObject>
    {
        private Vector2Int _Dimensions;

        private int _Width => _Dimensions.x;
        private int _Height => _Dimensions.y;

        private float _CellSize;
        private Vector2 _Origin;

        private List<List<TCellObject>> _Values;

        public Grid2D(int width, int height, float cellSize, Vector2 origin, Func<TCellObject> createCellObject)
        {
            _Dimensions = new Vector2Int(width, height);
            _CellSize = cellSize;
            _Origin = origin;

            _Values = new List<List<TCellObject>>(_Width);
            for (int x = 0; x < _Width; x++)
            {
                _Values.Add(new List<TCellObject>(Enumerable.Repeat(default(TCellObject), _Height)));
                for (int y = 0; y < _Height; y++)
                {
                    _Values[x][y] = createCellObject();
                }
            }
        }
        public Grid2D(int width, int height, float cellSize, Vector3 origin, Func<TCellObject> createCellObject) : this(width, height, cellSize, (Vector2)origin, createCellObject) { }
        public Grid2D(Vector2Int dimensions, float cellSize, Vector2 origin, Func<TCellObject> createCellObject) : this(dimensions.x, dimensions.y, cellSize, origin, createCellObject) { }
        public Grid2D(Vector2Int dimensions, float cellSize, Vector3 origin, Func<TCellObject> createCellObject) : this(dimensions.x, dimensions.y, cellSize, (Vector2)origin, createCellObject) { }

        public Vector2Int GetDimensions()
        {
            return _Dimensions;
        }

        public int GetHeight()
        {
            return _Dimensions.y;
        }
        public int GetWidth()
        {
            return _Dimensions.x;
        }

        private void GetCellGridCoordinates(Vector2 position, out int x, out int y)
        {
            Vector2 diff = position - _Origin;

            x = Mathf.FloorToInt(diff.x / _CellSize);
            y = Mathf.FloorToInt(diff.y / _CellSize);
        }

        public Vector2Int GetCellGridCoordinates(Vector2 position)
        {
            GetCellGridCoordinates(position, out int x, out int y);
            return new Vector2Int(x, y);
        }

        private Vector2 GetCellWorldPosition(int x, int y)
        {
            return new Vector2(x, y) * _CellSize + _Origin;
        }

        public Vector2 GetCurrentCellCenterFromInternalPoint(Vector2 position)
        {
            GetCellGridCoordinates(position, out int x, out int y);
            position = GetCellWorldPosition(x, y);
            position.x += _CellSize / 2;
            position.y += _CellSize / 2;
            return position;
        }

        public Vector2 GetCurrentCellCenterFromGridCoords(Vector2Int coords)
        {
            Vector2 position = GetCellWorldPosition(coords.x, coords.y);
            position.x += _CellSize / 2;
            position.y += _CellSize / 2;
            return position;
        }

        public void SetCellValue(int x, int y, TCellObject value)
        {
            if (x < 0 || x >= _Values.Count) return;
            if (y < 0 || y >= _Values[x].Count) return;

            _Values[x][y] = value;
        }

        public void SetCellValue(Vector2Int position, TCellObject value)
        {
            SetCellValue(position.x, position.y, value);
        }

        public void SetCellValueWorld(Vector2 position, TCellObject value)
        {
            GetCellGridCoordinates(position, out int x, out int y);
            SetCellValue(x, y, value);
        }

        public TCellObject GetCellValue(int x, int y)
        {
            if (x < 0 || x >= _Values.Count) return default(TCellObject);
            if (y < 0 || y >= _Values[y].Count) return default(TCellObject);

            return _Values[x][y];
        }

        public TCellObject GetCellValue(Vector2Int position)
        {
            return GetCellValue(position.x, position.y);
        }

        public TCellObject GetCellValueWorld(Vector2 position)
        {
            GetCellGridCoordinates(position, out int x, out int y);
            return GetCellValue(x, y);
        }

        public TCellObject this[int x, int y]
        {
            get
            {
                return GetCellValue(x, y);
            }
        }

# if UNITY_EDITOR
        public void DrawGizmos()
        {            
            for (int x = 0; x < _Values.Count; x++)
            {
                Debug.DrawLine(GetCellWorldPosition(x, 0), GetCellWorldPosition(x, _Height), Color.white, 100f);
                for (int y = 0; y < _Values[x].Count; y++)
                {
                    if (_Values[x][y] == null)
                    {
                        continue;
                    }

                    TDTile tile = _Values[x][y] as TDTile;
                    if (tile == null)
                    {
                        continue;
                    }

                    if (tile.GetProps()?.TileType == TDTileType.Ground)
                    {
                        Debug.DrawLine(GetCellWorldPosition(x, y), GetCellWorldPosition(x + 1, y + 1), Color.green, 100f);
                        continue;
                    }

                    if (tile.GetProps()?.TileType == TDTileType.Road)
                    {
                        Debug.DrawLine(GetCellWorldPosition(x, y), GetCellWorldPosition(x + 1, y + 1), Color.blue, 100f);
                    }

                    if (tile.GetProps()?.TileType == TDTileType.Unspecified)
                    {
                        Debug.DrawLine(GetCellWorldPosition(x, y), GetCellWorldPosition(x + 1, y + 1), Color.red, 100f);
                    }
                }
            }

            for (int y = 0; y < _Values[0].Count; y++)
            {
                Debug.DrawLine(GetCellWorldPosition(0, y), GetCellWorldPosition(_Width, y), Color.white, 100f);
            }

            Debug.DrawLine(GetCellWorldPosition(0, _Height), GetCellWorldPosition(_Width, _Height), Color.white, 100f);
            Debug.DrawLine(GetCellWorldPosition(_Width, 0), GetCellWorldPosition(_Width, _Height), Color.white, 100f);
        }
#endif
    }
}
