namespace JSGCode.Test
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using JSGCode.Base;
    using JSGCode.File;
    using JSGCode.Model;
    using NUnit.Framework;
    using UnityEngine;
    using UnityEngine.TestTools;

    public class FileManagerTest
    {
        #region Test Object
        private readonly MessageModel testMessageModel1 = new MessageModel(StringValues.TestID, "Test_Message adfjlka;jdf;~!!#~$%~$@~$#`");
        private readonly MessageModel testMessageModel2 = new MessageModel(StringValues.TestTargetID, "Test_Messa\age2 adfjlka;jdf;~!!#~$\n%~afaf\n$@~$#`");
        #endregion

        #region Method : Initialize
        [UnityTest, Order(1)]
        public IEnumerator FileManagerCreate()
        {
            // Arrange
            FileDataManager fileManager = FileDataManager.Instance;

            // Act
            fileManager.Init();

            // Assert
            Assert.IsNotNull(fileManager);
            Assert.IsTrue(FileDataManager.Instance.IsInit);

            yield return null;
        }
        #endregion

        #region Method : Test Message
        [UnityTest]
        public IEnumerator FileManagerMessageCreate()
        {
            // Arrange
            var messageHelper = FileDataManager.Instance.GetMessageHelper(StringValues.TestTargetID);

            // Act
            messageHelper?.ReadFileData().AddMessage(testMessageModel1);
            messageHelper?.ReadFileData().AddMessage(testMessageModel2);

            // Assert
            var readData = messageHelper?.ReadFileData().Models.ToArray();

            Assert.IsNotNull(messageHelper);

            Assert.IsTrue(testMessageModel1.sender.Equals(readData[0].sender));
            Assert.IsTrue(testMessageModel1.message.Equals(readData[0].message));

            Assert.IsTrue(testMessageModel2.sender.Equals(readData[1].sender));
            Assert.IsTrue(testMessageModel2.message.Equals(readData[1].message));

            messageHelper.DeleteFile();
            yield return null;
        }
        #endregion
    }

}