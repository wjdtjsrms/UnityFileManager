namespace JSGCode.Base
{
    using UnityEngine;

    public class StringValues
    {
        public static readonly string MessageDataFolderPath = Application.persistentDataPath + "/Messages/";
        public static readonly string CallHistoryDataFolderPath = Application.persistentDataPath + "/CallHistory/";

        public static readonly string TestID = "TestID";
        public static readonly string TestTargetID = "TestTargetID";
        public static readonly string TestDate = "2022-07-20";
    }

    public enum CallResult { None = -1, Send, Receive, NotResponding, Rejected }
}