using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D enemyRigidbody;
    Collider2D enemySideCollider;
    [SerializeField] float moveSpeed = 5f;    

    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        enemySideCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
       enemyRigidbody.velocity = new Vector2(moveSpeed, enemyRigidbody.velocity.y);
      
    }  


    void FlipEnemy()
    {
        float enemyDirection = Mathf.Sign(enemyRigidbody.velocity.x);
        transform.localScale = new Vector2(-enemyDirection, 1f);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Hazards"))
        {
            moveSpeed = -moveSpeed;
            FlipEnemy();
        }
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            moveSpeed = -moveSpeed;
            FlipEnemy();
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
       Dictionary<string, bool> collisions = TwoDCollisionHelper.GetCollisionsPositions(transform, enemySideCollider, LayerMask.GetMask("Ground"));
       if(collisions["left"] || collisions["right"] ){
            moveSpeed = -moveSpeed;
            FlipEnemy();
        }
        
    }
}
