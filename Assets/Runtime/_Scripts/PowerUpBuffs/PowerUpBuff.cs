using System.Collections;
using UnityEngine;

public abstract class PowerUpBuff : MonoBehaviour
{
    [SerializeField] protected GameMode gameMode;
    [SerializeField] protected PlayerController playerControl;

    [Space]

    [Header("PowerUp Params")]

    [Space]

    [SerializeField] protected GameObject ParticlePowerUp;

    private bool wasActived = false;

    private float endTime = 0;

    private GameObject activePowerUpParticle;

    public bool IsPowerUpActive => Time.time < endTime;

    public void ActiveForDuration(float duration)
    {
        wasActived = IsPowerUpActive;
        endTime = Time.time + duration;
        if(!wasActived)
        {
            StartCoroutine(UpdateBehaviourCoroutine());
        }

    }

    private IEnumerator UpdateBehaviourCoroutine()
    {
        StartBehaviour();
        activePowerUpParticle = Instantiate(ParticlePowerUp, playerControl.transform);

        while(IsPowerUpActive)
        {
            UpdateBehaviour();
            yield return null;
        }

        Destroy(activePowerUpParticle);
        EndBehaviour();
    }

    protected abstract void StartBehaviour();
    protected abstract void UpdateBehaviour();
    protected abstract void EndBehaviour();

}
