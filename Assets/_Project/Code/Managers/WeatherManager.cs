using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeatherManager : MonoBehaviour
{
    public static WeatherManager Instance;
    
    [SerializeField] private Material[] newSkyboxMaterial;
    [SerializeField]private Vector3 _direction;
    [SerializeField] float _windSpeed = 1f;
    [SerializeField] WeatherTypeSo _currentWeatherType;
    private List<WeatherTypeSo> _weatherTypes = new List<WeatherTypeSo>();
    
    PlayerController _playerController;
    
    private Material _currentSkyboxMaterial;
    private bool _isRaining;

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
        var randomweather = Random.Range(0, _weatherTypes.Count);
        AssignWeatherType(_weatherTypes[randomweather]);

    }

    private void Start()
    {
        _playerController = FindFirstObjectByType<PlayerController>();
        
    }

    private void AssignWeatherType(WeatherTypeSo type)
    {
        _currentWeatherType = type;
        _windSpeed = _currentWeatherType.windSpeed;
        _direction = _currentWeatherType.windDirection;
        _isRaining = _currentWeatherType.isRaining;
    }

    private void Update()
    {
        if (_isRaining)
        {
            ApplyWind();
        }
    }

    public void SunnyWeather()
    {
        AssignWeatherType(_weatherTypes.Find(x => x.name == "Sunny"));
        _currentSkyboxMaterial = newSkyboxMaterial[0];
        RenderSettings.skybox = _currentSkyboxMaterial;
    }
    public void RainyWeather()
    {
        AssignWeatherType(_weatherTypes.Find(x => x.name == "RainyWeather"));
        _currentSkyboxMaterial = newSkyboxMaterial[1];
        RenderSettings.skybox = _currentSkyboxMaterial;
    }

    private void ApplyWind()
    {
        _playerController._rb.AddForce(_direction * _windSpeed, ForceMode.Force);
    }
}
