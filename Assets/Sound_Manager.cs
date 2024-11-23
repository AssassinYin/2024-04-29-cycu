using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("Audio Clips")]
    public AudioClip playerHurtSound;
    public AudioClip jumpSound; // 跳躍音效
    public AudioClip attackSound; // 攻擊音效
    public AudioClip backgroundMusic; // 背景音樂
    public AudioClip dashSound;    // 衝刺音效
    public AudioClip runSound;  // 跑步音效

    public AudioClip monster1HurtSound;


    private AudioSource audioSource;
    private AudioSource loopingAudioSource; // 用於循環播放音效（例如跑步）


    private void Awake()
    {
        // Singleton 設定
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // 初始化 AudioSource
        audioSource = GetComponent<AudioSource>();
        loopingAudioSource = gameObject.AddComponent<AudioSource>();
        loopingAudioSource.loop = true; // 設置為循環播放
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource is missing on SoundManager! Adding one dynamically.");
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    /// <summary>
    /// 播放指定的音效
    /// </summary>
    /// <param name="clip">要播放的音效</param>
    public void PlaySound(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogError("Attempted to play a null AudioClip!");
            return;
        }

        if (audioSource == null)
        {
            Debug.LogError("AudioSource is null! Unable to play sound.");
            return;
        }

        audioSource.PlayOneShot(clip);
    }

    /// <summary>
    /// 播放跳躍音效
    /// </summary>
    public void PlayJumpSound()
    {
        if (jumpSound == null)
        {
            Debug.LogError("JumpSound AudioClip is not assigned in the Inspector!");
            return;
        }
        PlaySound(jumpSound);
    }

    /// <summary>
    /// 播放攻擊音效
    /// </summary>
    public void PlayAttackSound()
    {
        if (attackSound == null)
        {
            Debug.LogError("AttackSound AudioClip is not assigned in the Inspector!");
            return;
        }
        PlaySound(attackSound);
    }

    /// <summary>
    /// 播放衝刺音效
    /// </summary>
    public void PlayDashSound()
    {
        if (dashSound == null)
        {
            Debug.LogError("DashSound AudioClip is not assigned in the Inspector!");
            return;
        }
        PlaySound(dashSound);
    }


    /// <summary>
    /// 播放跑步音效（循環）
    /// </summary>
    public void PlayRunSound()
    {
        if (runSound == null)
        {
            Debug.LogError("RunSound AudioClip is not assigned!");
            return;
        }

        if (!loopingAudioSource.isPlaying)
        {
            loopingAudioSource.clip = runSound;
            loopingAudioSource.Play();
        }
    }

    /// <summary>
    /// 停止跑步音效
    /// </summary>
    public void StopRunSound()
    {
        if (loopingAudioSource.isPlaying)
        {
            loopingAudioSource.Stop();
        }
    }

    /// <summary>
    /// 檢查是否正在播放跑步音效
    /// </summary>
    public bool IsRunningSoundPlaying()
    {
        return loopingAudioSource.isPlaying && loopingAudioSource.clip == runSound;
    }

    /// <summary>
    /// 播放背景音樂
    /// </summary>
    public void PlayBackgroundMusic()
    {
        if (backgroundMusic == null)
        {
            Debug.LogError("BackgroundMusic AudioClip is not assigned in the Inspector!");
            return;
        }

        if (audioSource == null)
        {
            Debug.LogError("AudioSource is null! Unable to play background music.");
            return;
        }

        audioSource.clip = backgroundMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    /// <summary>
    /// 停止背景音樂
    /// </summary>
    public void StopBackgroundMusic()
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is null! Unable to stop background music.");
            return;
        }

        audioSource.Stop();

    }
    public void PlayHurtSound(CharacterType characterType)
    {
        AudioClip clip = GetHurtSound(characterType); // 根據角色類型獲取對應的音效
        if (clip != null)
        {
            PlaySound(clip); // 播放對應的音效
        }
        else
        {
            Debug.LogWarning($"No hurt sound assigned for {characterType}");
        }
    }

    private AudioClip GetHurtSound(CharacterType characterType)
    {
        AudioClip clip = null;
        switch (characterType)
        {
            case CharacterType.Player:
                clip = playerHurtSound;
                break;
            case CharacterType.Monster1:
                clip = monster1HurtSound;
                break;
            default:
                Debug.LogWarning($"CharacterType {characterType} has no assigned hurt sound!");
                break;
        }

        if (clip == null)
        {
            Debug.LogError($"No AudioClip assigned for {characterType} hurt sound.");
        }

        return clip;
    }
}
