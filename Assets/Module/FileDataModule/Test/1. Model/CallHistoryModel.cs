namespace JSGCode.FileDataModule.Test
{
    [System.Serializable]
    public class CallHistoryModel
    {
        #region Mmmbers : data
        public string targetUserID;
        public string callResult;
        public string date;
        #endregion

        #region Constructor
        public CallHistoryModel() { }

        public CallHistoryModel(string targetUserID, string callResult)
        {
            this.targetUserID = targetUserID;
            this.callResult = callResult;
            date = System.DateTime.Now.ToString();
        }
        #endregion
    }
}