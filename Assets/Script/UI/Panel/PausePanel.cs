using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : BasePanel
{
    Button continueBtn;
    Button backToMenuBtn;
    [SerializeField]Button RemakeBtn;
    protected override void Awake()
    {
        base.Awake();
        continueBtn=GameObject.Find("ContinueButton").GetComponent<Button>();
        backToMenuBtn=GameObject.Find("BackToMenuButton").GetComponent<Button>();
        RemakeBtn = transform.Find("Remake").GetComponent<Button>();
    }
    private void OnEnable()
    {
        //Bocchi:‘›Õ£”Œœ∑
        OnPause();
        continueBtn.onClick.AddListener(OnContinue);
        backToMenuBtn.onClick.AddListener(OnBackToMenu);
        RemakeBtn.onClick.AddListener(Remake);
    }

    private void OnDisable()
    {
        continueBtn.onClick.RemoveAllListeners();
        backToMenuBtn.onClick.RemoveAllListeners();
        RemakeBtn.onClick.RemoveAllListeners();
    }
    protected override void OnContinue()
    {
        base.OnContinue();
        UIManager.Instance.ClosePanel(gameObject.name);
    }

    private void OnBackToMenu()
    {
        OnContinue();
        InventoryManager.Instance.SaveInventoryData();
        UIManager.Instance.OpenPanel("MenuPanel");
    }    

    private void Remake()
    {
        OnContinue();
        SceneLoaderManager.Instance.ReLoadThisScene();
    }
}
