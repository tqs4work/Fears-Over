using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class BossTestMap3 : MonoBehaviour
{
    public GameObject canvas;
    public GameObject attack1Prefabs;
    public GameObject attack2Prefabs;

    GameObject attackCurLeft;
    GameObject attackCurRight;

    float moveSpeed = 10f;
    Animator animator;
    GameObject player;
    public LayerMask enemyLayers;
    public GameObject attackPoint;
    public float attackRange;
    
    public float hp = 200;
    

    int roll = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("PlayerTest");
    }

    // Update is called once per frame
    void Update()
    {
        //Flip
        if (transform.position.x < player.transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (transform.position.x > player.transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }


        if(canvas.GetComponent<CanvasCotrollerMap3>().isFight)
        {
            InvokeRepeating("Roll", 0, 5);

            //Choose activity

            if(roll == 1)
            {
                StartCoroutine(MovetoPlayer());
            }
            else if (roll == 2)
            {
                StartCoroutine(Attack1());
            }            

        }
        else
        {
            CancelInvoke();
        }
        

        if(hp<0)
        {
            canvas.GetComponent<CanvasCotrollerMap3>().isFight = false;
            animator.SetTrigger("dead");
        }

    }

    IEnumerator MovetoPlayer()
{
    animator.SetBool("run", true);
    
    while (Vector2.Distance(transform.position, player.transform.position) > 0.1f)
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        yield return null; // Đợi đến khung hình tiếp theo để tiếp tục di chuyển
    }

    animator.SetBool("run", false); // Dừng chạy khi đến gần player
}

    void Roll()
    {
        roll = (roll == 1) ? 2 : 1;
        StopAllCoroutines();
        animator.SetBool("run",false);
    }


    IEnumerator Attack1()
    {
        yield return null;
        animator.SetTrigger("attack2");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);

            if (enemy.CompareTag("Player")) enemy.GetComponent<PlayerTestMap3>().TakeDame(50);
            
        }

        if (transform.position.x < player.transform.position.x) SpawnAttackRight();
        else if (transform.position.x > player.transform.position.x) SpawnAttackLeft();

        if (attackCurLeft != null)
        {
            attackCurLeft.transform.position = Vector2.MoveTowards(attackCurLeft.transform.position, new Vector3(125, attackCurLeft.transform.position.y, 0),
            10f * Time.deltaTime);

            if (Vector2.Distance(attackCurLeft.transform.position, new Vector3(125, attackCurLeft.transform.position.y, 0)) <= 0.1f) Destroy(attackCurLeft);

            if(Vector2.Distance(attackCurLeft.transform.position, player.transform.position) <= 0.1f 
                && player.GetComponent<PlayerTestMap3>().isDash == false)
            {
                player.GetComponent<PlayerTestMap3>().TakeDame(20);
            }

        }

        if (attackCurRight != null)
        {
            attackCurRight.transform.position = Vector2.MoveTowards(attackCurRight.transform.position, new Vector3(200, attackCurLeft.transform.position.y, 0),
            10f * Time.deltaTime);

            if (Vector2.Distance(attackCurRight.transform.position, new Vector3(125, attackCurRight.transform.position.y, 0)) <= 0.1f) Destroy(attackCurRight);

            if (Vector2.Distance(attackCurRight.transform.position, player.transform.position) <= 0.1f
                && player.GetComponent<PlayerTestMap3>().isDash == false)
            {
                player.GetComponent<PlayerTestMap3>().TakeDame(20);
            }

        }



    }

    void SpawnAttackLeft()
    {        
        attackCurLeft = Instantiate(attack2Prefabs, transform.position, Quaternion.identity);
        
        
    }
    void SpawnAttackRight()
    {
        attackCurRight = Instantiate(attack1Prefabs, transform.position, Quaternion.identity);
        
        
    }


    public void TakeDame(float dame)
    {
        hp -= dame;
        animator.SetTrigger("hurt");
        //audioMap3.GetComponent<AudioMap3>().hurtSource.Play();
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.transform.position, attackRange);
    }



}
