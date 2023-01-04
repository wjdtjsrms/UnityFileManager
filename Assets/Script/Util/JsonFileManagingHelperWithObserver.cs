namespace JSGCode.Util
{
    using JSGCode.Base;
    using JSGCode.Model;

    public class JsonFileManagingHelperWithObserver<T, U> : JsonFileManagingHelper<T>, IDataObserver<T>
        where T : ContainerModel<U>, new() where U : class, new()
    {
        #region Constrructor
        public JsonFileManagingHelperWithObserver(string path) : base(path)
        {
            savedFilePath = path;
        }
        public JsonFileManagingHelperWithObserver(string path, T data) : this(path)
        {
            this.data = data;
        }
        #endregion

        #region Overriding
        public override void Reset()
        {
            data.ModelSubject.RemoveObserver((IDataObserver<ContainerModel<U>>)this);
            base.Reset();
        }

        public override void WriteFileData(T data)
        {
            data.SetSerializable();
            base.WriteFileData(data);
        }
        #endregion

        #region Method : Observer
        public void Notify(T data)
        {
            WriteFileData(data);
        }
        #endregion
    }
}