namespace JSGCode.FileDataModule.Test
{
    using UnityEngine;

    #region Test Values
    public enum CallResult { None = -1, Send, Receive, NotResponding, Rejected }

    public class TestStringValues
    {
        public static readonly string MessageDataFolderPath = Application.persistentDataPath + "/Test/Messages/";
        public static readonly string CallHistoryDataFolderPath = Application.persistentDataPath + "/Test/CallHistory/";

        public static readonly string TestID = "TestID";
        public static readonly string TestTargetID = "TestTargetID";
        public static readonly string TestDate = "2022-07-20";
    }
    #endregion
}