using System.Collections.Generic;
using UnityEngine;

public partial class WeatherManager : MonoBehaviour
{
    #region Instance

    public static WeatherManager Instance;

    #endregion

    #region SerializedFields

    [SerializeField] private GameObject playerObject;
    [SerializeField] private Material[] newSkyboxMaterial;
    [SerializeField]private Vector3 direction;
    [SerializeField] float windSpeed = 1f;
    [SerializeField] WeatherTypeSo currentWeatherType;

        #endregion

    #region References

        PlayerController _playerController;

        #endregion

    #region Private Fields

        private List<WeatherTypeSo> _weatherTypes = new List<WeatherTypeSo>();
        private Material _currentSkyboxMaterial;
        private PhysicsMaterial _currentFriction;
        private BoxCollider _playerCollider;
        private bool _isRaining;
        private bool _isSnowing;

        #endregion

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _currentSkyboxMaterial = RenderSettings.skybox;
        _weatherTypes.AddRange(Resources.LoadAll<WeatherTypeSo>("ScriptableObjects"));
    }
    private void Start()
    {
        _playerController = FindFirstObjectByType<PlayerController>();
        _playerCollider = playerObject.GetComponent<BoxCollider>();
    }
    private void AssignWeatherType(WeatherTypeSo type)
    {
        currentWeatherType = type;
        windSpeed = currentWeatherType.windSpeed;
        direction = currentWeatherType.windDirection;
        _currentFriction = currentWeatherType.currentFriction;
        _isRaining = currentWeatherType.isRaining;
        _isSnowing = currentWeatherType.isSnowing;
    }
    private void Update()
    {
        if (_isRaining || _isSnowing)
        {
            ApplyWind();
        }
    }
    public void SunnyWeather()
    {
        AssignWeatherType(_weatherTypes.Find(x => x.name == "SunnyWeather"));
        _currentSkyboxMaterial = newSkyboxMaterial[0];
        RenderSettings.skybox = _currentSkyboxMaterial;
        _playerCollider.material = _currentFriction;

    }
    public void RainyWeather()
    {
        AssignWeatherType(_weatherTypes.Find(x => x.name == "RainyWeather"));
        _currentSkyboxMaterial = newSkyboxMaterial[1];
        RenderSettings.skybox = _currentSkyboxMaterial;
        _playerCollider.material = _currentFriction;
    }
    public void SnowyWeather()
    {
        AssignWeatherType(_weatherTypes.Find(x => x.name == "SnowyWeather"));
        _currentSkyboxMaterial = newSkyboxMaterial[2];
        RenderSettings.skybox = _currentSkyboxMaterial;
        _playerCollider.material = _currentFriction;
    }
    private void ApplyWind()
    {
        _playerController.rb.AddForce(direction * windSpeed, ForceMode.Force);
    }
}
