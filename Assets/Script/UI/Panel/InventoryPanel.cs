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
        InventoryManager.Instance.CreateProp(slotGrid);
    }

    void OnCloseButtonClicked()
    {
        UIManager.Instance.ClosePanel(gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
