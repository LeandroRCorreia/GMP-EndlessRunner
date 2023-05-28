using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerAnimationController))]
public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private GameMode gameMode;
    [SerializeField] private PowerUpBuffController powerUpBuffControl;
    private PlayerController playerController;
    private PlayerAnimationController animationController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        animationController = GetComponent<PlayerAnimationController>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<IPlayerCollisionReact>(out IPlayerCollisionReact playerCollisionInterface))
        {
            PlayerCollisionInfo data = new PlayerCollisionInfo(playerController,
            animationController,
            other,
            gameMode, 
            powerUpBuffControl);
            playerCollisionInterface.ReactPlayerCollision(in data);
            
        }

    }
    
}
