using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

// ����SO
[CreateAssetMenu(fileName = "NewSceneSO", menuName = "GameScene/GameSceneSO")]
public class GameSceneSO : ScriptableObject
{
    public AssetReference sceneReference;
    public Vector3 positionToGo;
}
