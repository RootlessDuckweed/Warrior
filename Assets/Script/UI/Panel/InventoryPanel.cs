using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventoryPanel : BasePanel
{
    Button closeButton;
    // Start is called before the first frame update
    void Start()
    {
        closeButton = transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(OnCloseButtonClicked);
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
