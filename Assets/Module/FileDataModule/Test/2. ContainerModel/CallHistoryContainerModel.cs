namespace JSGCode.FileDataModule.Test
{
    using UnityEngine;
    using JSGCode.FileDataModule;

    [System.Serializable]
    public class CallHistoryContainerModel : ContainerModel<CallHistoryModel>
    {
        #region Members : Data(Serializing field)
        [SerializeField] private string callDate;
        #endregion

        #region Properties
        public string CallDate => callDate;
        #endregion

        #region Constructor
        public CallHistoryContainerModel() : base() { }
        public CallHistoryContainerModel(string callDate) : base()
        {
            this.callDate = callDate;
        }
        #endregion

        #region Method
        public void AddCallHistory(string targetUserId, CallResult callResult)
        {
            var newCallHistory = new CallHistoryModel(targetUserId, callResult.ToString());

            modelList.Add(newCallHistory);
            modelSubject.NotifyObservers(this);
        }
        #endregion
    }
}