using UnityEngine;
using UnityEngine.SceneManagement;
public class PausePan : MonoBehaviour
{
         
    public void Resume()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }    
    public void BackMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    } 
    public void ExitDestop()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false; // Dừng chế độ chạy trong Editor
        #else
                    Application.Quit(); // Thoát ứng dụng
        #endif
    }
}
