using UnityEngine;

public class PowerUpBuffController : MonoBehaviour
{
    [SerializeField] private PowerUpBuffMultiplier powerUpBuffMultiplier;
    [SerializeField] private PowerUpBuffInvincible powerUpBuffInvincible;
    [SerializeField] private PowerUpBuffMagnet powerUpBuffMagnet;

    [SerializeField] private GameMode gameMode;
    [SerializeField] private PlayerController playerController;

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            ActivePowerUpMagnet();
        }    
    }


    public void ActivePowerUpMultiplier()
    {
        powerUpBuffMultiplier.ActiveForDuration(30);
    }

    public void ActivePowerUpInvincible()
    {
        powerUpBuffInvincible.ActiveForDuration(30);
    }

    public void ActivePowerUpMagnet()
    {
        powerUpBuffMagnet.ActiveForDuration(30);
    }

}
