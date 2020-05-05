using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class PlayerMovementAchievements : MonoBehaviour
    {
        public AchievementManager _achievementManager;
        public PlayerMovement _playerMovement;
        
        void Start()
        {
            //var properties = typeof(PlayerMovementAchievements).GetMethods(BindingFlags.DeclaredOnly |
            //                                                                BindingFlags.Instance |
            //                                                                BindingFlags.Public);

            Achievement test = new Achievement
            {
                Id = "MoveLeft",
                IsUnlocked = true,
                Trigger = CanMoveRightCheck,
                Title = "Every adventure starts with a lot of nothing",
                Condition = "Do nothing for 5 seconds",
                RewardMessage = "Move Right unlocked",
                Reward = () => { _achievementManager.CanMoveRight = true; }
            };

            _achievementManager.AddAchievement(test);
        }

        private bool CanMoveRightCheck()
        {
            var diffInSeconds = (DateTime.UtcNow - _playerMovement.lastMovement).TotalSeconds;
            if(diffInSeconds > 5)
            {
                //unlock achievement, but for now...
                //_achievementManager.CanMoveRight = true;
                return true;
            }

            return false;
        }
    }
}
