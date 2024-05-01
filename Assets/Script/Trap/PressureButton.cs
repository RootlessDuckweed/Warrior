using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressureButton : MonoBehaviour
{
    private Animator anim;
    public bool isPress;
    public UnityEvent onPressedEvent;
    public UnityEvent onReleasedEvent;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Player":
                isPress = true;
                anim.SetBool("isPress", isPress);
                onPressedEvent?.Invoke();
                break;
            case "PlayerDead":
                isPress = true;
                anim.SetBool("isPress", isPress);
                onPressedEvent?.Invoke();
                break;
            case "Box":
                isPress = true;
                anim.SetBool("isPress", isPress);
                onPressedEvent?.Invoke();
                break;
            case "ReflectBox":
                isPress = true;
                anim.SetBool("isPress", isPress);
                onPressedEvent?.Invoke();
                break;
           
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {

        switch (other.tag)
        {
            case "Player":
                isPress = false;
                anim.SetBool("isPress", isPress);
                onReleasedEvent?.Invoke();
                break;
            case "PlayerDead":
                isPress = false;
                anim.SetBool("isPress", isPress);
                onReleasedEvent?.Invoke();
                break;
            case "Box":
                isPress = false;
                anim.SetBool("isPress", isPress);
                onReleasedEvent?.Invoke();
                break;
            case "ReflectBox":
                isPress = false;
                anim.SetBool("isPress", isPress);
                onReleasedEvent?.Invoke();
                break;
            
        }

    }

    #region Unity Animation Event
    public void PlayButtonDownFX()
    {
        AudioManager.Instance.PlayFX(AudioPathGlobals.PressureBtn, 0.2f);
    }
    #endregion

}
