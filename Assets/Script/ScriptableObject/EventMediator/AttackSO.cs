using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// ¹¥»÷ÖÐ½éÀà
[CreateAssetMenu(menuName = "Data/AttackSO")]
public class AttackSO : ScriptableObject
{
    private bool isCritical;

    public void SetCritical(bool isCritic)
    {
        isCritical = isCritic;
    }
    public bool GetCritical()
    {
       return isCritical;
    }

}
