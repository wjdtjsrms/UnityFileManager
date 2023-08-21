namespace JSGCode.File
{
    using JSGCode.Model;
    using JSGCode.Util;
    using System.Collections.Generic;
    using System.IO;
    using UnityEngine;

    public class FileDataReader
    {
        #region Constructor
        public FileDataReader()
        {
            BetterStreamingAssets.Initialize();
        }
        #endregion

        #region Method : Get File Data
        public Dictionary<string, JsonFileManagingHelper<T, U>> GetFile<T, U>(string folderPath, string currentUserID) where T : ContainerModel<U>, new() where U : class, new()
        {
            var messageDic = new Dictionary<string, JsonFileManagingHelper<T, U>>();

            foreach ((string name, string path) file in GetFileNameAndPathInAccountFolder(folderPath, currentUserID))
            {
                JsonFileManagingHelper<T, U> newMessageHelper = new JsonFileManagingHelper<T, U>(folderPath + file.path);

                var data = newMessageHelper.ReadFileData();
                data.GetPrevData();
                data.ModelSubject.AddObserver(newMessageHelper);

                messageDic.Add(file.name, newMessageHelper);
            }
            return messageDic;
        }
        #endregion

        #region Method : File
        private string[] GetFiles(string folderName)
        {
            try
            {
                return Directory.GetFiles(folderName);
            }
            catch (DirectoryNotFoundException)
            {
                Debug.LogFormat("Directory not found on {0}. Create new directory", folderName);
                Directory.CreateDirectory(folderName);
                return Directory.GetFiles(folderName);
            }
        }

        private List<(string, string)> GetFileNameAndPathInAccountFolder(string folderPath, string accountID)
        {
            string[] filePathsInFolder = GetFiles(folderPath + accountID);
            List<(string, string)> fileDatas = new List<(string, string)>();

            if (filePathsInFolder != null && filePathsInFolder.Length > 0)
            {
                foreach (string filePath in filePathsInFolder)
                {
                    string[] splitedPath = filePath.Split('/', Path.DirectorySeparatorChar);

                    string fileName = splitedPath[splitedPath.Length - 1].Split('.')[0];
                    string filePathInMyAccountFolder = string.Format("{0}/{1}", splitedPath[splitedPath.Length - 2], splitedPath[splitedPath.Length - 1]);
                    fileDatas.Add((fileName, filePathInMyAccountFolder));
                }
            }
            return fileDatas;
        }
        #endregion
    }
}