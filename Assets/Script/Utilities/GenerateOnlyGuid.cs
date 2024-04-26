using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����ΨһGUID
public class GenerateOnlyGuid : MonoBehaviour
{
    public string ID;
    public bool Regenerate;
    private void OnValidate()
    {
        if (Regenerate)
        {
             ID = System.Guid.NewGuid().ToString();
        }
    }
}
