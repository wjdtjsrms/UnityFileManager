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
        #region Member
        protected Dictionary<string, JsonFileManagingHelper<MessageContainerModel, MessageModel>> messageFilesDic = new Dictionary<string, JsonFileManagingHelper<MessageContainerModel, MessageModel>>();
        protected Dictionary<string, JsonFileManagingHelper<CallHistoryContainerModel, CallHistoryModel>> callHistoryFilesDic = new Dictionary<string, JsonFileManagingHelper<CallHistoryContainerModel, CallHistoryModel>>();
        #endregion

        #region Property
        public IEnumerable<JsonFileManagingHelper<MessageContainerModel, MessageModel>> MessageList => messageFilesDic.Values;
        public IEnumerable<JsonFileManagingHelper<CallHistoryContainerModel, CallHistoryModel>> CallHistoryList => callHistoryFilesDic.Values;
        #endregion

        #region Method : Implements
        public override void Init()
        {
            base.Init();
            BetterStreamingAssets.Initialize();

            messageFilesDic = GetMessageDic(StringValues.TestID);
            callHistoryFilesDic = GetCallHistoryDic(StringValues.TestID);
        }

        public override void Release()
        {
            base.Release();
            messageFilesDic = null;
        }
        #endregion

        #region Method : Get File Data
        private Dictionary<string, JsonFileManagingHelper<MessageContainerModel, MessageModel>> GetMessageDic(string currentUserID) => GetFile<MessageContainerModel, MessageModel>(StringValues.MessageDataFolderPath, currentUserID);
        private Dictionary<string, JsonFileManagingHelper<CallHistoryContainerModel, CallHistoryModel>> GetCallHistoryDic(string currentUserID) => GetFile<CallHistoryContainerModel, CallHistoryModel>(StringValues.CallHistoryDataFolderPath, currentUserID);


        private Dictionary<string, JsonFileManagingHelper<T, U>> GetFile<T, U>(string folderPath, string currentUserID) where T : ContainerModel<U>, new() where U : class, new()
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

        #region Method : Get or Create Message Helper
        public JsonFileManagingHelper<MessageContainerModel, MessageModel> GetMessageHelper(string targetUserName)
        {
            if (messageFilesDic.ContainsKey(targetUserName))
                return messageFilesDic[targetUserName];

            return CreateMessageFileContainer(StringValues.TestID, targetUserName);
        }

        private JsonFileManagingHelper<MessageContainerModel, MessageModel> CreateMessageFileContainer(string currentUserID, string targetUserName)
        {
            if (messageFilesDic.ContainsKey(targetUserName))
                return null;

            string pathName = StringValues.MessageDataFolderPath + $"{currentUserID}/{targetUserName}.json";

            MessageContainerModel newContainer = new MessageContainerModel(currentUserID, targetUserName);
            JsonFileManagingHelper<MessageContainerModel, MessageModel> helper = new JsonFileManagingHelper<MessageContainerModel, MessageModel>(pathName, newContainer);

            newContainer.ModelSubject.AddObserver(helper);

            messageFilesDic.Add(targetUserName, helper);

            return helper;
        }
        #endregion

        #region Method : Get or Create Message Helper
        public JsonFileManagingHelper<CallHistoryContainerModel, CallHistoryModel> GetCallHistoryHelper(string callDate)
        {
            if(callHistoryFilesDic.ContainsKey(callDate))
                return callHistoryFilesDic[callDate];

            return CreateCallHistorFileContainer(StringValues.TestID, callDate);
        }

        private JsonFileManagingHelper<CallHistoryContainerModel, CallHistoryModel> CreateCallHistorFileContainer(string currentUserID, string callDate)
        {
            if (callHistoryFilesDic.ContainsKey(callDate))
                return null;

            string pathName = StringValues.CallHistoryDataFolderPath + $"{currentUserID}/{callDate}.json";

            CallHistoryContainerModel newContainer = new CallHistoryContainerModel(callDate);
            JsonFileManagingHelper<CallHistoryContainerModel, CallHistoryModel> helper = new JsonFileManagingHelper<CallHistoryContainerModel, CallHistoryModel>(pathName, newContainer);

            newContainer.ModelSubject.AddObserver(helper);

            callHistoryFilesDic.Add(callDate, helper);

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