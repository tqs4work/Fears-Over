using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
#if UNITY_EDITOR
        int t = EditorPrefs.GetInt("sceneIndex", 0);
#else
        int t = PlayerPrefs.GetInt("sceneIndex", 0);
#endif
        t++;
#if UNITY_EDITOR
        EditorPrefs.SetInt("sceneIndex", t); // Lưu giá trị mới của t
#else
        PlayerPrefs.SetInt("sceneIndex", t);
#endif
        Debug.Log("Current t value: " + t); // Kiểm tra giá trị t

        StartCoroutine(LoadAfter2s(t));
    }

    IEnumerator LoadAfter2s(int t)
    {
        yield return new WaitForSeconds(2);
        if (t == 1) SceneManager.LoadScene("Map1");
        else if (t == 2) SceneManager.LoadScene("Map2");
        else if (t == 3) SceneManager.LoadScene("Map3");
        else
        {
            // Reset t về 0 sau khi tải hết các scene
#if UNITY_EDITOR
            EditorPrefs.SetInt("sceneIndex", 0);
#else
            PlayerPrefs.SetInt("sceneIndex", 0);
#endif
        }
    }
}
