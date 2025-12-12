using UnityEngine;

public class PlayerTestMap2 : MonoBehaviour
{
    public GameObject audioMap2;
    
    float moveSpeed = 10f;
    float jumpForce = 6f;
    public Vector2 move;
    Animator animator;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    public bool isGround = true;
    
    public GameObject attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    float attackRate = 2f;
    float nextAttackTime = 0;


    float blockRate = 2f;
    float nextBlockTime = 0;
    public bool isBlock = false;
    public bool isSucBlock = false;



    public bool isDash = false;
    float dashRate = 2f;
    float nextDashTime = 0;    

    public float stamina = 100;
    public float hp = 100;

    public bool isDead = false;


    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();           
    }

    private void Update()
    {
        if (!isDead)
        {
            if (!isBlock)
            {
                //Move
                move.x = Input.GetAxis("Horizontal");
                transform.position += (Vector3)move * moveSpeed * Time.deltaTime;
                animator.SetFloat("speed", Mathf.Abs(move.x));
                

                //Flip
                if (move.x < 0) { transform.localScale = new Vector3(-1, 1, 1); }
                else if (move.x > 0) { transform.localScale = new Vector3(1, 1, 1); }


                //Jump
                if (Input.GetKeyDown(KeyCode.Space) && isGround == true && stamina >= 10)
                {
                    rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

                    animator.SetTrigger("jump");

                    audioMap2.GetComponent<AudioMap2>().jumpSource.Play();

                    stamina -= 10;
                }

                if (transform.position.y < -2.5) isGround = true;
                else isGround = false;

                //Attack
                if (Time.time >= nextAttackTime)
                {
                    if (Input.GetMouseButtonDown(0) && isGround == true && stamina >= 10)
                    {
                        audioMap2.GetComponent<AudioMap2>().attackSource.Play();

                        animator.SetTrigger("attack");

                        stamina -= 10;

                        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackRange, enemyLayers);

                        foreach (Collider2D enemy in hitEnemies)
                        {
                            Debug.Log("We hit " + enemy.name);

                            if (enemy.CompareTag("Dummy")) enemy.GetComponent<Dummy>().TakeDame(20);

                            if (enemy.CompareTag("Arrow"))
                            {
                                audioMap2.GetComponent<AudioMap2>().hitSource.Play();
                                Destroy(enemy.gameObject);                                
                            }
                        }


                        nextAttackTime = Time.time + 1f / attackRate;

                    }
                }

                //Dash

                if (Time.time >= nextDashTime)
                {

                    if (Input.GetKeyDown(KeyCode.LeftShift) && isDash == false && stamina >= 35)
                    {
                        isDash = true;
                        animator.SetTrigger("dash");
                        audioMap2.GetComponent<AudioMap2>().dashSource.Play();
                        moveSpeed = 20f;


                        nextDashTime = Time.time + 1f / dashRate;

                        stamina -= 35;

                    }
                    else
                    {
                        moveSpeed = 10f;
                        isDash = false;
                    }

                }

            }

            


            //Block
            if (Time.time >= nextBlockTime)
            {
                if (Input.GetMouseButton(1) && isGround == true && isDash == false)
                {
                    animator.SetBool("block",true);
                    
                    isBlock = true;                    

                    nextBlockTime = Time.time + 1f / blockRate;
                }
                else
                {
                    isBlock = false;
                    animator.SetBool("block", false);
                }
            }
            //SuccessBlock
            if (isBlock && isSucBlock)
            {
                audioMap2.GetComponent<AudioMap2>().hitSource.Play();
                animator.SetTrigger("sucblock");
                isSucBlock = false;
            }




            


            //Stamina
            if (Time.time >= nextDashTime && Time.time >= nextAttackTime && isGround == true)
            {
                if (stamina < 100)
                {
                    stamina += 15f * Time.deltaTime;
                }
            }


            //Dead

            if (hp < 0)
            {
                hp = 0;
                isDead = true;
                animator.SetBool("dead", true);
                audioMap2.GetComponent<AudioMap2>().c1Source.Stop();
                audioMap2.GetComponent<AudioMap2>().c2Source.Stop();
                audioMap2.GetComponent<AudioMap2>().bgSource1.Stop();
                audioMap2.GetComponent<AudioMap2>().deadSource.Play();
            }
        }
        
            


    }
    
    public void TakeDame(float dame)
    {
        hp -= dame;
        animator.SetTrigger("hurt");
        audioMap2.GetComponent<AudioMap2>().hurtSource.Play();
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.transform.position, attackRange);
    }
}
