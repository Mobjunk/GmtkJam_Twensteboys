
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;
    [SerializeField]
    float movementSpeed = 100f;

    private Collider _collider;
    private bool _allowInput;

    public bool IsAllowingInput() => _allowInput;
    public void AllowInput()
    {
        _allowInput = true;
    }

    public void DenyInput()
    {
        _allowInput = false;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    void Update()
    {
        if(!_allowInput)
            return;
        
        float horizontalMovement = Input.GetAxis("Horizontal"), // * movementSpeed 
            verticalMovement = Input.GetAxis("Vertical"); // * movementSpeed

        Vector3 normalizedInput = new Vector3(horizontalMovement, 0, verticalMovement).normalized;

        if (normalizedInput.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(normalizedInput, Vector3.up),6);
        }

        //Vector3.Normalize(new Vector3(horizontalMovement, rb.velocity.y, verticalMovement))
        rb.velocity = (normalizedInput * movementSpeed +
                       (IsOnGround() ? Vector3.zero : (rb.velocity.y + Physics.gravity.y) * rb.mass * Vector3.up))
                       * Time.deltaTime;
    }

    [SerializeField] private float MaxGroundDistance = 0.5f;

    bool IsOnGround()
    {
        Debug.DrawRay(transform.position, Vector3.down * MaxGroundDistance, Color.green);
        return Physics.Raycast(transform.position, Vector3.down, _collider.bounds.size.y * 0.5f + MaxGroundDistance);
    }
}
