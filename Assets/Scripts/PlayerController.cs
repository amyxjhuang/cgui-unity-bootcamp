using UnityEngine;

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
    public float bulletSpeed = 30f;

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

        }

    }

    public void FireGun()
    {
        if (isHoldingGun)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.linearVelocity = firePoint.forward * bulletSpeed;
        }
    }
}
