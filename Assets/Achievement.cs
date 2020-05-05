using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets
{
    public class Achievement
    {
        public string Id;
        public bool IsUnlocked;
        public bool IsComplete;
        public Func<bool> Trigger;
        public string Title;
        public string Condition;
        public string RewardMessage;
        public Action Reward;
    }
}
