using UnityEngine;

[CreateAssetMenu(fileName = "WeatherTypeSo", menuName = "Scriptable Objects/WeatherTypeSo")]
public class WeatherTypeSo : ScriptableObject
{
    #region PublicFields

    public Vector3 windDirection;
    public float windSpeed;
    public PhysicsMaterial currentFriction;
    public bool isRaining =  true;
    public bool isSnowing = true;

    #endregion
}
