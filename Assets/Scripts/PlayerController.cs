using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private int count;
    private int lives;

    public float speed;
    public float jumpForce;
    public Text livesText;
    public Text winText;
    public Text scoreText;
    public Text loseText;
    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    public AudioSource musicSource;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        lives = 3;
        winText.text = "";
        loseText.text = "";
        SetCountText();
        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;
    }

  
    void Update()
    {
        if (Input.GetKey("escape"))
            Application.Quit();
        

    }
    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal, 0);

        rb2d.AddForce(movement * speed);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        {
            if (Input.GetKey("escape"))
                Application.Quit();
        }
        if (other.gameObject.CompareTag ("Pickups"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
        if (count == 4)
        {
            transform.position = new Vector3(transform.position.x, 26.5f, transform.position.z);
            lives = 3;
            SetCountText();

        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            lives = lives - 1; // this removes 1 from the score
            SetCountText();
        }
        if (lives == 0)
        {
            Destroy(gameObject);
            Input.GetKey("escape");
            Application.Quit();
            

        }

         if (count == 8)
        {
            musicSource.Stop();
            musicSource.clip = musicClipTwo;
            musicSource.Play();
        }

    }

    void OnCollisionStay2D (Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.UpArrow))
            {
                rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }
    }

    void SetCountText()
    {
        livesText.text = "Lives: " + lives;
        scoreText.text = "Score: " + count;
        if (count == 8) 
        {
            winText.text = "You Win!";
     
        }
        if (lives <= 0)
        {
            loseText.text = "You Lose!";
        }
    }
}
