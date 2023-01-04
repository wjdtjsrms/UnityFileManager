namespace JSGCode.Model
{
    using UnityEngine;

    [System.Serializable]
    public class MessageContainerModel : ContainerModel<MessageModel>
    {
        #region Members : Data(Serializing field)
        [SerializeField] private string userID;
        [SerializeField] private string targetUserID;
        #endregion

        #region Properties
        public string UserID => userID;
        public string TargetID => targetUserID;
        #endregion

        #region Constructor
        public MessageContainerModel() : base() { }
        public MessageContainerModel(string userID, string targetUserID) : base()
        {
            this.userID = userID;
            this.targetUserID = targetUserID;
        }
        #endregion

        #region Method
        public void AddMessage(string id, string message)
        {
            var newMessage = new MessageModel(id, message);
            modelList.Add(newMessage);

            modelSubject.NotifyObservers(this);
        }
        #endregion
    }
}