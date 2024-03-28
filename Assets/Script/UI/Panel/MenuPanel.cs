using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : BasePanel
{
    Button newGameBtn;
    Button continueBtn;
    Button quitBtn;
    protected override void Awake()
    {
        base.Awake();
        newGameBtn = GameObject.Find("NewGameButton").GetComponent<Button>();
        continueBtn = GameObject.Find("ContinueButton").GetComponent<Button>();
        quitBtn = GameObject.Find("QuitButton").GetComponent<Button>();
    }


}
