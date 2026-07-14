using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlatformMover: MonoBehaviour
{
    [Header("Movement Points")]
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    [Header("Settings")]
    [SerializeField] private float speed = 3f;
    [SerializeField] private float arrivalThreshold = 0.1f;

    private Vector3 currentTarget;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        rb.isKinematic = true; 
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        transform.position = pointA.position;
        currentTarget = pointB.position;
    }

    private void FixedUpdate()
    {
        Vector3 nextPosition = Vector3.MoveTowards(rb.position, currentTarget, speed * Time.fixedDeltaTime);
        
        rb.MovePosition(nextPosition);
        
        if (Vector3.Distance(rb.position, currentTarget) < arrivalThreshold)
        {
            if (currentTarget == pointB.position)
            {
                currentTarget = pointA.position;
            }
            else
            {
                currentTarget = pointB.position;
            }
        }
    }
}