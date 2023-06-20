using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity1Week0619;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// オーディオを管理するクラス
/// </summary>
public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource bgmSource;

    [SerializeField]
    private SoundEffectElement soundEffectElementPrefab;

    public static AudioManager Instance { get; private set; }

    public static async UniTask SetupAsync()
    {
        Assert.IsNull(Instance);

        var prefab = await AssetLoader.LoadAsync<GameObject>("Assets/Unity1Week0619/Prefabs/AudioManager.prefab");
        Instance = Instantiate(prefab).GetComponent<AudioManager>();
        DontDestroyOnLoad(Instance);
    }

    public static void PlayBGM(AudioClip clip)
    {
        Instance.bgmSource.clip = clip;
        Instance.bgmSource.Play();
    }

    public static async UniTask PlaySEAsync(SoundEffectElement.AudioData data)
    {
        var element = Instantiate(Instance.soundEffectElementPrefab, Instance.transform);
        await element.PlayAsync(data);
        Destroy(element.gameObject);
    }

    public static void PlaySE(SoundEffectElement.AudioData data)
    {
        PlaySEAsync(data).Forget();
    }
}
