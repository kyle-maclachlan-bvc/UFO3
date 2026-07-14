using UnityEngine;

public class ManagerDontDestroy : MonoBehaviour
{
    static ManagerDontDestroy _instance;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
