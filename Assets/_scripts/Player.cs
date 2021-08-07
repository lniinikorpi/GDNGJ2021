using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Camera mainCamera;
    public Transform mainObject;
    public GameObject projectile;
    public GameObject hitParticle;
    private AudioSource _audioSource;

    Vector2 movement = new Vector2();
    public float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D rigid = mainObject.GetComponent<Rigidbody2D>();
        if (movement != new Vector2())
        {
            Vector2 move = movement * speed * Time.deltaTime;
            rigid.MovePosition(rigid.position + move);
            // mainObject.position += new Vector3(movement.x, movement.y, 0) * speed * Time.deltaTime;
        }
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

    public void OnShoot()
    {
        GameObject p = Instantiate(projectile, mainObject.position, new Quaternion());
        Projectile proj = p.GetComponentInChildren<Projectile>();
        proj.dir = transform.up;
        proj.speed = 10;
    }

    public void TakeHit(int value)
    {
        Instantiate(hitParticle, transform.position, Quaternion.identity);
        _audioSource.Play();
    }
}
