namespace JSGCode.Util
{
    using JSGCode.Base;
    using JSGCode.Model;

    public class JsonFileManagingHelper<T, U> : JsonFileManagingHelperBase<T>, IDataObserver<ContainerModel<U>>
        where T : ContainerModel<U>, new() where U : class, new()
    {
        #region Constrructor
        public JsonFileManagingHelper(string path) : base(path)
        {
            savedFilePath = path;
        }

        public JsonFileManagingHelper(string path, T data) : this(path)
        {
            this.data = data;
        }
        #endregion

        #region Overriding
        public override void Reset()
        {
            data.ModelSubject.RemoveObserver(this);
            base.Reset();
        }

        public override void WriteFileData(T data)
        {
            data.SetSerializable();
            base.WriteFileData(data);
        }
        #endregion

        #region Method : Observer
        public void Notify(ContainerModel<U> data)
        {
            WriteFileData((T)data);
        }
        #endregion
    }
}