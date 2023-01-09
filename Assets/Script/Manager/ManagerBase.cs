namespace JSGCode.Base
{
    using UnityEngine;

    public class ManagerBase<T> : SingletonMonoBehaviour<T>, IManager where T : MonoBehaviour
    {
        #region Consturctor
        protected ManagerBase() { }
        #endregion

        #region Method : Mono
        protected override void OnDestroy()
        {
            base.OnDestroy();
            Release();
        }
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