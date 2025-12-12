using UnityEngine;

public class EnemyTestMap3 : MonoBehaviour
{
    float moveSpeed = 5f;
    float timeWalk = 6f;
    float timeStand = 3f;
    float timeCount = 0f;
    bool isStand = false;
    bool isDetect = false;
    Vector2 move;
    Vector2 direct;
    Animator animator;
    SpriteRenderer spriteRenderer;
    public GameObject player;

    private void Start()
    {
        direct = Vector2.left;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        Flip();
        Check();
        if (!isDetect)
        {
            if (!isStand)
            {
                Walk();
            }
            else
            {
                Stand();
            }
        }
        else 
        { 
            Chase(); 
        }
            
        
    }

    void Walk()
    {
        move = direct * moveSpeed * Time.deltaTime;
        transform.position += (Vector3)move;
        timeCount += Time.deltaTime;

        if (timeCount >= timeWalk)
        {
            isStand = true;
            timeCount = 0f;
        }
    }
    void Stand()
    {
        timeCount += Time.deltaTime;
        if(timeCount >= timeStand)
        {
            isStand = false;
            timeCount = 0f;
            direct = (direct == Vector2.right) ? Vector2.left : Vector2.right;
        }
    }
    void Flip()
    {
        if (direct == Vector2.right)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }
    void Check()
    {
        int enemyLayer = LayerMask.GetMask("Enemy");
        int layerMask = ~enemyLayer;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direct, 5f, layerMask);
        if(hit.collider != null)
        {
            isDetect = true;
        }
    }
    void Chase()
    {
        animator.SetBool("isDetect",true);
        float runSpeed = 5f;
        float step = runSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step);

        if (transform.position.x > player.transform.position.x) direct = Vector2.left;
        else direct = Vector2.right;

        if (Vector2.Distance(transform.position, player.transform.position) <= 4f)
        {
            Attack();
        }

    }
    void Attack()
    {
        animator.SetTrigger("Attack");
    }


}
