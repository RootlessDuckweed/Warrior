using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AudioManager : Singleton<AudioManager>
{ 
    public AudioSource BGM; //������Ч��� ����BGM
    public AudioSource FX;  //������Ч��� ������Ч

    protected override void Awake()
    {
        base.Awake();
        PlayBGM(AudioPathGlobals.BGM,0.1f);
    }

    //�첽���� BGM��Ч
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
    
    //�첽���� ��ͨ��Ч
    IEnumerator LoadFXAudioClip(string path, float volume)
    {
        var handle = Addressables.LoadAssetAsync<AudioClip>(path);
        yield return handle;
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            _PlayFX(handle.Result, volume);
        }
    }


    // �ڲ����õĽӿ� 
    void _PlayBGM(float volume)
    {
        BGM.Stop();
        BGM.volume = volume;
        BGM.Play();
       
    }
    // �ڲ����õĽӿ� 
    void _PlayFX(AudioClip FX_Clip,float volume)
    {
        FX.clip = FX_Clip;
        FX.PlayOneShot(FX_Clip,volume);
        
    }

    //����������õĽӿ� ����BGM
    public void PlayBGM(string path, float volume)
    {
        StartCoroutine(LoadBGMAudioClip(path, volume));
    }

    // ����������õĽӿ� ������Ч
    public void PlayFX(string path, float volume)
    {
        StartCoroutine(LoadFXAudioClip(path, volume));
    }

   
}
