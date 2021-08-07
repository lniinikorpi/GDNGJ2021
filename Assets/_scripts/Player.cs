using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Camera mainCamera;
    public Transform mainObject;
    public GameObject projectile;
    public float projectileSpeed = 10;
    public GameObject hitParticle;
    private AudioSource _audioSource;

    private bool isShooting = false;
    private Vector2 movement = new Vector2();
    public float speed = 10;
    public float weaponDelay = 0.1f;
    private float nextShoot = 0;
    Rigidbody2D rigid;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        rigid = mainObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isShooting && Time.time >= nextShoot)
        {
            nextShoot = Time.time + weaponDelay;
            GameObject p = Instantiate(projectile, mainObject.position, new Quaternion());
            Projectile proj = p.GetComponentInChildren<Projectile>();
            proj.dir = transform.up;
            proj.speed = projectileSpeed;
        }
    }

    private void FixedUpdate()
    {
        Vector2 move = movement * speed * Time.deltaTime;
        // rigid.velocity = move;
        mainObject.position += new Vector3(move.x, move.y, 0);
    }

    public void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    public void OnMouseMove(InputValue value)
    {
        Vector2 v = value.Get<Vector2>();
        Vector3 pos = mainCamera.WorldToScreenPoint(transform.position);
        Vector2 dir = v - (Vector2)pos;
        dir = dir.normalized;

        float angle = Vector2.SignedAngle(Vector2.up, dir);
        Vector3 dirVec = new Vector3(0, 0, angle);
        transform.localEulerAngles = dirVec;
    }

    public void OnShoot(InputValue value)
    {
        float v = value.Get<float>();
        isShooting = v == 1;
    }

    public void TakeHit(int value)
    {
        Instantiate(hitParticle, transform.position, Quaternion.identity);
        _audioSource.Play();
    }
}
