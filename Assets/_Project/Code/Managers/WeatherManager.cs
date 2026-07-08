using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    public static WeatherManager Instance;
    
    [SerializeField] private Material[] newSkyboxMaterial;
    [SerializeField]private Vector3 direction;
    [SerializeField] float windSpeed = 1f;
    [SerializeField] WeatherTypeSo currentWeatherType;

    PlayerController _playerController;
    
    private List<WeatherTypeSo> _weatherTypes = new List<WeatherTypeSo>();
    private Material _currentSkyboxMaterial;
    private bool _isRaining;
    private bool _isSnowing;

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
    }

    private void AssignWeatherType(WeatherTypeSo type)
    {
        currentWeatherType = type;
        windSpeed = currentWeatherType.windSpeed;
        direction = currentWeatherType.windDirection;
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
    }
    public void RainyWeather()
    {
        AssignWeatherType(_weatherTypes.Find(x => x.name == "RainyWeather"));
        _currentSkyboxMaterial = newSkyboxMaterial[1];
        RenderSettings.skybox = _currentSkyboxMaterial;
    }
    public void SnowyWeather()
    {
        AssignWeatherType(_weatherTypes.Find(x => x.name == "SnowyWeather"));
        _currentSkyboxMaterial = newSkyboxMaterial[2];
        RenderSettings.skybox = _currentSkyboxMaterial;
    }

    private void ApplyWind()
    {
        _playerController.rb.AddForce(direction * windSpeed, ForceMode.Force);
    }
}
