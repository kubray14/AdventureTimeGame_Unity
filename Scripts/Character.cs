using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [SerializeField]
    float Speed;

    [SerializeField]
    Rigidbody2D Rb;

    [SerializeField]
    AudioSource collect;

    [SerializeField]
    AudioSource death;

    [SerializeField]
    AudioSource gameFinish;

    [SerializeField]
    AudioSource gameMusic;

    [SerializeField]
    GameObject startPanel;

    [SerializeField]
    GameObject restartPanel;

    [SerializeField]
    GameObject NextLevelPanel;

    [SerializeField]
    GameObject FinishedPanel;

    [SerializeField]
    Animator anim;

    [SerializeField]
    Text scoreText;

    string level;

    public static bool degdiMi , finished = false;
    public static int score = 0;
    public float jumpHeight;
    public bool isJumping = false;
    public static bool isGameStarted = false;
    bool isRun, isFalling = false;
    bool nextLevelFreeze = false;
   
    void Start()
    {
        level = SceneManager.GetActiveScene().name;
        if (GameManager.isRestart == true || level != "Level1")
        {
            startPanel.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.onOff)
        {
            gameMusic.mute = false;
        }
        else
        {
            gameMusic.mute = true; 
        }
        if (isGameStarted == false || nextLevelFreeze == true)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isJumping == false)
            {
                Rb.velocity = new Vector2(Rb.velocity.x, jumpHeight * Speed);
                isJumping = true;
                anim.SetBool("isJump", isJumping);
            }
        }
        if (Rb.velocity.y < -0.05)
        {
            isFalling = true;
            anim.SetBool("isFall", isFalling);
        }
        else
        {
            isFalling = false;
            anim.SetBool("isFall", isFalling);
        }
    }
    private void FixedUpdate()
    {
        if (isGameStarted == false || nextLevelFreeze == true)
        {
            return;
        }
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        //print(horizontal);
        
        Rb.velocity = new Vector2(horizontal * Speed, Rb.velocity.y);
        animasyon(horizontal);
        turnMove(horizontal);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Terrain" || collision.gameObject.tag == "Disappear")
        {
            isJumping = false;
            anim.SetBool("isJump", isJumping);
        }
        /*if (collision.gameObject.CompareTag("Disappear"))
        {
            Destroy(collision.gameObject, 2f);
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!finished)
        {
            if (collision.CompareTag("Fruit"))
            {
                addScore(collision, 1);
                scoreText.text = ": " + score.ToString();
            }
            else if (collision.CompareTag("DeathStick"))
            {
                death.Play();
                Destroy(this.gameObject);
                restartPanel.SetActive(true);
            }
            else if (collision.CompareTag("flag") && score == 10)
            {
                if (SceneManager.GetActiveScene().name == "Level3")
                {
                    degdiMi = true;
                    gameFinish.Play();
                    anim.speed = 0;
                    nextLevelFreeze = true;
                    finished = true;
                    FinishedPanel.SetActive(true);
                }
                else
                {
                    degdiMi = true;
                    gameFinish.Play();
                    anim.speed = 0;
                    nextLevelFreeze = true;
                    NextLevelPanel.SetActive(true);
                    finished = true;
                }
            }
            else if (collision.CompareTag("spike"))
            {
                Destroy(this.gameObject);
                death.Play();
                restartPanel.SetActive(true);
            }

        }
    }

    
    void addScore(Collider2D collision, int add)
    {
        score += add;
        collect.Play();
        Destroy(collision.gameObject);
    }
    public void startGame()
    {
        isGameStarted = true;
        startPanel.SetActive(false);
    }
    void animasyon(float horizontal)
    {
        if (horizontal != 0)
        {
            isRun = true;
        }
        else
        { 
            isRun = false;
        }
        anim.SetBool("isRun", isRun);
    }
    void turnMove(float horizontal)
    {
        if (horizontal > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (horizontal < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
}
