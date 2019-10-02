using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private float _enemySpeed = 4.0f;

    private Player _player;

    void Start()
    {
        transform.position = new Vector3(0, -8f);


        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        if (_player == null)
        {
            Debug.LogError("Player is null");
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * _enemySpeed * Time.deltaTime);

        if (transform.position.y <= -5f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (_player != null)
            {
                _player.Damage();
            }

            Destroy(this.gameObject);
        }

        if (other.gameObject.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);

            if (_player != null)
            {
                _player.IncreaseScore(10);
            }
        }

    }
}
