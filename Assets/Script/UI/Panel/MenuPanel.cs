using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Channels;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : BasePanel
{
    Button newGameBtn;
    Button continueBtn;
    Button quitBtn;
    protected override void Awake()
    {
        base.Awake();
        newGameBtn = transform.Find("NewGameButton").GetComponent<Button>();
        continueBtn = transform.Find("ContinueButton").GetComponent<Button>();
        quitBtn = transform.Find("QuitButton").GetComponent<Button>();

        //RootlessDuckweed : ����¼�����
        newGameBtn.onClick.AddListener(SceneLoaderManager.Instance.NewGame);
        continueBtn.onClick.AddListener(SceneLoaderManager.Instance.ContinueGame);
        //Bocchi:��Ӽ��غα��汳�����ݵ��¼�����
        continueBtn.onClick.AddListener(InventoryManager.Instance.LoadInventoryData);
        quitBtn.onClick.AddListener(OnQuit);
    }

    private void OnDisable()
    {
        newGameBtn.onClick.RemoveAllListeners();
        continueBtn.onClick.RemoveAllListeners();
        quitBtn.onClick.RemoveAllListeners();
    }

    private void OnQuit()
    {
        Application.Quit();
    }


}
