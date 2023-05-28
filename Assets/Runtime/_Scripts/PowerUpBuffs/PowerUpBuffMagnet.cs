using System.Collections.Generic;
using UnityEngine;

public class PowerUpBuffMagnet : PowerUpBuff
{
    [SerializeField] private float addSpeedMagnet = 5f;
    [SerializeField] private float speedReduceScale = 15f;

    [SerializeField] private Vector3 magnetBoxSize = new Vector3(10, 8, 2);

    private List<Pickup> pickupsToAtract = new List<Pickup>();
    private Collider[] pickupsOnRange = new Collider[20];

    protected override void StartBehaviour()
    {
        pickupsToAtract.Clear();

    }

    protected override void UpdateBehaviour()
    {
        GetPickupsInRange();
        for (int pickupIndex = 0; pickupIndex < pickupsToAtract.Count; pickupIndex++)
        {
            ExecuteMagnetEffect(pickupsToAtract[pickupIndex]);
        }
    }

    private void GetPickupsInRange()
    {
        int lenght = Physics.OverlapBoxNonAlloc(playerControl.transform.position,
        magnetBoxSize,
        pickupsOnRange);

        for(int i = 0; i < lenght; i++)
        {
            Pickup pickup = pickupsOnRange[i].GetComponent<Pickup>();
            if(pickup != null &&
               !pickupsToAtract.Contains(pickup))
            {
                pickupsToAtract.Add(pickup);
            }
        }

    }

    private void ExecuteMagnetEffect(Pickup pickup)
    {
        if(pickup != null)
        {
            var finalSpeedMagnet = playerControl.ForwardSpeed + addSpeedMagnet;
            pickup.transform.position = Vector3.MoveTowards(pickup.transform.position, 
            playerControl.transform.position, finalSpeedMagnet * Time.deltaTime);

            pickup.transform.localScale = Vector3.Lerp(pickup.transform.localScale, Vector3.one * 0.5f, 
            speedReduceScale * Time.deltaTime);
        }

    }

    protected override void EndBehaviour()
    {
        pickupsToAtract.Clear();
        
    }
    
    private void OnDrawGizmos() 
    {
        Gizmos.DrawWireCube(playerControl.transform.position, magnetBoxSize);
    }

}
