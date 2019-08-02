
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

        Vector3 normalize = new Vector3(horizontalMovement, rb.velocity.y, verticalMovement).normalized;

        //Vector3.Normalize(new Vector3(horizontalMovement, rb.velocity.y, verticalMovement))

        rb.AddForce(normalize * movementSpeed);
    }
}
