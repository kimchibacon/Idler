//=============================================================================
// Singleton.cs
//
// Singleton base class with generic type checking. 
//=============================================================================

using UnityEngine;

namespace Idler.Core
{
    //=========================================================================
    // Singleton<T>
    //=========================================================================
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        [HideInInspector] public static T Instance { get; private set; }
        [HideInInspector] public static bool IsInitialized => Instance != null;

        //=====================================================================
        //=====================================================================
        protected virtual void Awake()
        {
            if (IsInitialized)
            {
                Debug.LogError("[" + typeof(T) + "] Trying to instantiate a second instance of a singleton class.");
            }
            else
            {
                Instance = (T) this;
            }
        }

        //=====================================================================
        //=====================================================================
        protected virtual void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }
    }
}
