using UnityEngine;

public class ShardCollision : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.IncreaseScore();
            Destroy(gameObject);
        }
    }
}
