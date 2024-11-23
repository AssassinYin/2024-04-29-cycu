using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("Audio Clips")]
    public AudioClip playerHurtSound;
    public AudioClip jumpSound; // ���D����
    public AudioClip attackSound; // ��������
    public AudioClip backgroundMusic; // �I������
    public AudioClip dashSound;    // �Ĩ뭵��
    public AudioClip runSound;  // �]�B����

    public AudioClip monster1HurtSound;


    private AudioSource audioSource;
    private AudioSource loopingAudioSource; // �Ω�`�����񭵮ġ]�Ҧp�]�B�^


    private void Awake()
    {
        // Singleton �]�w
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

        // ��l�� AudioSource
        audioSource = GetComponent<AudioSource>();
        loopingAudioSource = gameObject.AddComponent<AudioSource>();
        loopingAudioSource.loop = true; // �]�m���`������
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource is missing on SoundManager! Adding one dynamically.");
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    /// <summary>
    /// ������w������
    /// </summary>
    /// <param name="clip">�n���񪺭���</param>
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
    /// ������D����
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
    /// �����������
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
    /// ����Ĩ뭵��
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
    /// ����]�B���ġ]�`���^
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
    /// ����]�B����
    /// </summary>
    public void StopRunSound()
    {
        if (loopingAudioSource.isPlaying)
        {
            loopingAudioSource.Stop();
        }
    }

    /// <summary>
    /// �ˬd�O�_���b����]�B����
    /// </summary>
    public bool IsRunningSoundPlaying()
    {
        return loopingAudioSource.isPlaying && loopingAudioSource.clip == runSound;
    }

    /// <summary>
    /// ����I������
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
    /// ����I������
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
        AudioClip clip = GetHurtSound(characterType); // �ھڨ��������������������
        if (clip != null)
        {
            PlaySound(clip); // �������������
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
