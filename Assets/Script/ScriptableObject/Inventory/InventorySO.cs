using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����
[CreateAssetMenu(menuName = "Inventory/InventorySO")]
public class InventorySO : ScriptableObject
{
   public Dictionary<string,PropStack> propsPakage;
}
