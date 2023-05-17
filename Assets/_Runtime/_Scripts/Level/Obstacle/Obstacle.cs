using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, IPlayerCollisionReact
{
    [SerializeField] private DecorationSpawner[] decorationSpawners;

    private List<ObstacleDecoration> obstacleDecorations = new List<ObstacleDecoration>();

    public void SpawnDecorations()
    {
        foreach (var decorationSpawner in decorationSpawners)
        {
            decorationSpawner.SpawnDecorations();
            ObstacleDecoration decoration = decorationSpawner.CurrentDecoration.GetComponent<ObstacleDecoration>();
            if(decoration != null) obstacleDecorations.Add(decoration);
        }
    }

    public virtual void PlayCollisionFeedBack(Collider collider)
    {
        ObstacleDecoration obstacleDecoration = FindDecorationForCollider(collider);
        obstacleDecoration.PlayCollisionFeedBack();

    }

    private ObstacleDecoration FindDecorationForCollider(Collider collider)
    {
        float mindDistX = Mathf.Infinity;
        ObstacleDecoration minDistDecoration = null;
        
        foreach (ObstacleDecoration decoration in obstacleDecorations)
        {
            float decorationPosX = decoration.transform.position.x;
            float colliderPosX = collider.bounds.center.x;
            float distX = Mathf.Abs(decorationPosX - colliderPosX);

            if(distX < mindDistX)
            {
                mindDistX = distX;
                minDistDecoration = decoration;
            }
        }

        return minDistDecoration;
    }

    public void ReactPlayerCollision(in PlayerCollisionInfo playerCollisionInfo)
    {
        playerCollisionInfo.PlayerControl.Die();
        playerCollisionInfo.playerAC.Die();
        playerCollisionInfo.gameMode.OnGameOver();
        PlayCollisionFeedBack(playerCollisionInfo.colliderFromReaction);
    }

}
