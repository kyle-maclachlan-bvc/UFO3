using UnityEngine;

public class ShardCollision : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }

    void OnDisable()
    {
        Invoke("CollectionMethod", 1.5f);
    }

    void CollectionMethod()
    {
        Destroy(gameObject);
        GameManager.Instance.IncreaseScore();
        sceneManager.Instance.LoadNextScene();
        Time.timeScale = 1f;
    }
}
