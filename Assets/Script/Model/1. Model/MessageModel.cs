namespace JSGCode.Model
{
    [System.Serializable]
    public class MessageModel
    {
        #region Mmmbers : data
        public string sender;
        public string message;
        public string date;
        #endregion

        #region Constructor
        public MessageModel() { }

        public MessageModel(string sender, string message)
        {
            this.sender = sender;
            this.message = message;
            date = System.DateTime.Now.ToString();
        }
        #endregion
    }
}