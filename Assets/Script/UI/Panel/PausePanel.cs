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
        //Bocchi:��ͣ��Ϸ
        OnPause();
        continueBtn.onClick.AddListener(OnContinue);
        backToMenuBtn.onClick.AddListener(OnBackToMenu);
    }

    private void OnDisable()
    {
        continueBtn.onClick.RemoveAllListeners();
        backToMenuBtn.onClick.RemoveAllListeners();
    }
    protected override void OnContinue()
    {
        base.OnContinue();
        UIManager.Instance.ClosePanel(gameObject.name);
    }

    private void OnBackToMenu()
    {
        OnContinue();
<<<<<<< HEAD
        InventoryManager.Instance.SaveInventoryData();
=======
>>>>>>> 2239575 (对Panel组件获取Player输入系统进行非空检查，并添加了部分Panel的按钮的功能)
        UIManager.Instance.OpenPanel("MenuPanel");
    }    
}
