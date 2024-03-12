using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

namespace TD
{
    public class SavesHandler
    {
        [Serializable]
        public struct SaveItem
        {
            public string ID;
            public string Data;
        }

        [Serializable]
        public class SaveData
        {
            public string Date = DateTime.Now.ToString();
            public List<SaveItem> Data = new List<SaveItem>();
        }

        private IFileManager _fileManager;

        public SavesHandler(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        public void Save()
        {
            List<IPersistantData> objects = FindAllSavableObjects();

            SaveData objectsData = new SaveData();
            foreach(IPersistantData obj in objects)
            {
                objectsData.Data.Add(new SaveItem() { ID = obj.GetUniqueID(), Data = obj.PackData()});
            }

            string jsonString = JsonUtility.ToJson(objectsData);

            // TODO: At the moment there will be only autosave, later 
            // extend method to make manual saves
            _fileManager.WriteFile(jsonString, "autosave");
        }

        public void Load()
        {
            // TODO: At the moment there will be only autosave, later 
            // extend method to make manual saves
            _fileManager.ReadFile(out string jsonString, "autosave");

            List<IPersistantData> objects = FindAllSavableObjects();

            Dictionary<string, IPersistantData> objectsByID = new Dictionary<string, IPersistantData>();
            foreach (IPersistantData obj in objects)
            {
                objectsByID.Add(obj.GetUniqueID(), obj);
            }

            SaveData objectsData = JsonUtility.FromJson<SaveData>(jsonString);
            foreach (SaveItem saveBox in objectsData.Data)
            {
                if (!objectsByID.ContainsKey(saveBox.ID))
                {
                    continue;
                }
                objectsByID[saveBox.ID].UnpackData(saveBox.Data);
            }
        }

        private List<IPersistantData> FindAllSavableObjects()
        {
            // NOTE: At the moment saving only persistent game data such as level progression etc 
            // which is stored in scriptable objects.
            // TODO: Resource.LoadAll is "heavy" operation in terms of performance - need to find a better solution
            // to handle objects.
            // Probably Button in editor to add references to all IPersistantData scriptable objects will be sufficient. 
            return Resources.LoadAll<ScriptableObject>("ScriptableObjects").OfType<IPersistantData>().ToList();
        }

        public void ResetData()
        {
            List<IPersistantData> objects = FindAllSavableObjects();

            foreach (var obj in objects) {
                obj.ResetState();
            }
        }
    }
}
