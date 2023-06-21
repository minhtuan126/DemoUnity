using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : SingletonPersistent<EventManager>
{
    private static EventData _eventData;

    /// <summary>
    /// On awake, we check if there's already a copy of the object in the scene. If there's one, we destroy it.
    /// </summary>
    protected override void Awake()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            return;
        }
#endif
        base.Awake();
        if (this == _instance)
        {
            _eventData = new EventData();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _eventData = new EventData();
    }

    /// static
    //----------------------------------------------------------------------
    #region Add Listener
    /// Adds listener without parameters.
    public static void AddListener(Enum ev, Callback handler)
    {
        _eventData?.AddListenerToDict(ev, handler);
    }

    /// Adds listener with a single parameter.
    public static void AddListener<T0>(Enum ev, Callback<T0> handler)
    {
        _eventData?.AddListenerToDict<T0>(ev, handler);
    }

    /// Adds listener with two parameters.
    public static void AddListener<T0, T1>(Enum ev, Callback<T0, T1> handler)
    {
        _eventData?.AddListenerToDict<T0, T1>(ev, handler);
    }

    /// Adds listener with three parameters.
    public static void AddListener<T0, T1, T2>(Enum ev, Callback<T0, T1, T2> handler)
    {
        _eventData?.AddListenerToDict<T0, T1, T2>(ev, handler);
    }

    /// Adds listener with three parameters.
    public static void AddListener<T0, T1, T2, T3>(Enum ev, Callback<T0, T1, T2, T3> handler)
    {
        _eventData?.AddListenerToDict<T0, T1, T2, T3>(ev, handler);
    }

    #endregion // Add Listener
    ////////////////////////////////////////////////////////////////////////


    //----------------------------------------------------------------------
    #region Remove Listener
    /// Removes the listener has no parameters.
    public static void RemoveListener(Enum ev, Callback handler)
    {
        _eventData?.RemoveListenerFromDict(ev, handler);
    }

    /// Removes the listener has single parameter.
    public static void RemoveListener<T0>(Enum ev, Callback<T0> handler)
    {
        _eventData?.RemoveListenerFromDict<T0>(ev, handler);
    }

    /// Removes the listener has two parameters.
    public static void RemoveListener<T0, T1>(Enum ev, Callback<T0, T1> handler)
    {
        _eventData?.RemoveListenerFromDict<T0, T1>(ev, handler);
    }

    /// Removes the listener has three parameters.
    public static void RemoveListener<T0, T1, T2>(Enum ev, Callback<T0, T1, T2> handler)
    {
        _eventData?.RemoveListenerFromDict<T0, T1, T2>(ev, handler);
    }

    /// Removes the listener has three parameters.
    public static void RemoveListener<T0, T1, T2, T3>(Enum ev, Callback<T0, T1, T2, T3> handler)
    {
        _eventData?.RemoveListenerFromDict<T0, T1, T2, T3>(ev, handler);
    }

    #endregion // Remove Listener
    ////////////////////////////////////////////////////////////////////////


    //----------------------------------------------------------------------
    #region static Invokes
    public static void Invoke(Enum ev)
    {
        _eventData?.InvokeEvent(ev);
    }

    public static void Invoke<T0>(Enum ev, T0 arg)
    {
        _eventData?.InvokeEvent<T0>(ev, arg);
    }

    public static void Invoke<T0, T1>(Enum ev, T0 arg0, T1 arg1)
    {
        _eventData?.InvokeEvent<T0, T1>(ev, arg0, arg1);
    }

    public static void Invoke<T0, T1, T2>(Enum ev, T0 arg0, T1 arg1, T2 arg2)
    {
        _eventData?.InvokeEvent<T0, T1, T2>(ev, arg0, arg1, arg2);
    }

    public static void Invoke<T0, T1, T2, T3>(Enum ev, T0 arg0, T1 arg1, T2 arg2, T3 arg3)
    {
        _eventData?.InvokeEvent<T0, T1, T2, T3>(ev, arg0, arg1, arg2, arg3);
    }

    #endregion static  methods
}

public class EventData
{
    public Dictionary<Enum, Delegate> _eventsTable0 = new Dictionary<Enum, Delegate>();
    public Dictionary<Enum, Delegate> _eventsTable1 = new Dictionary<Enum, Delegate>();
    public Dictionary<Enum, Delegate> _eventsTable2 = new Dictionary<Enum, Delegate>();
    public Dictionary<Enum, Delegate> _eventsTable3 = new Dictionary<Enum, Delegate>();
    public Dictionary<Enum, Delegate> _eventsTable4 = new Dictionary<Enum, Delegate>();

    //----------------------------------------------------------------------
    #region Verify


    private void OnListenerAdding<T>(T ev, Dictionary<Enum, Delegate> eventsTable, Delegate handler) where T : Enum
    {
        // Prepare for the event type in table.
        if (!eventsTable.ContainsKey(ev))
            eventsTable.Add(ev, null);
    }

    private bool CheckListenerToRemoving<T>(T ev, Dictionary<Enum, Delegate> eventsTable, Delegate handler) where T : Enum
    {
        if (!eventsTable.ContainsKey(ev)) return false;

        Delegate d = eventsTable[ev];
        if (d == null)
        {
            Debug.LogAssertion(string.Format("E65123418964: {0} Attemping to remove a null listener.", ev));
            return false;
        }

        if (d.GetType() != handler.GetType())
        {
            Debug.LogAssertion(string.Format("E45112677365: {0} Attemping to remove a listener is not exist in system.", ev));
            return false;
        }
        return true;
    }

    private void OnListenerRemoved<T>(T ev, Dictionary<Enum, Delegate> eventsTable) where T : Enum
    {
        if (eventsTable[ev] != null) return;
        eventsTable.Remove(ev);
    }

    #endregion // Verify
    ////////////////////////////////////////////////////////////////////////


    //----------------------------------------------------------------------
    #region Add Listener
    /// Adds listener without parameters.
    public void AddListenerToDict(Enum ev, Callback handler)
    {
        OnListenerAdding(ev, _eventsTable0, handler);
        _eventsTable0[ev] = (Callback)_eventsTable0[ev] - handler;
        _eventsTable0[ev] = (Callback)_eventsTable0[ev] + handler;
    }

    /// Adds listener with a single parameter.
    public void AddListenerToDict<T0>(Enum ev, Callback<T0> handler)
    {
        OnListenerAdding(ev, _eventsTable1, handler);
        _eventsTable1[ev] = (Callback<T0>)_eventsTable1[ev] - handler;
        _eventsTable1[ev] = (Callback<T0>)_eventsTable1[ev] + handler;
    }

    /// Adds listener with two parameters.
    public void AddListenerToDict<T0, T1>(Enum ev, Callback<T0, T1> handler)
    {
        OnListenerAdding(ev, _eventsTable2, handler);
        _eventsTable2[ev] = (Callback<T0, T1>)_eventsTable2[ev] - handler;
        _eventsTable2[ev] = (Callback<T0, T1>)_eventsTable2[ev] + handler;
    }

    /// Adds listener with three parameters.
    public void AddListenerToDict<T0, T1, T2>(Enum ev, Callback<T0, T1, T2> handler)
    {
        OnListenerAdding(ev, _eventsTable3, handler);
        _eventsTable3[ev] = (Callback<T0, T1, T2>)_eventsTable3[ev] - handler;
        _eventsTable3[ev] = (Callback<T0, T1, T2>)_eventsTable3[ev] + handler;
    }

    /// Adds listener with three parameters.
    public void AddListenerToDict<T0, T1, T2, T3>(Enum ev, Callback<T0, T1, T2, T3> handler)
    {
        OnListenerAdding(ev, _eventsTable4, handler);
        _eventsTable4[ev] = (Callback<T0, T1, T2, T3>)_eventsTable4[ev] - handler;
        _eventsTable4[ev] = (Callback<T0, T1, T2, T3>)_eventsTable4[ev] + handler;
    }

    #endregion // Add Listener
    ////////////////////////////////////////////////////////////////////////


    //----------------------------------------------------------------------
    #region Remove Listener
    /// Removes the listener has no parameters.
    public void RemoveListenerFromDict(Enum ev, Callback handler)
    {
        if (CheckListenerToRemoving(ev, _eventsTable0, handler))
        {
            _eventsTable0[ev] = (Callback)_eventsTable0[ev] - handler;
            OnListenerRemoved(ev, _eventsTable0);
        }
    }

    /// Removes the listener has single parameter.
    public void RemoveListenerFromDict<T0>(Enum ev, Callback<T0> handler)
    {
        if (CheckListenerToRemoving(ev, _eventsTable1, handler))
        {
            _eventsTable1[ev] = (Callback<T0>)_eventsTable1[ev] - handler;
            OnListenerRemoved(ev, _eventsTable1);
        }
    }

    /// Removes the listener has two parameters.
    public void RemoveListenerFromDict<T0, T1>(Enum ev, Callback<T0, T1> handler)
    {
        if (CheckListenerToRemoving(ev, _eventsTable2, handler))
        {
            _eventsTable2[ev] = (Callback<T0, T1>)_eventsTable2[ev] - handler;
            OnListenerRemoved(ev, _eventsTable2);
        }
    }

    /// Removes the listener has three parameters.
    public void RemoveListenerFromDict<T0, T1, T2>(Enum ev, Callback<T0, T1, T2> handler)
    {
        if (CheckListenerToRemoving(ev, _eventsTable3, handler))
        {
            _eventsTable3[ev] = (Callback<T0, T1, T2>)_eventsTable3[ev] - handler;
            OnListenerRemoved(ev, _eventsTable3);
        }
    }
    /// Removes the listener has three parameters.
    public void RemoveListenerFromDict<T0, T1, T2, T3>(Enum ev, Callback<T0, T1, T2, T3> handler)
    {
        if (CheckListenerToRemoving(ev, _eventsTable4, handler))
        {
            _eventsTable4[ev] = (Callback<T0, T1, T2, T3>)_eventsTable4[ev] - handler;
            OnListenerRemoved(ev, _eventsTable4);
        }
    }
    #endregion // Remove Listener
    ////////////////////////////////////////////////////////////////////////


    //----------------------------------------------------------------------
    #region Invokes
    public void InvokeEvent(Enum ev)
    {
        Delegate func;
        if (_eventsTable0.TryGetValue(ev, out func))
        {
            Callback callback = func as Callback;
            if (callback != null)
            {
                callback();
            }
            else
            {
                Debug.Log("E5201984312: Callback function is null.");
            }
        }
    }

    public void InvokeEvent<T0>(Enum ev, T0 arg)
    {
        Delegate func;
        if (_eventsTable1.TryGetValue(ev, out func))
        {
            Callback<T0> callback = func as Callback<T0>;
            if (callback != null)
            {
                callback(arg);
            }
            else
            {
                Debug.Log("E2210983187: Callback function is null.");
            }
        }
    }

    public void InvokeEvent<T0, T1>(Enum ev, T0 arg0, T1 arg1)
    {
        Delegate func;
        if (_eventsTable2.TryGetValue(ev, out func))
        {
            Callback<T0, T1> callback = func as Callback<T0, T1>;
            if (callback != null)
            {
                callback(arg0, arg1);
            }
            else
            {
                Debug.Log("E45554695439: Callback function is null.");
            }
        }
    }

    public void InvokeEvent<T0, T1, T2>(Enum ev, T0 arg0, T1 arg1, T2 arg2)
    {
        Delegate func;
        if (_eventsTable3.TryGetValue(ev, out func))
        {
            Callback<T0, T1, T2> callback = func as Callback<T0, T1, T2>;
            if (callback != null)
            {
                callback(arg0, arg1, arg2);
            }
            else
            {
                Debug.Log("E4310948391: Callback function is null.");
            }
        }
    }

    public void InvokeEvent<T0, T1, T2, T3>(Enum ev, T0 arg0, T1 arg1, T2 arg2, T3 arg3)
    {
        Delegate func;
        if (_eventsTable4.TryGetValue(ev, out func))
        {
            Callback<T0, T1, T2, T3> callback = func as Callback<T0, T1, T2, T3>;
            if (callback != null)
            {
                callback(arg0, arg1, arg2, arg3);
            }
            else
            {
                Debug.Log("E4310948391: Callback function is null.");
            }
        }
    }
    #endregion Invoke  methods

}
