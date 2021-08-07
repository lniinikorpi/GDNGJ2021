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
    public AudioSource attackSource;
    public AudioSource footStepSource;
    public Transform bulletSpawn;
    public Animator animator;

    private bool isShooting = false;
    private Vector2 movement = new Vector2();
    public float speed = 10;
    public float maxHealth = 10;
    private float _currentHealth;
    public float weaponDelay = 0.1f;
    private float nextShoot = 0;
    private float eFrames = 0;
    Rigidbody2D rigid;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        rigid = mainObject.GetComponent<Rigidbody2D>();
        _currentHealth = maxHealth;
        UIManagerGame.instance.UpdateHealthSlider(_currentHealth / maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (UIManagerGame.instance.isPaused)
            return;

        if (isShooting && Time.time >= nextShoot)
        {
            nextShoot = Time.time + weaponDelay;
            GameObject p = Instantiate(projectile, bulletSpawn.position, new Quaternion());
            attackSource.Play();
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
        if (movement != Vector2.zero)
        {
            animator.SetBool("walking", true);
        }
        else
        {
            animator.SetBool("walking", false);
        }
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
        _currentHealth -= value;
        UIManagerGame.instance.UpdateHealthSlider(_currentHealth/maxHealth);
        if (Time.time < eFrames)
        {
            return;
        }
        eFrames = Time.time + 1;
        Instantiate(hitParticle, transform.position, Quaternion.identity);
        _audioSource.Play();
    }

    public void PlayFootsep()
    {
        footStepSource.Play();
    }

    public void OnPause()
    {
        UIManagerGame.instance.PauseGame();
    }
}
