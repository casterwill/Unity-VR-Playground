using UnityEngine;

public class RotateBasedOnVelocity : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private float fallRotateSpeed = 10f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (rb.linearVelocity.sqrMagnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(rb.linearVelocity);
            rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, fallRotateSpeed * Time.fixedDeltaTime));
        }
    }
}
