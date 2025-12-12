using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField] Image stabar;    
    bool isPause = false;
    void Update()
    {
        
        GameObject player = GameObject.Find("PlayerTest");        

        if (player != null )
        {
            if (player.GetComponent<Transform>().position.x >= 385) transform.Find("Tutorial").Find("exitdirect").gameObject.SetActive(false);
            else transform.Find("Tutorial").Find("exitdirect").gameObject.SetActive(true);

            if (player.GetComponent<PlayerTestMap1>().isDead == true) StartCoroutine(GOon());

            if(player.GetComponent<Transform>().position.x >= 414) { SceneManager.LoadScene("LoadingScene"); }

            stabar.fillAmount = player.GetComponent<PlayerTestMap1>().stamina/100;
            

        }
        

        if (Input.GetKeyDown(KeyCode.R) && player.GetComponent<PlayerTestMap1>().isDead == true)
        {
            Time.timeScale = 1f;
            ReloadCurrentScene();            
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            transform.Find("PausePan").gameObject.SetActive(!isPause);
            ToggleTimeScale();
        }

    }
    public void ReloadCurrentScene()
    {                      
        SceneManager.LoadScene("Map1");        
    }

    IEnumerator GOon()
    {
        yield return new WaitForSeconds(2f);
        transform.Find("GO").gameObject.SetActive(true);    
        Time.timeScale = 0f;
    }


    void ToggleTimeScale()
    {
        isPause = !isPause;

        Time.timeScale = isPause ? 0 : 1;
    }

}
