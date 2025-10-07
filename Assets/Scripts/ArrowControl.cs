using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ArrowControl : MonoBehaviour
{
    private float timer;
    private bool inWall = false;
    public float initialWaitTime = 3f;
    public float blinkDuration = 2f;
    public float startBlinkInterval = 0.5f;
    public float endBlinkInterval = 0.05f;
    private SpriteRenderer arrowRenderer;
    private BoxCollider2D bc;
    private BoxCollider2D bcTrigger;
    
    void Start()
    {
        foreach (BoxCollider2D t in GetComponents<BoxCollider2D>())
        {
            if (t.isTrigger)
            {
                bcTrigger = t;
            }
            else
            {
                bc = t;
            }
        }
        bc.enabled = false;
        bcTrigger.enabled = true;
        arrowRenderer = GetComponent<SpriteRenderer>();
        gameObject.SetActive(false);
        Invoke("ArrowMove", 0.3f);
    }

    private void ArrowMove()
    {
        timer = 0f;
        AudioManager.Instance.PlaySound("1115");
        transform.localScale = GameObject.Find("player").transform.localScale;
        transform.position = GameObject.Find("shooter").transform.position;
        gameObject.SetActive(true);
        GetComponent<Rigidbody2D>().AddForce(Vector2.left * transform.localScale * 30f);

    }

    void Update()
    {
        timer += Time.deltaTime;
        if (!inWall)
        {
            if (timer > 2f)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ground")
        {
            Destroy(gameObject);
        }
        else if (collision.tag == "wall")
        {
            AudioManager.Instance.PlaySound("1117");
            gameObject.layer = 10;
            inWall = true;
            timer = 0;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Destroy(GetComponent<Rigidbody2D>());
            Destroy(bcTrigger);
            bc.enabled = true;
            
            StartCoroutine(Blink());
        }
    }

    private IEnumerator Blink()
    {
        yield return new WaitForSeconds(3);
        float blinkStartTime = Time.time;
        float currentBlinkInterval;

        while (Time.time < blinkStartTime + blinkDuration)
        {
            float progress = (Time.time - blinkStartTime) / blinkDuration;
            currentBlinkInterval = Mathf.Lerp(startBlinkInterval, endBlinkInterval, progress);

            arrowRenderer.enabled = false;
            yield return new WaitForSeconds(currentBlinkInterval / 2f);
            arrowRenderer.enabled = true;
            yield return new WaitForSeconds(currentBlinkInterval / 2f);
        }
        Destroy(gameObject);
    }
}
