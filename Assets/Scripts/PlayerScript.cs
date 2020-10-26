using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    private int scoreValue = 0;
    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;
    public Text winText;
    public Text livesText;
    private int livesValue = 3;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;
    Animator anim;
    private bool facingRight = true;


    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        winText.text = "";
        livesText.text = livesValue.ToString();
        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);
        if (scoreValue == 8)
      {
          musicSource.clip = musicClipOne;
          musicSource.Stop();
          musicSource.loop = false;
      }
      if (scoreValue == 8)
      {
          musicSource.clip = musicClipTwo;
          musicSource.Play();
      }
      if (Input.GetKeyDown(KeyCode.W))
      {
          anim.SetInteger("State", 2);
      }
      if (Input.GetKeyDown(KeyCode.D))
      {
          anim.SetInteger("State", 1);
      }
      if (Input.GetKeyDown(KeyCode.A))
      {
          anim.SetInteger("State", 1);
      }
      if (facingRight == false && hozMovement > 0)
         {
            Flip();
         }
      else if (facingRight == true && hozMovement < 0)
         {
            Flip();
         }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
         if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            if (scoreValue >= 8)
            {
                winText.text = "You Win! Game by Shane Moore";
            }
        }
         if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            livesText.text = livesValue.ToString();
            Destroy(collision.collider.gameObject);
            if (livesValue <= 0)
            {
                winText.text = "Game Over! You Lose!";
                Destroy(this);
            }
        }
        if (scoreValue == 4)
        {
            transform.position = new Vector3(280.2f, 3.0f, 1.0f);
            livesValue = 3;
        }
    }
     private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.collider.tag == "Ground" && isOnGround)
                {
                    if (Input.GetKey(KeyCode.W))
                        {
                            rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
                        }
                }
        }
     void Flip()
        {
            facingRight = !facingRight;
            Vector2 Scaler = transform.localScale;
            Scaler.x = Scaler.x * -1;
            transform.localScale = Scaler;
        }
    }