using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class TrackSegment : MonoBehaviour
{
    [SerializeField] private Transform startZ;
    [SerializeField] private Transform endZ;
    [SerializeField] private ObstacleSpawner[] obstacleSpawners;
    [SerializeField] private DecorationSpawner decorationSpawner;

    [Header("Pickup")]
    [Range(0, 1)]
    [SerializeField] private float pickupSpawnChance = 0.25f;
    [SerializeField] private PickupLineSpawner[] pickupLineSpawner;


    public Transform StartZ => startZ;
    public Transform EndZ => endZ;
    public float Length => Vector3.Distance(EndZ.position, StartZ.position);
    public float SqrLength => (EndZ.position - StartZ.position).sqrMagnitude;
    public ObstacleSpawner[] ObstacleSpawners => obstacleSpawners;
    public DecorationSpawner DecorationSpawner => decorationSpawner;

    public void SpawnObstacles()
    {
        foreach (var obstacleSpawner in ObstacleSpawners)
        {
            obstacleSpawner.SpawnObstacle();
        }
    }

    public void SpawnDecorations()
    {
        if(DecorationSpawner == null) return;
        DecorationSpawner.SpawnDecorations();
    }

    public void SpawnPickups()
    {

        if(pickupLineSpawner.Length > 0 && Random.value <= pickupSpawnChance)
        {
            Vector3[] skipPositions = new Vector3[obstacleSpawners.Length];
            for(int i = 0; i < skipPositions.Length; i++)
            {
                skipPositions[i] = ObstacleSpawners[i].transform.position;
            }

            int index = Random.Range(0, pickupLineSpawner.Length);
            PickupLineSpawner pickup = pickupLineSpawner[index];
            pickup.SpawnPickups(skipPositions);
        }
    }

}
