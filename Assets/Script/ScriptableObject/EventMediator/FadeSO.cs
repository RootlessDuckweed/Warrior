using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

[CreateAssetMenu(fileName ="FadeSO",menuName = "Event/FadeSO")]
public class FadeSO : ScriptableObject
{
    public UnityAction< float,bool> OnRaisedEvent;
   
    
    public void RaisedEvent(float dura,bool isFadeIn)
    {
        OnRaisedEvent?.Invoke(dura, isFadeIn );
    }
}
