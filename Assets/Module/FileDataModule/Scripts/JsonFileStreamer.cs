namespace JSGCode.FileDataModule
{
    using System;
    using System.IO;
    using UnityEngine;

    public class JsonFileStreamer<T>
    {
        public static T ReadFile(string path)
        {
            if (File.Exists(path))
                return JsonUtility.FromJson<T>(File.ReadAllText(path));

            return default(T);
        }

        public static void WriteFile(string path, T data)
        {
            try
            {
                File.WriteAllText(path, JsonUtility.ToJson(data, true));
            }
            catch (DirectoryNotFoundException ex)
            {
                Debug.LogError("DIrectory not found : " + ex.StackTrace);
            }
            catch (Exception ex)
            {
                Debug.LogError("Write file error with exception: " + ex.Message);
            }
        }

        public void CopyWrite(string originPath, string copyPath)
        {
            if (File.Exists(copyPath))
                File.Delete(copyPath);

            File.Copy(originPath, copyPath);
        }

        public static void ClearFile(string path)
        {
            try
            {
                File.WriteAllText(path, string.Empty);
            }
            catch (DirectoryNotFoundException ex)
            {
                Debug.LogError("DIrectory not found : " + ex.StackTrace);
            }
            catch (Exception ex)
            {
                Debug.LogError("Write file error with exception: " + ex.Message);
            }
        }
    }
}