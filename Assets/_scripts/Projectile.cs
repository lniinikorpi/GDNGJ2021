using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform parent;
    public Vector3 dir;
    public float speed;
    float timeAlive;
    // Start is called before the first frame update
    void Start()
    {
        timeAlive = Time.time + 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= timeAlive)
        {
            Destroy(parent.gameObject);
            return;
        }
        transform.Rotate(new Vector3(0, 0, 1));
        parent.Translate(dir * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Enemy enemy = collider.gameObject.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.OnHit(dir);
            Destroy(parent.gameObject);
        }
        else
        {
            Player player = collider.gameObject.GetComponentInChildren<Player>();
            Projectile proj = collider.gameObject.GetComponent<Projectile>();
            if (!player && !proj)
            {
                Destroy(parent.gameObject);
            }
        }
    }
}
