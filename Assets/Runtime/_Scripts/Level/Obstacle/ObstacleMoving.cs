using Unity.VisualScripting;
using UnityEngine;

public class ObstacleMoving : Obstacle
{
    //TODO:
    [SerializeField] private float laneDistanceX = 4;
    //
    [SerializeField] private float  initialSpeed = 10f;

    private float positionT = 0;
    public float MoveSpeed => initialSpeed;
    public float sideToSideMoveTime => 1.0f / MoveSpeed;

    private void FixedUpdate() 
    {
        positionT += MoveSpeed * Time.deltaTime;
        float positionX = (Mathf.PingPong(positionT, 1) - 0.5f) * laneDistanceX * 2;

        Vector3 pos = transform.position;
        pos.x = positionX;
        transform.position = pos;

    }

    public override void PlayCollisionFeedBack(Collider collider)
    {
        base.PlayCollisionFeedBack(collider);
        enabled = false;
    }

}
