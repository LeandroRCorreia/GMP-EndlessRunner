using UnityEngine;

public struct PlayerCollisionInfo
{
    public GameMode gameMode;
    public PlayerController PlayerControl;
    public PlayerAnimationController playerAC;
    public Collider colliderFromReaction;
    public PowerUpBuffController powerUpBuffControl;

    public PlayerCollisionInfo(PlayerController playerControl, PlayerAnimationController playerAC,
    Collider colliderFromReaction, GameMode gameMode, PowerUpBuffController powerUpBuffControl)
    {
        this.gameMode = gameMode;
        this.PlayerControl = playerControl;
        this.playerAC = playerAC;
        this.colliderFromReaction = colliderFromReaction;
        this.powerUpBuffControl = powerUpBuffControl;
    }

}

public interface IPlayerCollisionReact
{
    void ReactPlayerCollision(in PlayerCollisionInfo playerCollisionInfo);

}
