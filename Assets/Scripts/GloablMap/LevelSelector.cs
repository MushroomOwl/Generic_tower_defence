using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TD
{
    public class LevelSelector : MonoBehaviour
    {
        [SerializeField] private GameObject _backgroundOutline;
        [SerializeField] private Animator _levelStatAnimation;

        [SerializeField] private LevelProperties _levelInfo;

        [SerializeField] private GameEvent _onLevelLoadCall;

        [SerializeField] private Color _lockedLevelColorOverlay;
        [SerializeField] private SpriteRenderer _visualModel;

        private bool _levelIsAvailable => _levelInfo != null && _levelInfo.Stats.Available;

        private void Start()
        {
            if (!_levelIsAvailable)
            {
                _visualModel.color = _lockedLevelColorOverlay;
            }
            _backgroundOutline.SetActive(false);
            _levelStatAnimation.speed = 0;
        }
        
        public bool IsLevelAvailable()
        {
            return _levelIsAvailable;
        }

        public bool IsLevelCompleted()
        {
            return _levelInfo.Stats.Completed;
        }

        public void MakeAvailable()
        {
            _visualModel.color = Color.white;
            _levelInfo.Stats.MakeAvailable();
        }

        private void OnMouseOver()
        {
            if (!_levelIsAvailable)
            {
                return;
            }
            _backgroundOutline.SetActive(true);
            _levelStatAnimation.speed = 1;
        }

        private void OnMouseExit()
        {
            if (!_levelIsAvailable)
            {
                return;
            }
            _backgroundOutline.SetActive(false);
            _levelStatAnimation.speed = 0;
        }

        private void OnMouseDown()
        {
            if (!_levelIsAvailable)
            {
                return;
            }
            _onLevelLoadCall.Raise(this, _levelInfo);
        }

#if UNITY_EDITOR
        [CustomEditor(typeof(LevelSelector))]
        public class LevelSelectorInspector : Editor
        {
            public override void OnInspectorGUI()
            {
                DrawDefaultInspector();
                EditorGUILayout.Space();
                LevelSelector levelSelector = (LevelSelector)target;

                GUIStyle textAreaStyle = new GUIStyle(EditorStyles.textArea)
                {
                    wordWrap = true
                };

                EditorGUILayout.LabelField("Info", textAreaStyle);

                if (levelSelector._levelInfo == null)
                {
                    return;
                }

                string text = string.Format(
                    "Title      \t{0}\n" +
                    "Completed  \t{1}\n" +
                    "Grade      \t{2}\n" +
                    "Available  \t{3}\n",
                    levelSelector._levelInfo.Title, 
                    levelSelector._levelInfo.Stats.Completed, 
                    levelSelector._levelInfo.Stats.Grade, 
                    levelSelector._levelInfo.Stats.Available
                );

                EditorGUILayout.TextArea(text, textAreaStyle, GUILayout.Height(100));

                if (GUILayout.Button("Make available"))
                {
                    levelSelector._levelInfo.Stats.MakeAvailable();
                }

                if (GUILayout.Button("ResetLevelData"))
                {
                    levelSelector._levelInfo.ResetState();
                }
            }
        }
#endif
    }
}
