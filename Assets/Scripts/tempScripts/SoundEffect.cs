using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    // 单例实例
    public static SoundEffect Instance { get; private set; }

    // 音乐播放器
    private AudioSource audioSource;

    // 当前播放的音乐剪辑
    private AudioClip currentClip;

    //放置音效
    public AudioClip typeSound;
    public AudioClip shakeSound;

    private void Awake()
    {
        // 确保只有一个实例
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 在场景切换时保留
        }
        else
        {
            Destroy(gameObject); // 如果已经存在一个实例，则销毁新创建的对象
            return;
        }

        // 添加 AudioSource 组件
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    /// <summary>
    /// 播放指定的 BGM
    /// </summary>
    /// <param name="clip">要播放的音乐剪辑</param>
    public void PlaySound(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("尝试播放的 BGM 为 null");
            return;
        }

        // 如果是当前已经在播放的音乐，则不重新播放
        if (currentClip == clip)
        {
            Debug.Log("BGM 已经在播放：" + clip.name);
            return;
        }

        // 切换音乐
        currentClip = clip;
        audioSource.clip = currentClip;
        audioSource.Play();
        Debug.Log("开始播放 BGM：" + clip.name);
    }

    /// <summary>
    /// 停止播放 BGM
    /// </summary>
    public void StopSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
            Debug.Log("BGM 已停止");
        }
    }

    /// <summary>
    /// 暂停当前播放的 BGM
    /// </summary>
    public void PauseSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
            Debug.Log("BGM 已暂停");
        }
    }

    /// <summary>
    /// 恢复播放暂停的 BGM
    /// </summary>
    public void ResumeBGM()
    {
        if (!audioSource.isPlaying && currentClip != null)
        {
            audioSource.Play();
            Debug.Log("BGM 已恢复播放：" + currentClip.name);
        }
    }

    /// <summary>
    /// 调整音量
    /// </summary>
    /// <param name="volume">音量大小（0.0 ~ 1.0）</param>
    public void SetVolume(float volume)
    {
        audioSource.volume = Mathf.Clamp01(volume);
        Debug.Log("BGM 音量已设置为：" + audioSource.volume);
    }

    public void playTypeSound()
    {
        audioSource.loop = true; // 设置循环播放
        PlaySound(typeSound);
    }

    public void playShakeSound()
    {
        audioSource.loop = false;
        PlaySound(shakeSound);
    }
}
