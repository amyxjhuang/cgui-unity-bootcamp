using UnityEngine;

public class Part2PlayerControllerScript : MonoBehaviour
{
    
    void OnCollisionEnter(Collision collision)
    {
        if (HasTagInHierarchy(collision.gameObject, "Platform 3"))
        {
            ScoreManagerScript.Instance.setPlatformLevel(3);

        } else if (HasTagInHierarchy(collision.gameObject, "Platform 4")) {
            ScoreManagerScript.Instance.setPlatformLevel(4);
        }
    }

    bool HasTagInHierarchy(GameObject obj, string tag)
    {
        Transform current = obj.transform;
        while (current != null)
        {
            if (current.CompareTag(tag))
            {
                return true;
            }
            current = current.parent;
        }
        return false;
    }
}
