using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementScript : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float boostTime;
    [SerializeField] private float boostIncrease;
    private bool isBoostActivated = false;
    Rigidbody2D rb;

    void Start()
    {
            rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float movX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(movX * speed, rb.velocity.y);
        if (isBoostActivated)
        {
            rb.velocity = new Vector2(movX * (speed + boostIncrease), rb.velocity.y);
        }

        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Approximately(rb.velocity.y,0))
        {
            rb.AddForce(transform.up * jumpForce);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Coin")
        {
            Debug.Log("Trigger");
            ScoreManager.instance.AddScore();
            Destroy(other.gameObject);
            if (!isBoostActivated)
            {
                Debug.Log("BOOST");
                isBoostActivated = true;
                Invoke("EndBoost", boostTime);
            }
        }

        if (other.tag == "Enemy")
        {
            Debug.Log("Enemy");
            ScoreManager.instance.MinusScore();
        }
    }
    private void EndBoost()
    {
        isBoostActivated = false;
    }
}
