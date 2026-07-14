using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    [Header("Movement Points")]
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    [Header("Settings")]
    [SerializeField] private float speed = 3f;
    [SerializeField] private float arrivalThreshold = 0.1f;

    private Vector3 currentTarget;

    private void Start()
    {
        transform.position = pointA.position;
        currentTarget = pointB.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
        
        if (Vector3.Distance(transform.position, currentTarget) < arrivalThreshold)
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