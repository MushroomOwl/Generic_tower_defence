using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TD
{
    [CreateAssetMenu(fileName = "ScenesInfoContainer")]
    public class ScenesInfoContainer: ScriptableObject
    {
        private List<string> _sceneNames = new List<string>();
        public IReadOnlyList<string> SceneNames => _sceneNames;

        private void OnEnable()
        {
            int sceneCount = SceneManager.sceneCountInBuildSettings;

            for (int i = 0; i < sceneCount; i++)
            {
                string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
                string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
                _sceneNames.Add(sceneName);
            }
        }
    }
}
