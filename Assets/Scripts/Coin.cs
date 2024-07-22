using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioClip CoinSound;
   void OnTriggerEnter2D(Collider2D other) {
    if(other.gameObject.layer == LayerMask.NameToLayer("Player")){
        FindObjectOfType<GameSession>().AddPlayerScore(10);
        AudioSource.PlayClipAtPoint(CoinSound, Camera.main.transform.position); // Play the sound at the camera position
        Destroy(gameObject);
    }
   }
}
