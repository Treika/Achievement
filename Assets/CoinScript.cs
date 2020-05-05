using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour {

    public AchievementManager AchievementManager;
    bool _alreadyCollected = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (AchievementManager.CanCollectCoins && collision.gameObject.tag == "Player")
        {
            if (_alreadyCollected)
            {
                Debug.Log("already collected");
                return;
            }

            _alreadyCollected = true;
            collision.gameObject.GetComponent<Inventory>().Coins++;
            //Debug.Log($"{collision.gameObject.GetComponent<Inventory>().Coins} coins collected!");
            var playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
            playerMovement.GetComponent<Rigidbody2D>().velocity = playerMovement.CurrentVelocity;
            this.gameObject.SetActive(false);
        }
    }
}
