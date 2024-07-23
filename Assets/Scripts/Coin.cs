using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioClip CoinSound;
    [SerializeField] int scorePerCoin = 100;
    bool hasBeenCollected = false;
   void OnTriggerEnter2D(Collider2D other) {
    if(other.gameObject.layer == LayerMask.NameToLayer("Player") && !hasBeenCollected){
        hasBeenCollected = true; // To ensure that the coin is not picked up again
        FindObjectOfType<GameSession>().AddPlayerScore(scorePerCoin);
        AudioSource.PlayClipAtPoint(CoinSound, Camera.main.transform.position); // Play the sound at the camera position
        gameObject.SetActive(false); // To ensure that the coin is not picked up again(Just in case)
        Destroy(gameObject);
    }
   }
}
