using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("Audio Clips")]
    //玩家音效
    public AudioClip player_jumpSound; // 跳躍音效
    public AudioClip player_attackSound; // 攻擊音效
    public AudioClip player_backgroundMusic; // 背景音樂
    public AudioClip player_dashSound;    // 衝刺音效
    public AudioClip player_runSound;  // 跑步音效

    //星字怪音效
    public AudioClip startree_prethrowSound;
    public AudioClip startree_throwStarSound;
    public AudioClip startree_spinSound;
    public AudioClip startree_digSound;
    public AudioClip startree_threestarSound;
    public AudioClip startree_dashSound;

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

    #region player sound
    /// <summary>
    /// 播放跳躍音效
    /// </summary>
    public void player_PlayJumpSound()
    {
        if (player_jumpSound == null)
        {
            Debug.LogError("JumpSound AudioClip is not assigned in the Inspector!");
            return;
        }
        PlaySound(player_jumpSound);
    }

    /// <summary>
    /// 播放攻擊音效
    /// </summary>
    public void player_PlayAttackSound()
    {
        if (player_attackSound == null)
        {
            Debug.LogError("AttackSound AudioClip is not assigned in the Inspector!");
            return;
        }
        PlaySound(player_attackSound);
    }

    /// <summary>
    /// 播放衝刺音效
    /// </summary>
    public void player_PlayDashSound()
    {
        if (player_dashSound == null)
        {
            Debug.LogError("DashSound AudioClip is not assigned in the Inspector!");
            return;
        }
        PlaySound(player_dashSound);
    }

    /// <summary>
    /// 播放跑步音效（循環）
    /// </summary>
    public void player_PlayRunSound()
    {
        if (player_runSound == null)
        {
            Debug.LogError("RunSound AudioClip is not assigned!");
            return;
        }

        if (!loopingAudioSource.isPlaying)
        {
            loopingAudioSource.clip = player_runSound;
            loopingAudioSource.Play();
        }
    }

    /// <summary>
    /// 停止跑步音效
    /// </summary>
    public void player_StopRunSound()
    {
        if (loopingAudioSource.isPlaying)
        {
            loopingAudioSource.Stop();
        }
    }

    /// <summary>
    /// 檢查是否正在播放跑步音效
    /// </summary>
    public bool player_IsRunningSoundPlaying()
    {
        return loopingAudioSource.isPlaying && loopingAudioSource.clip == player_runSound;
    }
    #endregion

    #region statree sound

    public void startree_PlayAttackSound(string attackType)
    {
        AudioClip clip = null;

        switch (attackType)
        {
            case "prethrow":
                clip = startree_prethrowSound;
                break;
            case "throwStar":
                clip = startree_throwStarSound;
                break;
            case "spin":
                clip = startree_spinSound;
                break;
            case "dig":
                clip = startree_digSound;
                break;
            case "threestar":
                clip = startree_threestarSound;
                break;
            case "dash":
                clip = startree_dashSound;
                break;
            default:
                Debug.LogWarning($"No sound assigned for StarTree attack type: {attackType}");
                break;
        }

        if (clip != null)
        {
            PlaySound(clip);
        }
    }
    #endregion
    /// <summary>
    /// 播放背景音樂
    /// </summary>
    public void player_PlayBackgroundMusic()
    {
        if (player_backgroundMusic == null)
        {
            Debug.LogError("BackgroundMusic AudioClip is not assigned in the Inspector!");
            return;
        }

        if (audioSource == null)
        {
            Debug.LogError("AudioSource is null! Unable to play background music.");
            return;
        }

        audioSource.clip = player_backgroundMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    /// <summary>
    /// 停止背景音樂
    /// </summary>
    public void player_StopBackgroundMusic()
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is null! Unable to stop background music.");
            return;
        }

        audioSource.Stop();
    }
}
