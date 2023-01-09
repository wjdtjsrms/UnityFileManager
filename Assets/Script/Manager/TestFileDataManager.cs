namespace JSGCode.File
{
    using JSGCode.Base;
    using JSGCode.Model;
    using JSGCode.Util;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class TestFileDataManager : ManagerBase<TestFileDataManager>
    {
        #region Member
        private FileDataReader fileDataReader;
        
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

            fileDataReader = new FileDataReader();
            messageFilesDic = GetMessageDic(StringValues.TestID);
            callHistoryFilesDic = GetCallHistoryDic(StringValues.TestID);
        }

        public override void Release()
        {
            base.Release();

            fileDataReader = null;
            messageFilesDic = null;
            callHistoryFilesDic = null;
        }
        #endregion
        
        #region Method : Get File Data
        private Dictionary<string, JsonFileManagingHelper<MessageContainerModel, MessageModel>> GetMessageDic(string currentUserID) => fileDataReader.GetFile<MessageContainerModel, MessageModel>(StringValues.MessageDataFolderPath, currentUserID);
        private Dictionary<string, JsonFileManagingHelper<CallHistoryContainerModel, CallHistoryModel>> GetCallHistoryDic(string currentUserID) => fileDataReader.GetFile<CallHistoryContainerModel, CallHistoryModel>(StringValues.CallHistoryDataFolderPath, currentUserID);
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
            if (callHistoryFilesDic.ContainsKey(callDate))
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
    }

}