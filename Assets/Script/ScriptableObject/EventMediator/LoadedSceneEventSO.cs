using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "LoadedSceneEventSO", menuName = "Event/LoadedSceneSO")]
public class LoadedSceneEventSO : ScriptableObject
{
    public UnityEvent OnLoadedSceneEvent;
    public void RaisedEvent()
    {
        OnLoadedSceneEvent?.Invoke();
    }
}
