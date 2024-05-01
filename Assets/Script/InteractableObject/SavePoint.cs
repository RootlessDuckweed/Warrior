using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour, IInteractable
{
    public GameObject showLightSign;
    public bool isOpen;
   /* public static SavePoint currentSavePoint;
    public string ID;
    public class NeedToConvertJsonData
    {
        public NeedToConvertJsonData(string ID,bool isOpen)
        {
            isOpened = isOpen;
            this.ID = ID;
        }

        public bool isOpened;
        public string ID;
    }*/

   
    private void Awake()
    {
        showLightSign = transform.GetChild(0).gameObject;
        showLightSign.SetActive(false);
    } 
    private void Start()
    {
       /* ID = GetComponent<GenerateOnlyGuid>().ID;*/
    }
    public bool RepeatInteraction()
    {
        return false;
    }

    public void TriggerAction()
    {
        isOpen = true;
        showLightSign.SetActive(isOpen);
        /*if (currentSavePoint != null)
        {
            currentSavePoint.showLightSign.SetActive(false);
            currentSavePoint.isOpen = false;
            currentSavePoint.gameObject.tag = "Interactable";
        }
        currentSavePoint = this;*/
        SceneLoaderManager.Instance.SaveScenePoint(transform.position);
        SaveInteractableObserver.SaveInteractableToJson();
        //Bocchi:���ñ��汳�����ݵķ���
        InventoryManager.Instance.SaveInventoryData();
    }

   /* public void DeserializeFromJsonData(string json) 
    { 
        var obj = JsonConvert.DeserializeObject<NeedToConvertJsonData>(json);
        isOpen = obj.isOpened;
        if(isOpen == true)
        {
            showLightSign?.SetActive(isOpen);
            gameObject.tag = "Untagged";
        }
        
    } // ��json�ļ��з����б������Ϣ ��ִ����ز���

   public string GetNeedToCovertJsonData() 
    {
        return JsonConvert.SerializeObject(new NeedToConvertJsonData(ID,isOpen));
    } // ��ȡ��Ҫ�����json���ݴӱ�����
*/
}
