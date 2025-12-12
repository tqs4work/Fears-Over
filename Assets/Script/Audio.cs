using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{
    [Header("----------------Audio Source----------------")]
    [SerializeField] AudioSource bgmusicSource1;
    [SerializeField] AudioSource bgmusicSource2;
    [SerializeField] AudioSource bgmusicSource3;
    [SerializeField] AudioSource bgmusicSource4;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource endSource;

    [Header("----------------Audio Clip----------------")]
    public AudioClip background1;
    public AudioClip background2;
    public AudioClip background3;
    public AudioClip background4;
    public AudioClip footsteps;
    public AudioClip isDetected;
    public AudioClip jumpfx;
    public AudioClip isDead;


    int count = 0;


    private void Start()
    {
        bgmusicSource1.clip = background1;
        bgmusicSource1.Play();

        bgmusicSource2.clip = background2;       
        bgmusicSource2.Play();

        bgmusicSource3.clip = background3;
        bgmusicSource3.Play();

        bgmusicSource4.clip = background4;
        StartCoroutine(PlayMusicEvery5Seconds());

        musicSource.clip = isDetected;      
        
        SFXSource.clip = footsteps;

        endSource.clip = isDead;

    }
    void Update()
    {

        GameObject player = GameObject.Find("PlayerTest");
        GameObject enemy1 = GameObject.Find("EnemyTest1");
        GameObject enemy2 = GameObject.Find("EnemyTest2");
        GameObject enemy3 = GameObject.Find("EnemyTest3");
        // Kiểm tra xem nhân vật có đang chạy hay không
        bool isRunning = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A);

        // Phát âm thanh chạy nếu nhân vật đang chạy và không đang phát âm thanh chạy
        if (isRunning && !SFXSource.isPlaying )
        {
            SFXSource.clip = footsteps; // Đặt âm thanh chạy
            SFXSource.loop = true; // Đặt âm thanh chạy lặp lại
            SFXSource.Play();
        }
        // Dừng âm thanh chạy nếu nhân vật không chạy
        else if (!isRunning && SFXSource.isPlaying && SFXSource.clip == footsteps)
        {
            SFXSource.Stop();
        }

        // Kiểm tra nhảy
        if (Input.GetKeyDown(KeyCode.Space) && player.GetComponent<PlayerTestMap1>().isGround)
        {
            SFXSource.clip = jumpfx; // Đặt âm thanh nhảy
            SFXSource.PlayOneShot(jumpfx); // Phát âm thanh nhảy
        }


        if (enemy1.GetComponent<EnemyTest>().isDetect == true 
            || enemy2.GetComponent<EnemyTest>().isDetect == true
            || enemy3.GetComponent<EnemyTest>().isDetect == true)
        {
            if (musicSource.isPlaying == false) 
            {
                musicSource.Play();
            }
            
        }

        

        if (player.GetComponent<PlayerTestMap1>().isDead == true)
        {
            
            bgmusicSource1.Stop();
            bgmusicSource2.Stop();
            bgmusicSource3.Stop();
            bgmusicSource4.Stop();
            musicSource.Stop();
            if (endSource.isPlaying == false && count == 0)
            {
                endSource.Play();
                count = 1;
            }
            
        }



    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    

    IEnumerator PlayMusicEvery5Seconds()
    {
        while (true) // Vòng lặp vô hạn
        {
            bgmusicSource4.Play(); // Phát nhạc
            yield return new WaitForSeconds(5f); // Đợi 5 giây
        }
    }
}