using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteAlways]
public class ChasePlayer : MonoBehaviour
{
    
    
    [SerializeField] private PlayerController player;
    [SerializeField] private float armZ;
    [SerializeField] private float armY;

     private void LateUpdate()
     {
        Vector3 armOffSet = new Vector3(0, armY, armZ);
        Vector3 followPlayerZ = new Vector3();
        followPlayerZ.z = player.transform.position.z;


        transform.position = followPlayerZ + armOffSet;
        
     }

}
