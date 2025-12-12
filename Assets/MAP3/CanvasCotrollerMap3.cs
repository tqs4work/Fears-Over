using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class CanvasCotrollerMap3 : MonoBehaviour
{
    GameObject player;
    bool isPause = false;

    public GameObject audioMap3;

    [SerializeField] Image staBar;
    [SerializeField] Image hpBar;
    [SerializeField] Image bossHpBar;
    [SerializeField] GameObject GOPan;
    [SerializeField] GameObject NearRoomPan;
    [SerializeField] GameObject Wallb1;
    [SerializeField] GameObject Wallb2;
    [SerializeField] GameObject Boss;

    public bool isFight = false;


    void Start()
    {
        player = GameObject.Find("PlayerTest");
    }

    // Update is called once per frame
    void Update()
    {
        //Bar
        float sta = player.GetComponent<PlayerTestMap3>().stamina;
        float hp = player.GetComponent<PlayerTestMap3>().hp;

        staBar.fillAmount = sta / 100f;
        hpBar.fillAmount = hp / 100f;


        //Portal
        if (player != null)
        {
            if (player.transform.position.x >= 275) SceneManager.LoadScene("Menu");

        }


        //ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            transform.Find("PausePan").gameObject.SetActive(!isPause);
            ToggleTimeScale();
        }

        //Reload Map
        if (Input.GetKeyDown(KeyCode.R) && player.GetComponent<PlayerTestMap3>().isDead)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Map3");
        }

        //GO panel
        if (player.GetComponent<PlayerTestMap3>().isDead)
        {
            StartCoroutine(GOon());
        }

        //NearRoomPan
        if (player.transform.position.x >= 120 && isFight == false && bossHpBar.fillAmount > 0)
        {
            NearRoomPan.SetActive(true);
        }
        else
        {
            NearRoomPan.SetActive(false);
        }

        //EnterRoom
        if (NearRoomPan.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            Wallb1.SetActive(false);
            NearRoomPan.SetActive(false) ;
            isFight = true;

            audioMap3.GetComponent<AudioMap3>().bgSource1.Stop();

            if (!audioMap3.GetComponent<AudioMap3>().c2Source.isPlaying)
            {
                audioMap3.GetComponent<AudioMap3>().c2Source.Play();
            }

        }

        //Boss HP Bar

        if (!isFight)
        {
            bossHpBar.gameObject.SetActive(false);
        }
        else
        {
            bossHpBar.gameObject.SetActive(true);
            bossHpBar.fillAmount = Boss.GetComponent<BossTestMap3>().hp / 200f; /////
        }

        //Way out
        if(bossHpBar.fillAmount <= 0)
        {
            Wallb2.SetActive(false);
            audioMap3.GetComponent<AudioMap3>().c2Source.Stop();
            audioMap3.GetComponent<AudioMap3>().awardSource.Play();

            if (!audioMap3.GetComponent<AudioMap3>().c3Source.isPlaying)
            {
                audioMap3.GetComponent<AudioMap3>().c3Source.Play();
            }
        }



    }
    void ToggleTimeScale()
    {
        isPause = !isPause;
        Time.timeScale = isPause ? 0 : 1;
    }

    IEnumerator GOon()
    {
        yield return new WaitForSeconds(2);
        GOPan.SetActive(true);
        Time.timeScale = 0;
    }


}
