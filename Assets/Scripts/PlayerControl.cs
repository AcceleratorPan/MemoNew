using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Animator ani;
    private Rigidbody2D rBody;
    private Collider2D mycollider;
    private SpriteRenderer playerRenderer;
    private bool onGround;
    public GameObject arrowPre;
    private float timer = 0;
    public int Hp = 3;
    private int originalLayer;
    private bool isInvincible = false;
    public bool die = false;
    private float speed = 1.5f;
    public float invincibilityDuration = 2f;
    public float blinkInterval = 0.15f;

    public float screenLeft;
    public float screenRight;
    public float screenTop;
    public float screenBottom;

    public float buffer = 0.5f;

    void Awake()
    {

    }
    void Start()
    {
        originalLayer = gameObject.layer;
        ani = GetComponent<Animator>();
        rBody = GetComponent<Rigidbody2D>();
        mycollider = GetComponent<Collider2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (GameManager.Instance != null && (GameManager.Instance.IsPaused || GameManager.Instance.isGameOver))
        {
            return;
        }
        if (Hp <= 0)
        {
            ani.SetTrigger("Dead");
            Destroy(mycollider);
            die = true;
            return;
        }
        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal != 0)
        {
            ani.SetBool("isRun", true);
            transform.localScale = new Vector3(horizontal > 0 ? -1 : 1, 1, 1);
            transform.Translate(Vector2.right * horizontal * speed * Time.deltaTime);
        }
        else
        {
            ani.SetBool("isRun", false);
        }

        if (Input.GetKeyDown(KeyCode.W) && onGround)
        {
            AudioManager.Instance.PlaySound("1113");
            rBody.AddForce(Vector2.up * 230);
        }

        if (!onGround)
        {
            ani.SetFloat("Vertical", rBody.velocity.y > 0 ? 1 : -1);
        }
        timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.F) && timer > 0.5f)
        {
            timer = 0;
            Shoot();
        }
    }

    private void Shoot()
    {
        ani.SetTrigger("Shot");
        GameObject.Instantiate(arrowPre, transform.position, transform.rotation);
    }

    void LateUpdate()
    {
        WrapAround();
    }

    private IEnumerator BecomeInvincible()
    {
        isInvincible = true;
        gameObject.layer = 8;
        float invincibilityTimer = 0f;
        while (invincibilityTimer < invincibilityDuration)
        {
            playerRenderer.enabled = !playerRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
            invincibilityTimer += blinkInterval;
        }
        playerRenderer.enabled = true;
        gameObject.layer = originalLayer;
        isInvincible = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.collider.tag == "ground" || collision.collider.tag == "arrow") && rBody.velocity.y <= 0)
        {
            onGround = true;
            ani.SetBool("isFly", false);
        }
        if (isInvincible)
        {
            return;
        }
        else if (collision.collider.tag == "enemy")
        {
            Hp--;
            BloodControl.Instance.BloodChanged(Hp);
            rBody.AddForce(Vector2.up * 200);
            if (Hp > 0)
            {
                StartCoroutine(BecomeInvincible());
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "ground" || collision.collider.tag == "arrow")
        {
            onGround = false;
            ani.SetBool("isFly", true);
        }
    }

    private void WrapAround()
    {
        Vector3 newPosition = transform.position;

        if (newPosition.x > screenRight)
        {
            newPosition.x = screenLeft;
        }
        else if (newPosition.x < screenLeft)
        {
            newPosition.x = screenRight;
        }

        if (newPosition.y > screenTop)
        {
            newPosition.y = screenBottom;
        }
        else if (newPosition.y < screenBottom)
        {
            if (die)
            {
                gameObject.SetActive(false);
                GameManager.Instance.PlayerDied();
            }
            newPosition.y = screenTop;
        }

        transform.position = newPosition;
    }

}
