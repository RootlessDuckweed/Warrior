using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AudioManager : Singleton<AudioManager>
{ 
    public AudioSource BGM; //自身音效组件 播放BGM
    public AudioSource FX;  //自身音效组件 播放音效

    protected override void Awake()
    {
        base.Awake();
        PlayBGM(AudioPathGlobals.BGM,0.1f);
    }

    //异步加载 BGM音效
    IEnumerator LoadBGMAudioClip(string path,float volume)
    {
        var handle = Addressables.LoadAssetAsync<AudioClip>(path);
        yield return handle;
        if(handle.Status == AsyncOperationStatus.Succeeded)
        {
            BGM.clip = handle.Result;
            _PlayBGM(volume);
        }
    }
    
    //异步加载 普通音效
    IEnumerator LoadFXAudioClip(string path, float volume)
    {
        var handle = Addressables.LoadAssetAsync<AudioClip>(path);
        yield return handle;
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            _PlayFX(handle.Result, volume);
        }
    }


    // 内部调用的接口 
    void _PlayBGM(float volume)
    {
        BGM.Stop();
        BGM.volume = volume;
        BGM.Play();
       
    }
    // 内部调用的接口 
    void _PlayFX(AudioClip FX_Clip,float volume)
    {
        FX.clip = FX_Clip;
        FX.PlayOneShot(FX_Clip,volume);
        
    }

    //公开向外调用的接口 播放BGM
    public void PlayBGM(string path, float volume)
    {
        StartCoroutine(LoadBGMAudioClip(path, volume));
    }

    // 公开向外调用的接口 播放音效
    public void PlayFX(string path, float volume)
    {
        StartCoroutine(LoadFXAudioClip(path, volume));
    }

   
}
