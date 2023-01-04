namespace JSGCode.File
{
    using JSGCode.Base;
    using JSGCode.Model;
    using JSGCode.Util;
    using System.Collections.Generic;
    using System.IO;
    using UnityEngine;

    public class FileDataManager : ManagerBase<FileDataManager>
    {
        #region Method : Test
        public void Awake()
        {
            Init();
        }
        #endregion

        #region Member
        protected Dictionary<string, JsonFileManagingHelper<MessageContainerModel, MessageModel>> messageFilesDic = new Dictionary<string, JsonFileManagingHelper<MessageContainerModel, MessageModel>>();
        #endregion

        #region Property
        public IEnumerable<JsonFileManagingHelper<MessageContainerModel, MessageModel>> MessageList => messageFilesDic.Values;
        #endregion

        #region Method : Implements
        public override void Init()
        {
            base.Init();
            BetterStreamingAssets.Initialize();
            messageFilesDic = GetMessageDic();

            var messageHelper = GetMessageHelper(StringValues.TestTargetID) ?? CreateMessageFileContainer(StringValues.TestID, StringValues.TestTargetID);
            messageHelper.ReadFileData().AddMessage(StringValues.TestID, "Test");
            messageHelper.ReadFileData().AddMessage(StringValues.TestTargetID, "TestTarget");
        }
        #endregion

        #region Method : Get File Data
        private Dictionary<string, JsonFileManagingHelper<MessageContainerModel, MessageModel>> GetMessageDic() => GetFile<MessageContainerModel, MessageModel>();



        private Dictionary<string, JsonFileManagingHelper<T, U>> GetFile<T,U>() where T : ContainerModel<U>, new() where U : class, new()
        {
            var messageDic = new Dictionary<string, JsonFileManagingHelper<T, U>>();

            foreach ((string name, string path) file in GetFileNameAndPathInAccountFolder(StringValues.MessageDataFolderPath))
            {
                JsonFileManagingHelper<T, U> newMessageHelper = new JsonFileManagingHelper<T, U>(file.path);
                var data = newMessageHelper.ReadFileData();
                data.GetPrevData();
                data.ModelSubject.AddObserver(newMessageHelper);

                messageDic.Add(file.name, newMessageHelper);
            }
            return messageDic;
        }
        #endregion

        #region Method : Get or Create Message Helper
        public JsonFileManagingHelper<MessageContainerModel, MessageModel> GetMessageHelper(string key)
        {
            if (messageFilesDic.ContainsKey(key))
                return messageFilesDic[key];

            return null;
        }

        public JsonFileManagingHelper<MessageContainerModel, MessageModel> CreateMessageFileContainer(string folderName, string fileName)
        {
            if (messageFilesDic.ContainsKey(fileName))
                return null;

            MessageContainerModel newContainer = new MessageContainerModel(StringValues.TestID, fileName);
            string pathName = string.Format("{0}/{1}.{2}", folderName, fileName, "json");
            JsonFileManagingHelper<MessageContainerModel, MessageModel> helper = new JsonFileManagingHelper<MessageContainerModel, MessageModel>(pathName, newContainer);
            messageFilesDic.Add(fileName, helper);

            newContainer.ModelSubject.AddObserver(helper);

            return helper;
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

        private List<(string, string)> GetFileNameAndPathInAccountFolder(string folderPath)
        {
            string[] filePathsInFolder = GetFiles(folderPath + StringValues.TestID);
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