using UnityEngine;

public class PickupLineSpawner : MonoBehaviour
{

    [Header("Pickups")]
    [SerializeField] private GameObject cherriePrefab;
    [SerializeField] private GameObject peanutPrefab;

    [Range(0, 1)]
    [SerializeField] private float peanutChance = 0.1f;

    [Space]

    [Header("PowerUps")]
    [SerializeField] private GameObject[] powerUpsOptions;

    [Range(0, 1)]
    [SerializeField] private float powerUpChance = 0.1f;

    [Space]

    [Header("Positioning")]
    [SerializeField] private float spaceBetweenPickups = 1.25f; 
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;

    public void SpawnPickups(Vector3[] skipPositions)
    {
        if(Random.value <= powerUpChance)
        {
            SpawnPowerUp();
        }
        else
        {
            SpawnPickupLine(skipPositions);
        }

    }

    private void SpawnPickupLine(Vector3[] skipPositions)
    {
        Vector3 currentSpawnPosition = start.position;

        if(spaceBetweenPickups <= 0) spaceBetweenPickups = 1;

        while(currentSpawnPosition.z < end.position.z)
        {
            if(!ShouldSkipPosition(currentSpawnPosition, skipPositions)) 
            {
                GameObject pickupInstance = Random.value < peanutChance ? peanutPrefab : cherriePrefab;
                Instantiate(pickupInstance, currentSpawnPosition, Quaternion.identity, transform);
            }
            currentSpawnPosition.z += spaceBetweenPickups;

        }

    }
    
    private void SpawnPowerUp()
    {
        var index = Random.Range(0, powerUpsOptions.Length);
        var middleLanePositonZ = (end.position.z - start.position.z) * 0.5f;
        var PowerUpposition = new Vector3(transform.position.x, transform.position.y, end.position.z);
        var powerUpInstance = Instantiate(powerUpsOptions[index], PowerUpposition, Quaternion.identity, transform);
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

}
