using UnityEngine;

public class WeatherObject : MonoBehaviour
{
    /// <summary>
    /// A temporary script that may be deleted later if necessary.
    /// This script grabs the snow objects on each object to allow it to be accessible by code.
    /// </summary>
    
    [Header("Weather Objects")]
    [SerializeField] private GameObject[] snowObject;

    public void SetSnowEnabled(bool enabled)
    {
        foreach (GameObject snow in snowObject)
        {
            if (snow != null)
                snow.SetActive(enabled);
        }
    }
}
