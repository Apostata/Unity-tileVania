using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    Rigidbody2D arrowRigidbody;
    [SerializeField] float arrowSpeed = 5f;
    Player player;
    float playerDirection;
    float arrowDirectionSpeed =0;
    void Start()
    {
     arrowRigidbody = GetComponent<Rigidbody2D>();  
     player = FindObjectOfType<Player>(); 
     playerDirection = player.transform.localScale.x;
     transform.localScale = new Vector2(playerDirection, 1f);
     arrowDirectionSpeed = arrowSpeed * playerDirection;
    }

    void Update()
    {
        arrowRigidbody.velocity = new Vector2(arrowDirectionSpeed, arrowRigidbody.velocity.y);
    }

     void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy")){
            Destroy(other.gameObject);
        }

        Destroy(gameObject);
    }
}
