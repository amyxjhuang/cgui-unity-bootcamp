using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    Quaternion startingRotation;
    public bool isHoldingGun = false;

    [Header("Jump Forces")]
    public float jumpUpForce = 3f;
    public float jumpForwardForce = 3f;

    [Header("Raycast")]
    public LayerMask groundMask = ~0; // everything by default
    bool isGrounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        startingRotation = transform.rotation;

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isGrounded)
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
        // Only count collisions with the Ground object (recommended: set the Ground GameObject's Tag to "Ground")
        if (collision.gameObject.CompareTag("Ground"))
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    isGrounded = true;
                    // Reset to "normal facing" when landing
                    Vector3 e = transform.eulerAngles;
                    transform.rotation = Quaternion.Euler(0f, e.y, 0f);
                }
            }
        }

        if (collision.gameObject.CompareTag("Gun"))
        {
            isHoldingGun = true;
            collision.gameObject.SetActive(false);
        }
        
    }
}
