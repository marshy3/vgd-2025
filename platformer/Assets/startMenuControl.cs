using UnityEngine;
using UnityEngine.SceneManagement;

public class startMenuControl : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onStartClick()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void onEndClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif 
        Application.Quit();
    }
}
