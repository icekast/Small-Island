using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public AudioClip soundClip; // ������ �� ���������
    private AudioSource audioSource;

    void Start()
    {
        // �������� ��� ������� AudioSource
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
        audioSource.PlayOneShot(soundClip); // ��������������� ��� ���������� �������� �����
        // ���
        // audioSource.clip = soundClip;
        // audioSource.Play(); // ����������� ���������������
    }
}
