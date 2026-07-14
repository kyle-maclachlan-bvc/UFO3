using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance;

   private float _score;
   
   void Awake()
   {
      if (Instance == null)
      {
         Instance = this;
      }
      else
      {
         Destroy(this);
         return;
      }
   }

   public void IncreaseScore()
   {
      _score += 10f;
      Debug.Log("Score: " + _score);
   }
}
