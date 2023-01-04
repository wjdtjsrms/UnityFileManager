namespace JSGCode.Model
{
    [System.Serializable]
    public class MessageModel
    {
        #region Mmmbers : data
        public string id;
        public string message;
        public string date;
        #endregion

        #region Constructor
        public MessageModel() { }

        public MessageModel(string id, string message)
        {
            this.id = id;
            this.message = message;
            date = System.DateTime.Now.ToString();
        }
        #endregion
    }
}