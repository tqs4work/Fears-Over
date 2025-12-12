using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ChallengeControllerMap2 : MonoBehaviour
{
    public GameObject dummy1;
    public GameObject dummy2;
    public GameObject dummy3;
    public GameObject audioMap2;
    public GameObject portal;
    public GameObject arrowPreLeft;
    public GameObject arrowPreRight;
    GameObject arrowCurLeft;
    GameObject arrowCurRight;
    float arrowSpeed = 15f;
    float randomY;
    float timer = 60f;

    public Text textTimer;
    public bool isChallenge = false;

    GameObject player;
    public GameObject canvas;
    private void Start()
    {
        player = GameObject.Find("Player");
        
    }
    private void Update()
    {
        if (canvas.transform.Find("ChallengePan").gameObject.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            PlayChallenge();
            isChallenge = true;

            audioMap2.GetComponent<AudioMap2>().bgSource1.Stop();

            if (!audioMap2.GetComponent<AudioMap2>().c1Source.isPlaying)
            {
                audioMap2.GetComponent<AudioMap2>().c1Source.Play();
            }
        }
                
        RuleChallenge();

        if (isChallenge)
        {
            dummy1.SetActive(false);
            dummy2.SetActive(false);
            dummy3.SetActive(false);
        }
        else
        {
            dummy1.SetActive(true);
            dummy2.SetActive(true);
            dummy3.SetActive(true);
        }

    }

    void TimerSet()
    {
        if (timer <= 0)
        {
            CancelInvoke();
            if (arrowCurLeft != null) Destroy(arrowCurLeft);
            if (arrowCurRight != null) Destroy(arrowCurRight);
            canvas.transform.Find("TimerPan").gameObject.SetActive(false);
            isChallenge = false;
            portal.SetActive(true);
            canvas.transform.Find("PortalPan").gameObject.SetActive(true);
            audioMap2.GetComponent<AudioMap2>().c2Source.Stop();
            audioMap2.GetComponent<AudioMap2>().bgSource1.Play();
        }

        else if (timer < 30 && player.GetComponent<PlayerTestMap2>().isDead == false)
        {
            audioMap2.GetComponent<AudioMap2>().c1Source.Stop();
            if (!audioMap2.GetComponent<AudioMap2>().c2Source.isPlaying)
            {
                audioMap2.GetComponent<AudioMap2>().c2Source.Play();
            }
        }
        timer -= 1;
        textTimer.text = timer.ToString();
        if((timer%5==0))
        {
            arrowSpeed += 1f;
        }
    }

    void SpawnArrowLeft()
    {
        randomY = Random.Range(-1, -4.25f);
        arrowCurLeft = Instantiate(arrowPreLeft, new Vector3(-20, randomY, 0), Quaternion.identity);                
    }
    void SpawnArrowRight()
    {
        randomY = Random.Range(-1, -4.25f);
        arrowCurRight = Instantiate(arrowPreRight, new Vector3(35, randomY, 0), Quaternion.identity);
    }

    void PlayChallenge()
    {
        InvokeRepeating("SpawnArrowLeft", 0, 4);
        InvokeRepeating("SpawnArrowRight", 0, 7);
        InvokeRepeating("TimerSet", 0, 1);
        canvas.transform.Find("TimerPan").gameObject.SetActive(true);
    }
    void RuleChallenge()
    {
        float step = arrowSpeed * Time.deltaTime;

        if (arrowCurLeft != null)
        {
            arrowCurLeft.transform.position = Vector2.MoveTowards(arrowCurLeft.transform.position, new Vector2(35, randomY), step);

            if (Vector2.Distance(arrowCurLeft.transform.position, new Vector2(35, randomY)) <= 0) Destroy(arrowCurLeft);

            if (Mathf.Abs(arrowCurLeft.transform.position.x - player.transform.position.x) < 0.1f
                && player.GetComponent<PlayerTestMap2>().isDash == false)
            {
                if (arrowCurLeft.transform.position.y < -3.7 && player.GetComponent<PlayerTestMap2>().isGround == false)
                {

                }
                else if (player.GetComponent<PlayerTestMap2>().isBlock == true && player.GetComponent<PlayerTestMap2>().stamina >= 50)
                {
                    player.GetComponent<PlayerTestMap2>().isSucBlock = true;
                    player.GetComponent<PlayerTestMap2>().stamina -= 50;
                    Destroy(arrowCurLeft);
                }
                else if (player.GetComponent<PlayerTestMap2>().isBlock == true)
                {
                    float sta = player.GetComponent<PlayerTestMap2>().stamina;
                    player.GetComponent<PlayerTestMap2>().stamina = 0;
                    player.GetComponent<PlayerTestMap2>().TakeDame((float)((50 - sta) * 0.2 + 25));
                    Destroy(arrowCurLeft);
                }
                else
                {
                    player.GetComponent<PlayerTestMap2>().TakeDame(30);
                    Destroy(arrowCurLeft);
                }
            }

        }

        if (arrowCurRight != null)
        {
            arrowCurRight.transform.position = Vector2.MoveTowards(arrowCurRight.transform.position, new Vector2(-20, randomY), step);

            if (Vector2.Distance(arrowCurRight.transform.position, new Vector2(-20, randomY)) <= 0) Destroy(arrowCurRight);

            if (Mathf.Abs(arrowCurRight.transform.position.x - player.transform.position.x) < 0.1f
                && player.GetComponent<PlayerTestMap2>().isDash == false)
            {
                if (arrowCurRight.transform.position.y < -3.7 && player.GetComponent<PlayerTestMap2>().isGround == false)
                {

                }
                else if (player.GetComponent<PlayerTestMap2>().isBlock == true && player.GetComponent<PlayerTestMap2>().stamina >= 50)
                {
                    player.GetComponent<PlayerTestMap2>().isSucBlock = true;
                    player.GetComponent<PlayerTestMap2>().stamina -= 50;
                    Destroy(arrowCurRight);
                }
                else if (player.GetComponent<PlayerTestMap2>().isBlock == true)
                {
                    float sta = player.GetComponent<PlayerTestMap2>().stamina;
                    player.GetComponent<PlayerTestMap2>().stamina = 0;
                    player.GetComponent<PlayerTestMap2>().TakeDame((float)((50 - sta) * 0.2 + 25));
                    Destroy(arrowCurRight);
                }
                else
                {
                    player.GetComponent<PlayerTestMap2>().TakeDame(30);
                    Destroy(arrowCurRight);
                }
            }

        }


        

        
    }
}
