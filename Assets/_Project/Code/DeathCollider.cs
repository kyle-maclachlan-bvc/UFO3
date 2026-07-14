using UnityEngine;

public class DeathCollider : MonoBehaviour
{
   void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         Invoke("InvokeReloadScene", 1.5f);
      }
      
   }
   
   void InvokeReloadScene()
   {
      sceneManager.Instance.ReloadScene();
   }
}
