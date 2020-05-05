using Assets;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{

    public bool CanMoveRight;
    public bool CanMoveLeft;
    public bool CanJump;
    public bool CanCollectCoins;
    public bool CanDie;


    public Canvas AchievementMessageCanvas;
    public Text Title;
    public Text Description;
    public Text Reward;
    public float FadeSeconds;

    private CanvasGroup _canvasGroup;
    private List<Achievement> _achievements = new List<Achievement>();

    public void AddAchievement(Achievement achievement)
    {
        _achievements.Add(achievement);
    }

    // Use this for initialization
    void Start()
    {
        _canvasGroup = AchievementMessageCanvas.GetComponent<CanvasGroup>();
        InvokeRepeating("CheckAchievements", 1f, 1f);  //1s delay, repeat every 1s
    }

    private void CheckAchievements()
    {
        foreach (var achievement in _achievements.Where(x => x.IsUnlocked && !x.IsComplete))
        {
            if (achievement.Trigger())
            {
                achievement.Reward();
                achievement.IsComplete = true;
                StartCoroutine(ShowAchievement(achievement));
            }
        }
    }

    //might want to rewrite as a queue?
    IEnumerator ShowAchievement(Achievement achievement)
    {
        Title.text = achievement.Title;
        Description.text = achievement.Condition;
        Reward.text = achievement.RewardMessage;
        //AchievementMessageCanvas.GetComponent<CanvasGroup>().alpha = 1;
        StartCoroutine(FadeIn(1));
        //var test = AchievementMessageCanvas.GetComponent<CanvasRenderer>();
        yield return new WaitForSeconds(5);
        StartCoroutine(FadeOut(0));
        //AchievementMessageCanvas.GetComponent<CanvasGroup>().alpha = 0;
    }

    IEnumerator FadeIn(float target)
    {
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            var newAlpha = i;
            _canvasGroup.alpha = newAlpha;
            yield return null;
        }
    }

    IEnumerator FadeOut(float target)
    {
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            var newAlpha = i;
            _canvasGroup.alpha = newAlpha;
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

}
