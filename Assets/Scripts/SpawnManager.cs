using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private IEnumerator coroutine;

    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private GameObject _enemyContainer;

    private bool _stopSpawning;

    IEnumerator SpawnEnemies()
    {
        while (_stopSpawning == false)
        {
            yield return null; // wait 1 frame

            float randomX = Random.Range(-8f, 8f);
            GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(randomX, 7f, 0), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(Random.Range(3f, 5f));
        }
    }

    void Start()
    {
        _stopSpawning = false;

        coroutine = SpawnEnemies();
        StartCoroutine(coroutine);

    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;

        GameObject[] _enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (var i = 0; i < _enemies.Length; i++)
        {
            Destroy(_enemies[i]);
        }
    }
}
