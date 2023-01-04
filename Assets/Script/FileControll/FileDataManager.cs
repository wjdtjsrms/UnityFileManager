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
        protected Dictionary<string, JsonFileManagingHelperWithObserver<MessageContainerModel, MessageModel>> messageFilesDic = new Dictionary<string, JsonFileManagingHelperWithObserver<MessageContainerModel, MessageModel>>();
        #endregion

        #region Property
        public IEnumerable<JsonFileManagingHelperWithObserver<MessageContainerModel, MessageModel>> MessageList => messageFilesDic.Values;
        #endregion

        #region Method : Implements
        public override void Init()
        {
            base.Init();
            BetterStreamingAssets.Initialize();
            messageFilesDic = GetMessageDic();

            var messageHelper = GetMessageHelper(StringValues.TestID) ?? CreateMessageFileContainer(StringValues.TestTargetID);
            messageHelper.ReadFileData().AddMessage(StringValues.TestID, "Test");
        }
        #endregion

        #region Method : Get File Data
        private Dictionary<string, JsonFileManagingHelperWithObserver<MessageContainerModel, MessageModel>> GetMessageDic()
        {
            var messageDic = new Dictionary<string, JsonFileManagingHelperWithObserver<MessageContainerModel, MessageModel>>();

            foreach ((string name, string path) file in GetFileNameAndPathInAccountFolder(StringValues.MessageDataFolderPath))
            {
                JsonFileManagingHelperWithObserver<MessageContainerModel, MessageModel> newMessageHelper = new JsonFileManagingHelperWithObserver<MessageContainerModel, MessageModel>(file.path);
                var data = newMessageHelper.ReadFileData();
                data.GetPrevData();
                data.ModelSubject.AddObserver((IDataObserver<ContainerModel<MessageModel>>)newMessageHelper);

                messageDic.Add(file.name, newMessageHelper);
            }
            return messageDic;
        }

        public JsonFileManagingHelperWithObserver<MessageContainerModel, MessageModel> CreateMessageFileContainer(string targetUserId)
        {
            if (messageFilesDic.ContainsKey(targetUserId))
                return null;

            MessageContainerModel newContainer = new MessageContainerModel(StringValues.TestID, targetUserId);
            string fileName = string.Format("{0}/{1}.{2}", StringValues.TestID, targetUserId, "json");
            JsonFileManagingHelperWithObserver<MessageContainerModel, MessageModel> helper = new JsonFileManagingHelperWithObserver<MessageContainerModel, MessageModel>(fileName, newContainer);
            messageFilesDic.Add(targetUserId, helper);

            newContainer.ModelSubject.AddObserver((IDataObserver<ContainerModel<MessageModel>>)helper);

            return helper;
        }


        #endregion

        #region Method : Get File ManagingHelper
        public JsonFileManagingHelperWithObserver<MessageContainerModel, MessageModel> GetMessageHelper(string key)
        {
            if (messageFilesDic.ContainsKey(key))
                return messageFilesDic[key];

            return null;
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