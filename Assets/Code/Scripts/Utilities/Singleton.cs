using UnityEngine;

namespace Utilities
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    var instance = FindObjectOfType<T>();

                    if (instance == null)
                    {
                        var obj = new GameObject
                        {
                            name = typeof(T).Name,
                        };

                        _instance = obj.AddComponent<T>();
                    }
                    else
                    {
                        _instance = instance;
                    }
                }

                return _instance;
            }
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }
    }
}