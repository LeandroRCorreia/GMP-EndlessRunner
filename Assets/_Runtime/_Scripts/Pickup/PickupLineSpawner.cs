using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickupLineSpawner : MonoBehaviour
{
    [SerializeField] private GameObject pickupPrefab;
    [SerializeField] private float spaceBetweenPickups = 1.25f; 
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;


    public void SpawnPickupLine(Vector3[] skipPositions)
    {
        Vector3 currentSpawnPosition = start.position;

        if(spaceBetweenPickups <= 0) spaceBetweenPickups = 1;

        while(currentSpawnPosition.z < end.position.z)
        {
            if(!ShouldSkipPosition(currentSpawnPosition, skipPositions)) 
            {
                Instantiate(pickupPrefab, currentSpawnPosition, Quaternion.identity, transform);
            }
            currentSpawnPosition.z += spaceBetweenPickups;

        }

    }

    private bool ShouldSkipPosition(Vector3 currentSpawnPosition, Vector3[] skipPositions)
    {
        foreach (var skipPosition in skipPositions)
        {
            float skipStart = skipPosition.z - spaceBetweenPickups * 0.5f;
            float skipEnd = skipPosition.z + spaceBetweenPickups * 0.5f;   

            if(currentSpawnPosition.z >= skipStart && currentSpawnPosition.z <= skipEnd)
            {
                return true;
            }

        }

        return false;
    }

    private void OnDrawGizmos() 
    {
        Vector3 currentSpawnPosition = start.position;
        Gizmos.color = Color.green;

        if(spaceBetweenPickups <= 0) spaceBetweenPickups = 1;

        while(currentSpawnPosition.z < end.position.z)
        {
            Gizmos.DrawCube(currentSpawnPosition, Vector3.one);
            currentSpawnPosition.z += spaceBetweenPickups;
        }

    }

    // public void SpawnCherries(TrackSegment trackInstance)
    // {
    //     currentTrack = trackInstance;
    //     ProcessSpawn(GetRandomTrackLaneCoordinatesX(currentTrack));
    // }

    // private float GetRandomTrackLaneCoordinatesX(TrackSegment trackSegment)
    // {
    //     return trackSegment.CoordinatesLane[Random.Range(0, trackSegment.CoordinatesLane.Length)];
    // }

    // private float MaxFruitsInTrack()
    // {
    //     return Mathf.Floor(currentTrack.Length / spacementBetweenFruitZ);
    // }

    // private float GetRandomQuantityFruitInTrack(float MaxFruitSpawn)
    // {
    //     return Mathf.Floor(Random.Range(3, MaxFruitSpawn));
    // }

    // private void ProcessSpawn(float laneSpawn)
    // {
    //     float maxFruitSpawn = MaxFruitsInTrack();
    //     float numberFruitSpawn = GetRandomQuantityFruitInTrack(maxFruitSpawn);
    //     float firstSpacement = spacementBetweenFruitZ * 0.5f;
    //     Vector3 frontCoordinate = new Vector3(laneSpawn, 0, transform.position.z + firstSpacement);
    //     Vector3 backCoordinate = new Vector3(laneSpawn, 0, transform.position.z - firstSpacement);
    //     int i = 0;
    //     SpawnFirstPairFruit(frontCoordinate, backCoordinate);
    //     i+=2;

    //     for (i = 0; i < numberFruitSpawn ; i += 2)
    //     {
    //         SpawnPairFruit(ref frontCoordinate, ref backCoordinate);
    //     }

    // }

    // private void SpawnFirstPairFruit(Vector3 frontCoordinate, Vector3 backCoordinate)
    // {
    //     var cherrieFront = Instantiate(pickupPrefab, transform);
    //     var cherrieBack = Instantiate(pickupPrefab, transform);
    //     cherrieFront.transform.position = frontCoordinate;
    //     cherrieBack.transform.position = backCoordinate;
    // }

    // private void SpawnPairFruit(ref Vector3 vFrontCoordinate, ref Vector3 vBackCoordinate)
    // {
    //     vFrontCoordinate.z += spacementBetweenFruitZ;
    //     vBackCoordinate.z -= spacementBetweenFruitZ;
    //     var cherrieFront = Instantiate(pickupPrefab, transform);
    //     var cherrieBack = Instantiate(pickupPrefab, transform);
    //     cherrieFront.transform.position = vFrontCoordinate;
    //     cherrieBack.transform.position = vBackCoordinate;
    // }


}
