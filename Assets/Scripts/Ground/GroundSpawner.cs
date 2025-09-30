using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GroundSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _groundTile;
    [SerializeField] private GameObject _containerGround;
    private Vector3 _nextSpawnPoint;


    public void SpawnTile()
    {
        int randomIndex = Random.Range(0, _groundTile.Length);
        GameObject spawned = Instantiate(_groundTile[randomIndex], _containerGround.transform);
        spawned.GetComponent<GroundTile>().TileDead += AddTile;
        spawned.transform.position = _nextSpawnPoint;
        _nextSpawnPoint = spawned.transform.GetChild(1).transform.position;
    }

    private void Start()
    {
        for (int i =  0; i < 5; i++)
        {
                SpawnTile(); 
        }
    }

    private void AddTile()
    {
         SpawnTile();
    }
}
