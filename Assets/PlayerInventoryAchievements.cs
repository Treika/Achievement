using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryAchievements : MonoBehaviour {

    public Inventory PlayerInventory;
    public AchievementManager AchievementManager;

    // Use this for initialization
    void Start () {
        Achievement test = new Achievement
        {
            Id = "ThreeCoins",
            IsUnlocked = true,
            Trigger = () => { return PlayerInventory.Coins >= 3; },
            Title = "Money money money. Literally.",
            Condition = "Collect 3 coins",
            RewardMessage = "Death unlocked!",
            Reward = () => { AchievementManager.CanDie = true; }
        };

        AchievementManager.AddAchievement(test);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
