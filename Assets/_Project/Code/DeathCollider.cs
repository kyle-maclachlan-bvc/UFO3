using UnityEngine;

public class DeathCollider : MonoBehaviour
{
   void OnTriggerEnter(Collider other)
   {
      Invoke("InvokeReloadScene", 1.5f);
   }
   
   void InvokeReloadScene()
   {
      LevelManager.Instance.ReloadScene();
   }
}
