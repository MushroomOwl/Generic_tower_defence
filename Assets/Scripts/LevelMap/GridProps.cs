using UnityEngine;

namespace TD
{
    [CreateAssetMenu(fileName = "gridSettings_")]
    internal class GridProps: ScriptableObject
    {
        [SerializeField] private float _CellSize;
        [SerializeField] private float _CellGap;
        [SerializeField] private Grid.CellLayout _CellLayout;
        [SerializeField] private Grid.CellSwizzle _CellSwizzle;

        public void ApplyTo(ref Grid grid)
        {
            grid.cellSize = new Vector3(_CellSize, _CellSize, 0);
            grid.cellGap = new Vector3(_CellGap, _CellGap, 0);
            grid.cellLayout = _CellLayout;
            grid.cellSwizzle = _CellSwizzle;
        }
    }
}
