
////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2019 AsNet Co., Ltd.
// All Rights Reserved. These instructions, statements, computer
// programs, and/or related material (collectively, the "Source")
// contain unpublished information proprietary to AsNet Co., Ltd
// which is protected by US federal copyright law and by
// international treaties. This Source may NOT be disclosed to
// third parties, or be copied or duplicated, in whole or in
// part, without the written consent of AsNet Co., Ltd.
////////////////////////////////////////////////////////////////////////////////
using System.Collections;
using System;

/// Callbacks
public delegate void Callback();
public delegate void Callback<T0>(T0 arg);
public delegate void Callback<T0, T1>(T0 arg, T1 arg1);
public delegate void Callback<T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2);
public delegate void Callback<T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3);

public interface IEventData<Ttype>
{

    //----------------------------------------------------------------------
    #region Add Listener
    void AddListener(Ttype ev, Callback handler);
    void AddListener<T>(Ttype ev, Callback<T> handler);
    void AddListener<T0, T1>(Ttype ev, Callback<T0, T1> handler);
    void AddListener<T0, T1, T2>(Ttype ev, Callback<T0, T1, T2> handler);
    void RemoveListener(Ttype ev, Callback handler);
    void RemoveListener<T>(Ttype ev, Callback<T> handler);
    void RemoveListener<T0, T1>(Ttype ev, Callback<T0, T1> handler);
    void RemoveListener<T0, T1, T2>(Ttype ev, Callback<T0, T1, T2> handler);
    void Invoke(Ttype ev);
    void Invoke<T>(Ttype ev, T arg);
    void Invoke<T0, T1>(Ttype ev, T0 arg0, T1 arg1);
    void Invoke<T0, T1, T2>(Ttype ev, T0 arg0, T1 arg1, T2 arg2);
    void Invoke<T0, T1, T2, T3>(Ttype ev, T0 arg0, T1 arg1, T2 arg2, T3 arg3);
    #endregion Add Listener
}
