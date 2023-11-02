using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private float _speed = 3.5f;
    private float _speedMultipiler = 2;
    [SerializeField] private  GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleLaserPrefab;
    [SerializeField] private float _fireRate = .5f;
    private float _canFire = -1f;
    [SerializeField] private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField] private bool _isTripleShotActive;
    [SerializeField] private bool _isSpeedBoostActive=false;
    [SerializeField] private bool _isSheildActive = false;
    [SerializeField] private GameObject _sheildVisualizer;
    [SerializeField] private int _socre;
    private UIManager _uiManager;
    
    void Start()
    {
        transform.position = new Vector3(0,0,0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager >();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL.");
        }

        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL");
        }
    }

    void Update()
    {
       CalculateMovement();
       if (Input.GetKeyDown(KeyCode.Space) && Time.time >_canFire)
       {
           FireLaser();
       }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * (_speed * Time.deltaTime));

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);
        
        if (transform.position.x> 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x <-11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
            _canFire = Time.time + _fireRate;
            if (_isTripleShotActive == true)
            {
                Instantiate(_tripleLaserPrefab, transform.position + new Vector3(1,1.05f,0), Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab,transform.position + new Vector3(0,1.05f,0),Quaternion.identity);
            }
    }

    public void Damage()
    {
        if (_isSheildActive)
        {
            _isSheildActive = false;
            _sheildVisualizer.SetActive(false);
            return;
        }
        _lives -= 1;
        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(gameObject);
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    public void SpeedBostActive()
    {
        _isSpeedBoostActive = true;
        _speed *= _speedMultipiler;
        StartCoroutine(SpeedBostPowerDownRoutine());
    }

    IEnumerator SpeedBostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
        _speed /= _speedMultipiler;
    }

    public void SheildsActive()
    {
        _isSheildActive = true;
        _sheildVisualizer.SetActive(true);
    }

    public void AddScore(int points)
    {
        _socre += points;
        _uiManager.UpdateScore(_socre);
    }
}
