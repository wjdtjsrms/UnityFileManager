namespace JSGCode.Base
{
    public class Singleton<T> where T : class, new()
    {
        #region Static
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new T();
                }

                return instance;
            }
        }
        #endregion

        #region Constructor
        protected Singleton() { }
        #endregion
    }
}