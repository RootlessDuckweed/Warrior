using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Channels;
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
        newGameBtn = transform.Find("NewGameButton").GetComponent<Button>();
        continueBtn = transform.Find("ContinueButton").GetComponent<Button>();
        quitBtn = transform.Find("QuitButton").GetComponent<Button>();
    }


}
