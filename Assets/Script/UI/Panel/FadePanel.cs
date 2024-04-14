using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadePanel : MonoBehaviour
{
    public Image fadeImage;
    public FadeSO fadeEvent;

    private void Awake()
    {
        fadeImage = GetComponent<Image>();
    }
    private void OnEnable()
    {
        fadeEvent.OnRaisedEvent += OnFadeEvent;
    }

    private void OnDisable()
    {
        fadeEvent.OnRaisedEvent -= OnFadeEvent;
    }

    public void OnFadeEvent(float dura,bool isFadeIn)
    {
        if (isFadeIn)
        {
            
            fadeImage.DOFade(1, dura);
        }
        else
        {
          
            fadeImage.DOFade(0, dura);
        }
       
    }
}
