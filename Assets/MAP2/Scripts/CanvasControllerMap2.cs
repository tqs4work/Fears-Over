using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasControllerMap2 : MonoBehaviour
{
    GameObject player;
    public GameObject portal;
    [SerializeField] Image staBar;
    [SerializeField] Image hpBar;
    [SerializeField] GameObject GOPan;
    [SerializeField] GameObject ChallengeController;

    bool isPause = false;
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float sta = player.GetComponent<PlayerTestMap2>().stamina;
        float hp = player.GetComponent<PlayerTestMap2>().hp;

        staBar.fillAmount = sta / 100f;
        hpBar.fillAmount = hp / 100f;


        // ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            transform.Find("PausePan").gameObject.SetActive(!isPause);
            ToggleTimeScale();
        }


        //GO panel
        if (player.GetComponent<PlayerTestMap2>().isDead)
        {
            StartCoroutine(GOon());
        }


        //Reload Map
        if (Input.GetKeyDown(KeyCode.R) && player.GetComponent<PlayerTestMap2>().isDead)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Map2");
        }

        //Challenge Panel
        if (player.transform.position.x >= 6 && player.transform.position.x <=8 
            && ChallengeController.GetComponent<ChallengeControllerMap2>().isChallenge == false)
        {
            transform.Find("ChallengePan").gameObject.SetActive(true);
        }
        else transform.Find("ChallengePan").gameObject.SetActive(false);


        //Portal
        if (player.transform.position.x >= 10 && portal.activeSelf) transform.Find("PortalPan").gameObject.SetActive(false);
        else if (portal.activeSelf)
        {
            transform.Find("PortalPan").gameObject.SetActive(true);
        }

        //LoadScene
        if (portal.activeSelf && player.transform.position.x >= 31)
        {
            SceneManager.LoadScene("LoadingScene");
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
