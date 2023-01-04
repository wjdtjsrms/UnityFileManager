namespace JSGCode.Util
{
    using System;
    using System.IO;
    using UnityEngine;
    using JSGCode.Base;

    public class JsonFileManagingHelperBase<T> where T : new()
    {
        #region Methods
        protected string savedFilePath;
        protected T data;
        #endregion

        #region Constructor
        public JsonFileManagingHelperBase(string path)
        {
            savedFilePath = path;
        }

        ~JsonFileManagingHelperBase() { Reset(); }
        #endregion

        #region Method : Public
        public virtual void WriteFileData(T data)
        {
            try
            {
                this.data = data;
                JsonFileStreamer<T>.WriteFile(savedFilePath, data);
            }
            catch (Exception ex)
            {
                Debug.LogError("Write file error with exception: " + ex.Message);
            }
        }

        public virtual void ClearFileData()
        {
            try
            {
                JsonFileStreamer<T>.ClearFile(savedFilePath);
            }
            catch (Exception ex)
            {
                Debug.LogError("Clear file error with exception: " + ex.Message);
            }
        }

        public virtual T ReadFileData()
        {
            if (data == null)
                data = JsonFileStreamer<T>.ReadFile(savedFilePath);

            return data;
        }

        public virtual void DeleteFile()
        {
            if (File.Exists(savedFilePath))
                File.Delete(savedFilePath);
        }

        public virtual void Reset()
        {
            data = default;
            savedFilePath = null;
        }
        #endregion
    }
}