using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayerRespawnEventSO",menuName = "Event/PlayerRespawnEventSO")]
public class PlayerRespawnEventSO : ScriptableObject
{
    public UnityEvent OnPlayerDeadEvent;
    public void RaisedEvent()
    {
        OnPlayerDeadEvent?.Invoke();
    }
}
