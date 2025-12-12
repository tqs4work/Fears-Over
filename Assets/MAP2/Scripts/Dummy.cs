using UnityEngine;

public class Dummy : MonoBehaviour
{
    Animator animator;
    
    public int maxHealth = 100;
    int currentHealth;

    private void Start()
    {
        
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();

    }
    public void TakeDame(int damage)
    {
        currentHealth -= damage;


        //anim
        animator.SetTrigger("Hurt");


        if (currentHealth < 0)
        {
            Die();
        }
    }
    void Die()
    {
        Debug.Log("Enemy die!");

        animator.SetBool("IsDead",true);

        GetComponent<Collider2D>().enabled = false;

    }
}
