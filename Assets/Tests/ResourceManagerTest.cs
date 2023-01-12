namespace JSGCode.Test
{
    using JSGCode.File;
    using NUnit.Framework;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.TestTools;

    public class ResourceManagerTest
    {
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
            Assert.IsTrue(TestFileDataManager.Instance.IsInit);

            yield return null;
        }
        #endregion
    }
}