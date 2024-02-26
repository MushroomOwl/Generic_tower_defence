using System.Collections.Generic;
using UnityEngine;

namespace TD
{
    public class AreaPath2D : MonoBehaviour
    {
        [SerializeField] private List<Area2D> _Areas;

        public int Length => _Areas.Count;
        public Area2D this[int i] { get => _Areas[i]; }
    }
}
