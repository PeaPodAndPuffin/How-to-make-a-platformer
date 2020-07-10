using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed;
    Rigidbody2D rb;
    bool facingRight = true;

    bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public float jumpForce;

    bool isTouchingFront;
    public Transform frontCheck;
    bool wallJumping;
    public float wallJumpTime;
    public float xWallForce;
    public float yWallForce;
    bool wallSliding;
    public float wallSlidingSpeed;

    Animator anim;

    public int health;

    public float timeBetweenAttacks;
    float nextAttackTime;

    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayer;

    public int damage;

    public SpriteRenderer weaponRenderer;

    public GameObject blood;
    public GameObject deathEffect;
    public GameObject pickupEffect;

    AudioSource source;

    public AudioClip jumpSound;
    public AudioClip hurtSound;
    public AudioClip pickupSound;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {

        if (Time.time > nextAttackTime)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                
                anim.SetTrigger("attack");
                nextAttackTime = Time.time + timeBetweenAttacks;
            }
        }


        float input = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(input * speed, rb.velocity.y);

        if (input > 0 && facingRight == false)
        {
            Flip();
        } else if (input < 0 && facingRight == true) {
            Flip();
        }

        if (input != 0)
        {
            anim.SetBool("isRunning", true);
        } else {
            anim.SetBool("isRunning", false);
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsGround);

        if (isGrounded == true)
        {
            anim.SetBool("isJumping", false);
        } else {
            anim.SetBool("isJumping", true);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
            source.clip = jumpSound;
            source.Play();
        }


        if (isTouchingFront && !isGrounded && input != 0)
        {
            wallSliding = true;
        } else {
            wallSliding = false;
        }

        if (wallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && wallSliding)
        {
            wallJumping = true;
            Invoke("SetWallJumpingToFalse", wallJumpTime);
        }

        if (wallJumping)
        {
            rb.velocity = new Vector2(xWallForce * -input, yWallForce);
            source.clip = jumpSound;
            source.Play();
        }


    }

    void Flip() {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        facingRight = !facingRight;
    }

    void SetWallJumpingToFalse() {
        wallJumping = false;
    }

    public void TakeDamage(int damage) {
        source.clip = hurtSound;
        source.Play();
        FindObjectOfType<CameraShake>().Shake();
        health -= damage;
        print(health);
        if (health <= 0)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        } else {
            Instantiate(blood, transform.position, Quaternion.identity);
        }
    }

    public void Attack() {
        FindObjectOfType<CameraShake>().Shake();
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D col in enemiesToDamage)
        {
            col.GetComponent<Enemy>().TakeDamage(damage);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void Equip(Weapon weapon) {
        source.clip = pickupSound;
        source.Play();
        damage = weapon.damage;
        attackRange = weapon.attackRange;
        weaponRenderer.sprite = weapon.GFX;
        Instantiate(pickupEffect, transform.position, Quaternion.identity);
        Destroy(weapon.gameObject);
    }

}
