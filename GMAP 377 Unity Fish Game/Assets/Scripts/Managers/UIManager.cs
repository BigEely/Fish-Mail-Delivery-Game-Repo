using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName) //UIManager.instance.LoadScene(sceneName);
    { 
        SceneManager.LoadScene(sceneName);
    }

    public void ToggleObject(GameObject subject) //UIManager.instance.ToggleObject(subject);
    { 
        subject.SetActive(!subject.activeSelf);
    }
}
