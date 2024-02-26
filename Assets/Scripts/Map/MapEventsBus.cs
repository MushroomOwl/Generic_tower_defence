using System;
using UnityEngine;
using UnityEngine.Events;

namespace TD
{
    [CreateAssetMenu(fileName = "MapEventsBus")]
    public class MapEventsBus: ScriptableObject
    {
        [SerializeField] private UnityEvent _ShowBuilder;
        public UnityEvent ShowBuilder => _ShowBuilder;

        [SerializeField] private UnityEvent _HideBuilder;
        public UnityEvent HideBuilder => _HideBuilder;

        public EventHandler<OnBuilderGridClickEventArgs> BuilderGridClick;

        public class OnBuilderGridClickEventArgs: EventArgs
        {
            public Vector2 position;  
        }

        public void OnHideBuilder()
        {
            _HideBuilder?.Invoke();
        }

        public void OnShowBuilder()
        {
            _ShowBuilder?.Invoke();
        }

        public void OnBuilderGridClick(Vector2 pos)
        {
            BuilderGridClick?.Invoke(this, new OnBuilderGridClickEventArgs { position = pos });
        }
    }
}
