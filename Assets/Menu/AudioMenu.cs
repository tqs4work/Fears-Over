using UnityEngine;

public class AudioMenu : MonoBehaviour
{
    [Header("----------------Audio Source----------------")]
    [SerializeField] AudioSource mucsic1;


    [Header("----------------Audio Clip----------------")]
    public AudioClip background1;
    void Start()
    {
        mucsic1.clip = background1;
        mucsic1.Play();
    }

   
}
