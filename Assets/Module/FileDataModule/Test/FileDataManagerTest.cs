namespace JSGCode.FileDataModule.Test
{
    using NUnit.Framework;
    using System.Collections;
    using System.Linq;
    using UnityEngine.TestTools;

    public class FileDataManagerTest
    {
        #region Test Object
        private readonly MessageModel testMessageModel1 = new MessageModel(TestStringValues.TestID, "Test_Message adfjlka;jdf;~!!#~$%~$@~$#`");
        private readonly MessageModel testMessageModel2 = new MessageModel(TestStringValues.TestTargetID, "Test_Messa\age2 adfjlka;jdf;~!!#~$\n%~afaf\n$@~$#`");

        private readonly CallHistoryModel testCallHistoryModel1 = new CallHistoryModel(TestStringValues.TestTargetID, CallResult.Rejected.ToString());
        private readonly CallHistoryModel testCallHistoryModel2 = new CallHistoryModel(TestStringValues.TestTargetID, CallResult.Receive.ToString());
        #endregion

        #region Method : Initialize
        [UnityTest, Order(1)]
        public IEnumerator FileManagerCreate()
        {
            // Arrange
            TestFileDataManager fileManager = TestFileDataManager.Instance;

            // Act
            fileManager.Init();

            // Assert
            Assert.IsNotNull(fileManager);

            yield return null;
        }
        #endregion

        #region Method : Test Message
        [UnityTest]
        public IEnumerator FileManagerMessageTest()
        {
            // Arrange
            var messageHelper = TestFileDataManager.Instance.GetMessageHelper(TestStringValues.TestTargetID);

            // Write Message
            messageHelper?.ReadFileData().AddModel(testMessageModel1);
            messageHelper?.ReadFileData().AddModel(testMessageModel2);

            // Read Message
            var readData = messageHelper?.ReadFileData().Models.ToArray();

            // Assert
            Assert.IsNotNull(messageHelper);

            Assert.IsTrue(testMessageModel1.sender.Equals(readData[0].sender));
            Assert.IsTrue(testMessageModel1.message.Equals(readData[0].message));

            Assert.IsTrue(testMessageModel2.sender.Equals(readData[1].sender));
            Assert.IsTrue(testMessageModel2.message.Equals(readData[1].message));

            messageHelper.DeleteFile();
            yield return null;
        }
        #endregion

        #region Method : Test CallHistory
        [UnityTest]
        public IEnumerator FileManagerCallHistoryTest()
        {
            // Arrange
            var callHistoryHelper = TestFileDataManager.Instance.GetCallHistoryHelper(TestStringValues.TestDate);

            // Write Message
            callHistoryHelper?.ReadFileData().AddModel(testCallHistoryModel1);
            callHistoryHelper?.ReadFileData().AddModel(testCallHistoryModel2);

            // Read Message
            var readData = callHistoryHelper?.ReadFileData().Models.ToArray();

            // Assert
            Assert.IsNotNull(callHistoryHelper);

            Assert.IsTrue(testCallHistoryModel1.targetUserID.Equals(readData[0].targetUserID));
            Assert.IsTrue(testCallHistoryModel1.callResult.Equals(readData[0].callResult));

            Assert.IsTrue(testCallHistoryModel2.targetUserID.Equals(readData[1].targetUserID));
            Assert.IsTrue(testCallHistoryModel2.callResult.Equals(readData[1].callResult));

            callHistoryHelper.DeleteFile();
            yield return null;
        }
        #endregion
    }
}