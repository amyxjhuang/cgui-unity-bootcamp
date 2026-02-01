using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    Quaternion startingRotation;

    [Header("Gun")]
    public bool isHoldingGun = false;
    public Transform propHoldPoint; 
    private GameObject equippedGun; 
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 35f;
    public Canvas gunCanvas; 

    [Header("Jump Forces")]
    public float jumpUpForce = 3f;
    public float jumpForwardForce = 3f;

    [Header("Raycast")]
    public LayerMask groundMask = ~0; 
    bool isGrounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        startingRotation = transform.rotation;

        // Hide canvas initially if it exists
        if (gunCanvas != null)
        {
            gunCanvas.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Don't jump if clicking on UI
        if (Input.GetMouseButtonDown(0) && isGrounded && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector2 screenPosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(screenPosition);
            RaycastHit hit;
            
            // Finds where the mouse is pointing
            if (Physics.Raycast(ray, out hit, 100f, groundMask))
            {
                Vector3 targetPosition = hit.point;
                Vector3 flatDirection = (targetPosition - transform.position).normalized;

                transform.rotation = Quaternion.LookRotation(flatDirection, Vector3.up);
                Vector3 v = flatDirection * jumpForwardForce;
                v.y = jumpUpForce;
                rb.linearVelocity = v;

                isGrounded = false;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;

        if (HasTagInHierarchy(collision.gameObject, "Platform 2"))
        {
            isHoldingGun = false;
            equippedGun = null;
            gunCanvas.gameObject.SetActive(false);
        } else if (HasTagInHierarchy(collision.gameObject, "Platform 3")) {
            isHoldingGun = false;
            equippedGun = null;
            gunCanvas.gameObject.SetActive(false);
            isGrounded = true;
            SceneManager.LoadScene("Scene Part 2");
        }
        // foreach (ContactPoint contact in collision.contacts)
        // { 
        //     // Reset to "normal facing" when landing
        Vector3 e = transform.eulerAngles;
        transform.rotation = Quaternion.Euler(0f, e.y, 0f);
        // }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Gun") && !isHoldingGun)
        {
            isHoldingGun = true;
            equippedGun = collision.gameObject;
            equippedGun.transform.SetParent(propHoldPoint, worldPositionStays: false);

            // Snap exactly to hold point
            equippedGun.transform.localPosition = Vector3.zero;
            equippedGun.transform.localRotation = Quaternion.identity;

            // Show canvas when gun is equipped
            if (gunCanvas != null)
            {
                gunCanvas.gameObject.SetActive(true);
            }
        }

    }

    public void FireGun()
    {
        if (isHoldingGun)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            
            // Flatten the forward direction to shoot horizontally (remove vertical component)
            Vector3 flatForward = firePoint.forward;
            flatForward.y = 0f;
            flatForward.Normalize();
            
            rb.linearVelocity = flatForward * bulletSpeed;
        }
    }

    /**
     * @return GameObject return the equippedGun
     */
    public GameObject getEquippedGun() {
        return equippedGun;
    }

    /**
     * @param equippedGun the equippedGun to set
     */
    public void setEquippedGun(GameObject equippedGun) {
        this.equippedGun = equippedGun;
    }

    /**
     * Checks if the GameObject or any of its parents have the specified tag
     * @param obj The GameObject to check
     * @param tag The tag to search for
     * @return true if the tag is found in the object or its parent hierarchy
     */
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

    public void decreaseScore(int amount) {
        ScoreManagerScript.Instance.subtractScore(amount);
    }
}
