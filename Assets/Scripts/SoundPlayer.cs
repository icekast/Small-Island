using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public AudioClip soundClip; // Ссылка на аудиофайл
    private AudioSource audioSource;

    void Start()
    {
        // Получаем или создаем AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlaySound();
        }
    }
    public void PlaySound()
    {
        audioSource.PlayOneShot(soundClip); // Воспроизведение без прерывания текущего звука
        // или
        // audioSource.clip = soundClip;
        // audioSource.Play(); // Стандартное воспроизведение
    }
}
