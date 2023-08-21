namespace JSGCode.FileDataModule
{
    using JSGCode.Util;
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    public class ContainerModel<T> : IJsonSerializable where T : class, new()
    {
        #region Members : Data(Serializing field)
        [SerializeField] protected T[] models;
        #endregion

        #region Member
        protected BasicDataSubject<ContainerModel<T>> modelSubject = new BasicDataSubject<ContainerModel<T>>();
        protected List<T> modelList;
        #endregion

        #region Properties
        public IEnumerable<T> Models => modelList;
        public BasicDataSubject<ContainerModel<T>> ModelSubject => modelSubject;
        #endregion

        #region Consturctor
        public ContainerModel() { modelList = new List<T>(); }
        ~ContainerModel()
        {
            modelSubject = null;
            models = null;
        }
        #endregion

        #region Methods
        public virtual void AddModel(T newModel)
        {
            modelList.Add(newModel);
            modelSubject.NotifyObservers(this);
        }

        public virtual T GetRecentMessage()
        {
            return modelList.Count == 0 ? null : modelList[modelList.Count - 1];
        }

        public virtual void GetPrevData()
        {
            if (models.Length != 0)
                modelList.AddRange(models);
        }

        public void ClearData()
        {
            modelList.Clear();
        }
        #endregion

        #region Implemernts
        public virtual void SetSerializable()
        {
            if (modelList.Count != 0)
                models = modelList.ToArray();
        }
        #endregion
    }
}