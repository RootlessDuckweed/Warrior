using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : BasePanel
{
    Button continueBtn;
    Button backToMenuBtn;
    protected override void Awake()
    {
        base.Awake();
        continueBtn=GameObject.Find("ContinueButton").GetComponent<Button>();
        backToMenuBtn=GameObject.Find("BackToMenuButton").GetComponent<Button>();
    }
}
