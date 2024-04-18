using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventoryPanel : BasePanel
{
    Button closeButton;
    GameObject slotGrid;
    // Start is called before the first frame update
    void Start()
    {
        closeButton = transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(OnCloseButtonClicked);
        slotGrid= transform.Find("GridPanel").gameObject;
        //Bocchi:‘›Õ£”Œœ∑
        InventoryManager.Instance.CreateProp(slotGrid);
    }
    private void OnEnable()
    {
        base.OnPause();
    }

    void OnCloseButtonClicked()
    {
        OnContinue();
    }

    protected override void OnContinue()
    {
        base.OnContinue();
        UIManager.Instance.ClosePanel(gameObject.name);
    }
}
