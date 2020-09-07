using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType
{
	RegisterAccount,
    SiginSucessWithName,
    SiginSucessWithouName,
    SiginFalse,
    CreateSucces,
    CreateFalse,
    LoadGame,
    UpdateMultiplyView,
    UpdateShoeView,
    UpdaeStarView,
    RollingStart,
    RollingEnd,
    InitOtherScore,
    UpdatePlayerScore,
    UpdatePlayerLife,
    UpdateOtherScore,
    UpdateOtherLife,
    CameraShake,
    CameraChange,
    ShowSettleMent,
    InPasue,
    RemoveScore,
}

public class EventCenter : Singleton<EventCenter> {

	public delegate void CallBack();
	public delegate void CallBack<T>(T t1);
    public delegate void CallBack<T1, T2>(T1 t1, T2 t2);

	Dictionary<EventType, Delegate> dicEvent = new Dictionary<EventType, Delegate>();


    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
    public void AddListen(EventType eventType,CallBack callBack)
    {
        OnAddListen(eventType, callBack);
        dicEvent[eventType] = (CallBack)dicEvent[eventType] + callBack;
    }

    public void AddListen<T>(EventType eventType, CallBack<T> callBack)
    {
        OnAddListen(eventType, callBack);
        dicEvent[eventType] = (CallBack<T>)dicEvent[eventType] + callBack;
    }

    public void AddListen<T1,T2>(EventType eventType, CallBack<T1,T2> callBack)
    {
        OnAddListen(eventType, callBack);
        dicEvent[eventType] = (CallBack<T1, T2>)dicEvent[eventType] + callBack;
    }

    void OnAddListen(EventType eventType, Delegate eventAdd)
    {
        if (!dicEvent.ContainsKey(eventType))
        {
            dicEvent.Add(eventType, null);
        }
        Delegate d = dicEvent[eventType];
        if (d != null && d.GetType() != eventAdd.GetType())
        {
            throw new Exception("类型不同");
        }
    }

    public void RemoveListen(EventType eventType, CallBack callBack)
    {
        OnRemoveListen(eventType, callBack);
        dicEvent[eventType] = (CallBack)dicEvent[eventType] - callBack;
    }

    public void RemoveListen<T>(EventType eventType, CallBack<T> callBack)
    {
        OnRemoveListen(eventType, callBack);
        dicEvent[eventType] = (CallBack<T>)dicEvent[eventType] - callBack;
    }

    public void RemoveListen<T1,T2>(EventType eventType, CallBack<T1,T2> callBack)
    {
        OnRemoveListen(eventType, callBack);
        dicEvent[eventType] = (CallBack<T1, T2>)dicEvent[eventType] - callBack;
    }

    void OnRemoveListen(EventType eventType, Delegate eventAdd)
    {
        if (dicEvent.ContainsKey(eventType))
        {
            Delegate d = dicEvent[eventType];
            if (d == null)
            {
                throw new Exception("没有对应的事件");
            }
            else if (d.GetType() != eventAdd.GetType())
            {
                throw new Exception("类型不同");
            }
        }
        else
        {
            throw new Exception("没有对应的事件");
        }
    }
    
    public void PostEvent(EventType eventType)
    {
        Delegate d;
        if (dicEvent.TryGetValue(eventType, out d))
        {
            CallBack callBack = (CallBack)d;
            if (callBack != null)
            {
                callBack();
            }
            else
            {
                throw new Exception("CallBack为空");
            }
        }
    }

    public void PostEvent<T>(EventType eventType, T t1)
    {
        Delegate d;
        if (dicEvent.TryGetValue(eventType, out d))
        {
            CallBack<T> callBack = (CallBack<T>)d;
            if (callBack != null)
            {
                callBack(t1);
            }
            else
            {
                throw new Exception("CallBack为空");
            }
        }
    }

    public void PostEvent<T1,T2>(EventType eventType, T1 t1,T2 t2)
    {
        Delegate d;
        if (dicEvent.TryGetValue(eventType, out d))
        {
            CallBack<T1,T2> callBack = (CallBack<T1,T2>)d;
            if (callBack != null)
            {
                callBack(t1,t2);
            }
            else
            {
                throw new Exception("CallBack为空");
            }
        }
    }

}
