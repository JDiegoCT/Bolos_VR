using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBall : MonoBehaviour
{
  
    public GameObject prefabBall;
    public Transform ballSpawn;


    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
        Instantiate(prefabBall, ballSpawn.position, ballSpawn.rotation);
    }




}
