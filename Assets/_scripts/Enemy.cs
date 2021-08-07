using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float speed = 2;
    public int distance;
    public GameObject hitParticle;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < distance)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.position, step);
        }
    }

    public void OnKill(Vector3 bulletRot)
    {
        SpawnHitParticle(bulletRot);
        audioSource.Play();
        Destroy(gameObject);
    }

    void SpawnHitParticle(Vector3 rotation)
    {
        GameObject obj = Instantiate(hitParticle, transform.position, Quaternion.identity);
        float angle = Vector2.SignedAngle(Vector2.up, rotation);
        obj.transform.localEulerAngles = new Vector3(0, 0, angle);
    }
}
