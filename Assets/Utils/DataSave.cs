using UnityEngine;

namespace Utils
{
    static class DataSave
    {
        public static void SaveData(string key, string value)
        {
            PlayerPrefs.SetString(key,value);
            PlayerPrefs.Save();
        }
        public static void SaveData(string key, int value)
        {
            PlayerPrefs.SetInt(key,value);
            PlayerPrefs.Save();
        }
        public static void SaveData(string key, float value)
        {
            PlayerPrefs.SetFloat(key,value);
            PlayerPrefs.Save();
        }

        public static string LoadDataString(string key)
        {
            return PlayerPrefs.GetString(key);
        }
        public static int LoadDataInt(string key)
        {
            return PlayerPrefs.GetInt(key);
        }
        public static float LoadDataFloat(string key)
        {
            return PlayerPrefs.GetFloat(key);
        }
    }
}
