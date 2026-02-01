using UnityEngine;
using UnityEngine.SceneManagement;

public class ReachEndGameScript : MonoBehaviour
{
    void onCollisionEnter(Collision collision)
    {
        Debug.Log("player hit end game");
        if (collision.gameObject.CompareTag("Player"))
        {
            ScoreManagerScript.Instance.StopTimer();
            SceneManager.LoadScene("End Scene");
        }
    }
}
