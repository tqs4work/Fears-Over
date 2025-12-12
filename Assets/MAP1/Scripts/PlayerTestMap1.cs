using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.Collections;
using System.Collections;
public class PlayerTestMap1 : MonoBehaviour
{
    float moveSpeed = 15f;
    float jumpForce = 13f;
    public Vector2 move;
    Vector2 direct;
    SpriteRenderer spriteRenderer;
    Animator animator;
    Rigidbody2D rb;
    public bool isHide = false;
    public bool isGround = true;
    public bool isDead = false;

    float timeJumpRate = 1f;
    float nextJumpTime = 0f;

    public Text textCD;
    public bool isExhaust = false;


    public float stamina = 100;
    public float detectRate = 0;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();        
    }
    private void Update()
    {
        if (isDead == false)
        {
            //Move
            move.x = Input.GetAxis("Horizontal");
            if (stamina >= moveSpeed * Time.deltaTime && isExhaust == false)
            {
                transform.position += (Vector3)move * moveSpeed * Time.deltaTime;
            }
            animator.SetFloat("speed", Mathf.Abs(move.x));

            if (move.x != 0)
            {
                stamina -= 10f * Time.deltaTime;
            }
            else if (stamina < 100)
            {
                stamina += moveSpeed * Time.deltaTime;
            }
            else if (stamina > 100)
            {
                stamina = 100;
            }

            if (move.x < 0) { spriteRenderer.flipX = true; }
            else if (move.x > 0) { spriteRenderer.flipX = false; }


            //Jump        
            if (Time.time >= nextJumpTime && isExhaust == false)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

                    animator.SetTrigger("jump");

                    nextJumpTime = Time.time + 1f / timeJumpRate;

                    stamina -= 10;
                }
            }



            //Hide
            if ((transform.position.x > 155 && transform.position.x < 170)
            || (transform.position.x > 250 && transform.position.x < 256)
            || (transform.position.x > 335 && transform.position.x < 340))
            {
                isHide = true;
            }
            else isHide = false;


            //Exhaust
            if (stamina < 0)
            {
                stamina = 0;
                isExhaust = true;
                animator.SetBool("exhaust", true);
                StartCoroutine(Countdown(5));

            }


            
        }

        //Dead
        if (isDead == true)
        {            
            animator.SetBool("dead",true);
        }


    }
    IEnumerator Countdown(float num)
    {
        float t = num;
        textCD.text = "";
        textCD.enabled = true;

        while (t > 0)
        {            
            if(isDead == true) 
            {
                animator.SetBool("exhaust", false);
                textCD.enabled = false;
                yield break; // Thoát khỏi coroutine ngay lập tức
            }
            t -= Time.deltaTime;
            textCD.text = t.ToString("F1");
            yield return null;
        }
        
        textCD.enabled = false;
        isExhaust = false;
        animator.SetBool("exhaust",false);
    }
}
