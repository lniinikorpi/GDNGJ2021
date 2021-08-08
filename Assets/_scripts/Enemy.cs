using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform player;
    public float speed = 2;
    public int hp = 3;
    public int distance;
    public GameObject hitParticle;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < distance)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.position, step);
            Vector3 dir = player.position - transform.position;
            dir = dir.normalized;

            float angle = Vector2.SignedAngle(Vector2.up, dir);
            Vector3 dirVec = new Vector3(0, 0, angle);
            transform.localEulerAngles = dirVec;
        }
    }

    public void OnHit(Vector3 bulletRot)
    {
        SpawnHitParticle(bulletRot);
        audioSource.Play();
        hp -= 1;
        if (hp <= 0)
        {
            EnemySpawner.instance.enemySpawns.Add(transform.position);
            Destroy(gameObject);
        }
        else
        {
            // Knockback
            transform.position += bulletRot.normalized / 5;
        }
    }

    void SpawnHitParticle(Vector3 rotation)
    {
        GameObject obj = Instantiate(hitParticle, transform.position, Quaternion.identity);
        float angle = Vector2.SignedAngle(Vector2.up, rotation);
        obj.transform.localEulerAngles = new Vector3(0, 0, angle);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponentInChildren<Player>();
        if (player)
        {
            player.TakeHit(1);
        }
    }

}
