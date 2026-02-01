using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        Debug.Log("Loading scene: " + sceneName);
        SceneManager.LoadScene(sceneName);
        if (sceneName == "Scene Part 1")
        {
            if (ScoreManagerScript.Instance != null)
            {
                ScoreManagerScript.Instance.StartTimer();
            }
        }
    }
}
