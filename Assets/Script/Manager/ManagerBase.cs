namespace JSGCode.Base
{
    using JSGCode.File;
    using UnityEngine;

    public class ManagerBase<T> : SingletonMonoBehaviour<FileDataManager>, IManager where T : MonoBehaviour
    {
        #region Consturctor
        protected ManagerBase() { }
        #endregion

        #region IManager
        public bool IsInit { get; protected set; } = false;

        public virtual void Init()
        {
            IsInit = true;
        }

        public virtual void Release()
        {
            IsInit = false;
        }
        #endregion
    }
}