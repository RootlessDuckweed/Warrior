using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class SlotDescriptionPanel : BasePanel
{
    public PropSO propSO;
    public TMP_Text titleText;
    public TMP_Text descriptionText;
    Button useButton;
    Button cancelButton;
    // Start is called before the first frame update
    void Start()
    {
        useButton=transform.Find("UseButton").GetComponent<Button>();
        cancelButton=transform.Find("CancelButton").GetComponent<Button>();
        if (propSO.useable)
        {
            useButton.interactable = true;
            useButton.onClick.AddListener(OnUseButtonClicked);
        }
        else
        {
            useButton.interactable=false;
        }
        cancelButton.onClick.AddListener(OnCancelButtonClicked);
    } 

    private void OnDisable()
    {
        useButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();
    }

    //Bocchi:���ʹ�ð�ť���õ��¼�
    void OnUseButtonClicked()
    {
        propSO.UseProp();
        InventoryManager.Instance.RemoveProp(propSO); 
        InventoryManager.Instance.RefreshProp();
        UIManager.Instance.ClosePanel(gameObject.name);
        
    }

    //Bocchi:���ȡ����ť���õ��¼�
    void OnCancelButtonClicked()
    {
        UIManager.Instance.ClosePanel(gameObject.name);
    }
}
