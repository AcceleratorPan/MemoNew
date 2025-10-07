using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GreenControl : MonoBehaviour
{
    public int Hp = 1;
    public float speed = 1f;
    private int dir = 1;
    private Rigidbody2D rBody;
    private Animator ani;

    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        GameManager.Instance.RegisterEnemy();
    }


    void Update()
    {
        if (Hp <= 0)
        {
            return;
        }
        transform.Translate(Vector2.left * dir * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "arrow")
        {
            Destroy(collision.gameObject);
            AudioManager.Instance.PlaySound("1125");
            Hp--;
            GameManager.Instance.EnemyDestroyed(100);
            ani.SetTrigger("Dead");
            Destroy(GetComponent<BoxCollider2D>());
            rBody.AddForce(new Vector2(collision.GetComponent<Rigidbody2D>().velocity.x * 10f, 60));
            rBody.gravityScale = 1;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.name);
        if (collision.collider.tag == "wall")
        {
            Flip();
        }
    }

    private void Flip()
    {
        dir = -dir;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
        // rBody.AddForce(new Vector2(dir * 0.1f, 0), ForceMode2D.Impulse);
    }

    public void DestroyAfterAnimation()
    {
        Destroy(gameObject);
    } 
}
