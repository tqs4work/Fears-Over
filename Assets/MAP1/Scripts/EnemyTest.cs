using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyTest : MonoBehaviour
{
    float moveSpeed = 5f;
    float timeWalk = 6f;
    float timeStand = 3f;
    float timeCount = 0f;
    Vector2 move;
    Vector2 direct;
    bool isStand = false;
    public bool isDetect = false;
    Animator animator;
    SpriteRenderer spriteRenderer;
    LineRenderer lineRenderer;
    public GameObject player;    
    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        lineRenderer = GetComponent<LineRenderer>();
        direct = Vector2.left;
        // Đảm bảo LineRenderer có 2 điểm
        lineRenderer.positionCount = 2;
    }
    private void Update()
    {        
        Flip();
        CheckPlayer();
        if (!isDetect )
        {
            Walk();
            
        }
        else
        {
            transform.Find("warning").gameObject.SetActive(true);
            Invoke("Chase", 1f);
        }
                


    }
    void Flip()
    {
        if (direct == Vector2.left)
        {
            transform.localScale = new Vector3(-1,1,1);
        }
        else
        {
            transform.localScale = new Vector3(1,1,1);
        }
    }   
    void Walk()
    {
        if (!isStand)
        {
            // Di chuyển
            move = direct.normalized * moveSpeed * Time.deltaTime;
            transform.position += (Vector3)move;

            // Cập nhật timeCount
            timeCount += Time.deltaTime;

            if (timeCount >= timeWalk)
            {
                timeCount = 0;
                isStand = true;
                animator.SetBool("isStand", true);
            }
        }
        else
        {
            // Đứng yên
            timeCount += Time.deltaTime;

            if (timeCount >= timeStand)
            {
                timeCount = 0;
                direct = (direct == Vector2.right) ? Vector2.left : Vector2.right;
                isStand = false;
                animator.SetBool("isStand", false);
            }
        }
    } 
    
    void CheckPlayer()
    {
        // Tạo LayerMask để loại trừ layer của chính Enemy
        int enemyLayer = LayerMask.GetMask("Enemy");
        int layerMask = ~enemyLayer; // Lấy tất cả các layer ngoại trừ layer Enemy                        
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direct, 19f, layerMask);
        if(hit.collider != null)
        {
            

            GameObject player = hit.collider.gameObject;

            if (player.GetComponent<PlayerTestMap1>().isHide == false)
            {
                isDetect = true;
                //Debug.Log("Detect " + hit.collider.name);
                //DrawRay(transform.position, hit.point);
            }            

            
        }
        else 
        {
            //DrawRay(transform.position, (Vector2)transform.position + direct * 19f);
        }
        
    }

    void Chase()
    {
        animator.SetBool("isDetect", true);
        float runSpeed = 10f;
        float step = runSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step);

        if (transform.position.x > player.transform.position.x) direct = Vector2.left;
        else direct = Vector2.right;

        if (Vector2.Distance(transform.position, player.transform.position) <= 4f)
        {
            GameObject.Find("PlayerTest").GetComponent<PlayerTestMap1>().isDead = true;
            isDetect = false;
            isStand = true;       
            
        }
    }    


    void DrawRay(Vector2 start, Vector2 end)
    {
        lineRenderer.SetPosition(0, start); // Điểm bắt đầu
        lineRenderer.SetPosition(1, end);   // Điểm kết thúc        
        //lineRenderer.enabled = true; // Bật LineRenderer
    }
}
