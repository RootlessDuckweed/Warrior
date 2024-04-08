using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    public PropSO propSO;
    public Image slotImage;
    public TMP_Text slotNum;
    public Button button;
    public void OnButtonClicked()
    {
        InventoryManager.DisplayDescription(propSO);
    }
}
