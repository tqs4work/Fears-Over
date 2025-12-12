using UnityEngine;

public class go : MonoBehaviour
{
    public float moveSpeed = 5f;

    Vector2 movement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(moveX, moveY, 0).normalized;

        // Di chuyển bằng transform
        transform.Translate(move * moveSpeed * Time.deltaTime);

    }


}
