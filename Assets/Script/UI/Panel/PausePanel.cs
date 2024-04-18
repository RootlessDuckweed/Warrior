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
    private void OnEnable()
    {
        //Bocchi:‘›Õ£”Œœ∑
        OnPause();
        continueBtn.onClick.AddListener(OnContinue);
    }

    private void OnDisable()
    {
        continueBtn.onClick.RemoveAllListeners();
    }
    protected override void OnContinue()
    {
        base.OnContinue();
        UIManager.Instance.ClosePanel(gameObject.name);
    }
}
