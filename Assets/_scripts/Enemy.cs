using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public int movementDelta;
    public int distance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < distance)
        {
            transform.position += (player.position - transform.position) / movementDelta;
        }
    }

    public void OnKill()
    {
        Destroy(gameObject);
    }
}
