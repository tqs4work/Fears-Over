using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject Buttonsss;
    
    private void Start()
    {
#if UNITY_EDITOR
        EditorPrefs.SetInt("sceneIndex", 0); // Lưu giá trị mới của t
#else
        PlayerPrefs.SetInt("sceneIndex", 0);
#endif
    }
    public void OnButtonClick()
    {
        // Tải LoadingScene trước
        SceneManager.LoadScene("LoadingScene");        

    }
    public void Exit()
    {        
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false; // Dừng chế độ chạy trong Editor
        #else
            Application.Quit(); // Thoát ứng dụng
        #endif
    }

    bool isShow = false;
    public void Toggle()
    {
        isShow = !isShow;
        Buttonsss.gameObject.SetActive(isShow);
    }
    public void Load1()
    {
        SceneManager.LoadScene("Map1");
    }
    public void Load2()
    {
        SceneManager.LoadScene("Map2");
    }
    public void Load3()
    {
        SceneManager.LoadScene("Map3");
    }
}
