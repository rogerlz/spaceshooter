using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _playerSpeed = 8f;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private int _playerLives = 3;

    [SerializeField]
    private float _fireRate = 0.15f;

    [SerializeField]
    private int _playerScore;

    private float _nextFire;

    private SpawnManager _spawnManager;
    private UIManager _uiManager;


// Start is called before the first frame update
void Start()
    {
        transform.position = new Vector3(0, 0, 0);

        _spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("SpawnManager is null");
        }

        if (_uiManager == null)
        {
            Debug.LogError("UI Manager is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire)
        {
            FireLaser();
        }
    }

    void FireLaser()
    {
        _nextFire = Time.time + _fireRate;
        Instantiate(_laserPrefab, new Vector3(transform.position.x, transform.position.y + 1.05f, transform.position.z), Quaternion.identity);
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _playerSpeed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }


    public void Damage()
    {
        _playerLives -= 1;

        _uiManager.UpdateLives(_playerLives);

        if (_playerLives < 1)
        {
            Destroy(this.gameObject);
            _spawnManager.OnPlayerDeath();
        }
    }

    public void IncreaseScore(int scoreValue)
    {
        _playerScore += scoreValue;
        _uiManager.UpdateScore(_playerScore);
    }

}
