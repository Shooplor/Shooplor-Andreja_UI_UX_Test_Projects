using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{

    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] GameObject player;
    [SerializeField] GameObject gameManager;
    public Animator animator;
    public GameObject bloodParticleSystem;



    
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        gameManager = GameObject.FindWithTag("GameManager");
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    
    void Update()
    {
        navMeshAgent.SetDestination(player.transform.position);
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            animator.SetBool("isAttacking", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            Die();
            Destroy(other.gameObject);
        }
    }

    void Die()
    {
        FindObjectOfType<EnemySpawnerScript>()?.OnEnemyDeath();
        Instantiate(bloodParticleSystem, transform.position, Quaternion.identity);
        Destroy(gameObject);
       // gameManager.GetComponent<GameManager>().enemiesKilled = gameManager.GetComponent<GameManager>().enemiesKilled + 1;
    }
}
