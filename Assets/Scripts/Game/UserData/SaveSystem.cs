using System;
using System.IO;
using Habits;
using UnityEngine;

namespace Game.UserData
{
    public class SaveSystem
    {
        private UserData _userData;

        private static string FilePath => Path.Combine(Application.persistentDataPath, "userdata.json");

        public UserData GetData()
        {
            if (_userData == null) Debug.LogError("UserData is null! Load() was not called before GetData().");
            return _userData;
        }

        public void Save()
        {
            try
            {
                var json = JsonUtility.ToJson(_userData, true);
                File.WriteAllText(FilePath, json);
                Debug.Log($"[SaveSystem] Saved to {FilePath}");
            }
            catch (Exception e)
            {
                Debug.LogError($"[SaveSystem] Failed to save data: {e}");
            }
        }

        public void Load()
        {
            if (!File.Exists(FilePath))
            {
                Debug.Log("[SaveSystem] Save file not found. Creating new UserData.");
                _userData = new UserData();
                return;
            }

            try
            {
                var json = File.ReadAllText(FilePath);
                Debug.Log($"[SaveSystem] Loaded JSON: {json}");

                _userData = JsonUtility.FromJson<UserData>(json) ?? new UserData();

                // Ensure fields are initialized in case JSON was missing keys
                _userData.SettingsData ??= new SettingsData();
                _userData.TutorialData ??= new TutorialData();
                _userData.UserInventoryData ??= new UserInventoryData();
                _userData.UserHabitsData ??= new UserHabitsData();

                Debug.Log("[SaveSystem] Load successful.");
            }
            catch (Exception e)
            {
                Debug.LogWarning($"[SaveSystem] Failed to load save file. Using new UserData. Error: {e}");
                _userData = new UserData();
            }
        }
    }
}