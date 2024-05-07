using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "StartToLoadSceneEventSO", menuName = "Event/StartToLoadSceneEventSO")]
public class StartToLoadSceneEventSO : ScriptableObject
{
    public UnityEvent OnStartToLoadSceneEvent;
    public void RaisedEvent()
    {
        OnStartToLoadSceneEvent?.Invoke();
    }
}
