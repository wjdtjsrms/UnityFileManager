namespace JSGCode.Base
{
    using UnityEngine;

    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        #region Static
        private static T instance;
        public static T Instance
        {
            get
            {
                instance = (T)FindObjectOfType(typeof(T));

                if (instance == null)
                {
                    instance = new GameObject(name: typeof(T).ToString(), components: typeof(T)).GetComponent<T>();
                }

                return instance;
            }
        }
        #endregion
    }
}