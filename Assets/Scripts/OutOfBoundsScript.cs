using UnityEngine;
using UnityEngine.SceneManagement;
public class OutOfBoundsScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Debug.Log("Platform Level: " + ScoreManagerScript.Instance.platformLevel);
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("player hit out of bounds");
            if (ScoreManagerScript.Instance.platformLevel == 1 || ScoreManagerScript.Instance.platformLevel == 2) {
                SceneManager.LoadScene("Scene Part 1");
            } else if (ScoreManagerScript.Instance.platformLevel == 3 || ScoreManagerScript.Instance.platformLevel == 4) {
                SceneManager.LoadScene("Scene Part 2");
            }
        }
    }
}
