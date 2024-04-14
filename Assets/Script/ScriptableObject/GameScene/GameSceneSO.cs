using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

// ³¡¾°SO
[CreateAssetMenu(fileName = "NewSceneSO", menuName = "GameScene/GameSceneSO")]
public class GameSceneSO : ScriptableObject
{
    public AssetReference sceneReference;
    public Vector3 positionToGo;
}
