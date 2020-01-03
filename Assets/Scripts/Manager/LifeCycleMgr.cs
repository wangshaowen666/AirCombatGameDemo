﻿using System;
using System.Collections.Generic;

public class LifeCycleMgr : MonoSingleton<LifeCycleMgr>, IInit
{
    void IInit.Init()
    {
        var config = new LifeCycleAddConfig();
        config.Init();
        InitObject(config);

        LifeCycleConfig.LifeCycleFuns[LifeName.INIT]();
    }

    private void InitObject(LifeCycleAddConfig config)
    {
        foreach (var o in config.Objects)
        foreach (var cycle in LifeCycleConfig.LifeCycles)
            if (cycle.Value.Add(o))
                break;
    }

    public void Add(LifeName name, object o)
    {
        LifeCycleConfig.LifeCycles[name].Add(o);
    }

    public void Remove(LifeName name, object o)
    {
        LifeCycleConfig.LifeCycles[name].Remove(o);
    }

    public void RemoveAll(object o)
    {
        foreach (var cycle in LifeCycleConfig.LifeCycles) cycle.Value.Remove(o);
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameStateModel.Single.Pause)
            return;
        LifeCycleConfig.LifeCycleFuns[LifeName.UPDATE]();
    }
}

public interface ILifeCycle
{
    bool Add(object o);
    void Remove(object o);
    void Execute<T>(Action<T> execute);
}

public class LifeCycle<T> : ILifeCycle
{
    private readonly HashSet<object> _objects = new HashSet<object>();
    private readonly HashSet<object> _removeObjects = new HashSet<object>();

    public bool Add(object o)
    {
        if (o is T)
        {
            _objects.Add(o);
            return true;
        }

        return false;
    }

    public void Remove(object o)
    {
        _removeObjects.Add(o);
    }

    public void Execute<T1>(Action<T1> execute)
    {
        foreach (var o in _objects)
            if (o == null)
                _removeObjects.Add(o);
            else
                try
                {
                    execute((T1) o);
                }
                catch (Exception e)
                {
                }

        foreach (var o in _removeObjects) _objects.Remove(o);

        _removeObjects.Clear();
    }
}