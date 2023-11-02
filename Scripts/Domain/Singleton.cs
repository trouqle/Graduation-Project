using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Herb.Domain
{

    public abstract class Singleton<T> : MonoBehaviour where T : class
    {
        static T instance;
        public static T Instance => instance;
        protected virtual void Awake()
        {
            if (instance == null) instance = this as T;
            else
            {

                Debug.Log("Found another singleton:" + instance.ToString());
                Destroy(gameObject);
            }
        }
        protected virtual void OnDestroy()
        {
            if(this as T == instance) instance = null;
        }
    }

    public abstract class SingletonPersistent<T> : MonoBehaviour where T : class
    {
        static T instance;
        public static T Instance => instance;
        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else Destroy(gameObject);

        }
        protected virtual void OnDestroy()
        {
            if (this as T == instance) instance = null;
        }
    }


}