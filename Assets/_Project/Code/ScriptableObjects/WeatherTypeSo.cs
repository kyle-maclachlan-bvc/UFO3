using UnityEngine;

[CreateAssetMenu(fileName = "WeatherTypeSo", menuName = "Scriptable Objects/WeatherTypeSo")]
public class WeatherTypeSo : ScriptableObject
{
    #region PublicFields

    public Vector3 windDirection;
    public float windSpeed;
    public bool isRaining =  true;
    public bool isSnowing = true;

    #endregion
}
