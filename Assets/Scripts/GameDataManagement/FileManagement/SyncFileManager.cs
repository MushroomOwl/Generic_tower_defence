using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System;

namespace TD
{
    public class SyncFileManager: IFileManager
    {
        private string _operatingDirectory = "./Saves";
        private string _defaultExt = ".save";

        public List<string> ListFiles()
        {
            return new List<string>();
        }


        public void WriteFile(in string data, string filename)
        {
            string fullPath = Path.Combine(_operatingDirectory, string.Concat(filename, _defaultExt));
            try
            {
                using (FileStream stream = new FileStream(fullPath, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Write(data);
                    }
                }
            } catch (Exception e)
            {
                Debug.LogWarning("Unexpected error reading file " + e.Message + " - " + e.GetType().Name);
            }
        }

        public void ReadFile(out string data, string filename)
        {
            string fullPath = Path.Combine(_operatingDirectory, string.Concat(filename, _defaultExt));
            try
            {
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        data = reader.ReadToEnd();
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning("Unexpected error reading file " + e.Message + " - " + e.GetType().Name);
                data = null;
            }
        }

        private List<IPersistantData> FindAllSavableObjects()
        {
            return UnityEngine.Object.FindObjectsOfType<ScriptableObject>().OfType<IPersistantData>().ToList();
        }
    }
}
