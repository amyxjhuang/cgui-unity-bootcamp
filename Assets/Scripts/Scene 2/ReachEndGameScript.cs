using UnityEngine;
using UnityEngine.SceneManagement;

public class ReachEndGameScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    void onCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ScoreManagerScript.Instance.StopTimer();
            SceneManager.LoadScene("End Scene");
        }
    }
}
