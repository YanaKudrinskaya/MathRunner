using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class GroundTile : MonoBehaviour
{
    [SerializeField] private GameObject[] _extractionPrefabs;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private GameObject _container;

    private int[] _position = new int[3] {-4, 0, 4};

    public Action TileDead;

    public void SpawnTile(int point)
    {
        int randomIndex = Random.Range(0, _extractionPrefabs.Length);
        int positionIndex = Random.Range(0, _position.Length);
        GameObject spawned = Instantiate(_extractionPrefabs[randomIndex], _container.transform);
        spawned.transform.position = _spawnPoints[point].position + new Vector3(_position[positionIndex], 0,0);
    }

    private void Start()
    {
        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            SpawnTile(i);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        TileDead?.Invoke();
        Destroy(gameObject, 5f);
    }


}
