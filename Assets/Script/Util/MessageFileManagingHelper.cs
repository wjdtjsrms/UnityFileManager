namespace JSGCode.File
{
    using JSGCode.Base;
    using JSGCode.Model;
    using JSGCode.Util;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MessageFileManagingHelper : JsonFileManagingHelperBase<MessageContainerModel>,IDataObserver<MessageContainerModel>
    {
        #region Consturctor
        public MessageFileManagingHelper(string path) : base(path)
        {
            savedFilePath = StringValues.MessageDataFolderPath + path;
        }

        public MessageFileManagingHelper(string path, MessageContainerModel data) : this(path)
        {
            this.data = data;
        }
        #endregion


        #region Method : Observer
        public void Notify(MessageContainerModel data)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}