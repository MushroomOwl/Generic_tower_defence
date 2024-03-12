using System.Linq;
using UnityEditor;
using UnityEngine;

namespace TD
{
    public class LoadSceneButton : MonoBehaviour
    {
        [SerializeField] private GameEvent _onLoadLevelCall;
        [SerializeField] private ScenesInfoContainer _scenesInfo;
        [SerializeField] private int _selectedScene;


        public void LoadSceneCallback()
        {
            _onLoadLevelCall?.Raise(this, _scenesInfo.SceneNames[_selectedScene]);
        }

#if UNITY_EDITOR
        [CustomEditor(typeof(LoadSceneButton))]
        public class LoadSceneButtonInspector : Editor
        {
            public override void OnInspectorGUI()
            {
                DrawDefaultInspector();
                EditorGUILayout.Space();
                LoadSceneButton button = (LoadSceneButton)target;
                button._selectedScene = EditorGUILayout.Popup("Scene", button._selectedScene, button._scenesInfo.SceneNames.ToArray());
                if (GUI.changed)
                {
                    EditorUtility.SetDirty(button);
                }
            }
        }
#endif
    }
}
