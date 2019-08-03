
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;
    [SerializeField]
    float movementSpeed = 100f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal"), // * movementSpeed 
            verticalMovement = Input.GetAxis("Vertical"); // * movementSpeed

        Vector3 normalize = new Vector3(horizontalMovement, 0, verticalMovement).normalized;

        //Vector3.Normalize(new Vector3(horizontalMovement, rb.velocity.y, verticalMovement))
        rb.velocity = (normalize * movementSpeed +
                       (IsOnGround() ? Vector3.zero : (rb.velocity.y + Physics.gravity.y) * rb.mass * Vector3.up))
                       * Time.deltaTime;
    }

    [SerializeField] private float MaxGroundDistance = 0.5f;

    bool IsOnGround()
    {
        return Physics.Raycast(transform.position, Vector3.down, MaxGroundDistance);
    }
}
