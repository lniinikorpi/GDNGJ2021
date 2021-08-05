using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform parent;
    public Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1));
        parent.Translate(dir);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Enemy enemy = collider.gameObject.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.OnKill();
            Destroy(gameObject);
        }
    }
}
