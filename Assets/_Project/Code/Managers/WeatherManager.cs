using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class WeatherManager : MonoBehaviour
{
    #region Instance
    public static WeatherManager Instance;
    #endregion

    #region SerializedFields
    [SerializeField] private Material[] newSkyboxMaterial;
    [SerializeField] private Vector3 direction;
    [SerializeField] private float windSpeed = 1f;
    [SerializeField] private WeatherTypeSo currentWeatherType;
    #endregion

    #region References
    private PlayerController _playerController;
    private BoxCollider _playerCollider;
    #endregion

    #region Private Fields
    private List<WeatherTypeSo> _weatherTypes = new List<WeatherTypeSo>();
    private PhysicsMaterial _currentFriction;
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
            return;
        }

        _weatherTypes.AddRange(Resources.LoadAll<WeatherTypeSo>("ScriptableObjects"));
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
       
        _playerController = FindFirstObjectByType<PlayerController>();
        
        if (_playerController != null)
        {
            _playerCollider = _playerController.GetComponent<BoxCollider>();
        }

        
        ApplyWeatherForScene(scene.buildIndex);
    }

    private void ApplyWeatherForScene(int sceneIndex)
    {
        switch (sceneIndex)
        {
            case 0: 
                SunnyWeather();
                break;
            case 1: 
                SnowyWeather();
                break;
            case 2: 
                RainyWeather();
                break;
        }
    }

    private void Update()
    {
        
        if ((_isRaining || _isSnowing) && _playerController != null && _playerController.rb != null)
        {
            ApplyWind();
        }
    }

    private void AssignWeatherType(WeatherTypeSo type)
    {
        if (type == null) return;

        currentWeatherType = type;
        windSpeed = currentWeatherType.windSpeed;
        direction = currentWeatherType.windDirection;
        _isRaining = currentWeatherType.isRaining;
        _isSnowing = currentWeatherType.isSnowing;

        
        if (_playerCollider != null)
        {
            _playerCollider.material = _currentFriction;
        }
    }

    public void SunnyWeather()
    {
        AssignWeatherType(_weatherTypes.Find(x => x.name == "SunnyWeather"));
        UpdateSkybox(0);
    }

    public void RainyWeather()
    {
        AssignWeatherType(_weatherTypes.Find(x => x.name == "RainyWeather"));
        UpdateSkybox(2); 
    }

    public void SnowyWeather()
    {
        AssignWeatherType(_weatherTypes.Find(x => x.name == "SnowyWeather"));
        UpdateSkybox(1); 
    }

    private void UpdateSkybox(int index)
    {
        if (newSkyboxMaterial != null && index < newSkyboxMaterial.Length && newSkyboxMaterial[index] != null)
        {
            RenderSettings.skybox = newSkyboxMaterial[index];
        }
    }

    private void ApplyWind()
    {
        _playerController.rb.AddForce(direction * windSpeed, ForceMode.Force);
    }
}