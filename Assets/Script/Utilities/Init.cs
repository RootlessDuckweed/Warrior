using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Init : MonoBehaviour
{
    public GameSceneSO UIScene;
    private void Awake()
    {
        Addressables.LoadSceneAsync(UIScene.sceneReference);
    }
}
