using UnityEngine;

public class AudioMap3 : MonoBehaviour
{
    [Header("----------------Audio Source----------------")]
    public AudioSource bgSource1;
    public AudioSource attackSource;
    public AudioSource dashSource;
    public AudioSource jumpSource;
    public AudioSource blockSource;
    public AudioSource hitSource;
    public AudioSource hurtSource;
    public AudioSource deadSource;
    public AudioSource c1Source;
    public AudioSource c2Source;
    public AudioSource c3Source;
    public AudioSource awardSource;

    [Header("----------------Audio Clip----------------")]
    public AudioClip bg1;
    public AudioClip jump;
    public AudioClip attack;
    public AudioClip dash;
    public AudioClip block;
    public AudioClip hit;
    public AudioClip hurt;
    public AudioClip dead;
    public AudioClip c1;
    public AudioClip c2;
    public AudioClip c3;
    public AudioClip award;

    private void Start()
    {
        bgSource1.clip = bg1;
        bgSource1.Play();

        attackSource.clip = attack;
        dashSource.clip = dash;
        jumpSource.clip = jump;
        blockSource.clip = block;
        hitSource.clip = hit;
        hurtSource.clip = hurt;
        deadSource.clip = dead;
        c1Source.clip = c1;
        c2Source.clip = c2;
        c3Source.clip = c3;
        awardSource.clip = award;
    }
}
