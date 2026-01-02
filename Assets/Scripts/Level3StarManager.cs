using UnityEngine;
using UnityEngine.UI;

public class Level3StarManager : MonoBehaviour
{
    [SerializeField] private Image[] stars;
    [SerializeField] private Sprite greyStar;
    [SerializeField] private Sprite goldStar;

    [SerializeField] private Level3TimerManager timerManager;
    [SerializeField] private Level3CompletionUI completionUI;

    private int currentStarIndex;
    private bool levelCompleted;

    void Start()
    {
        foreach (Image star in stars)
            star.sprite = greyStar;
    }

    public void AddStar()
    {
        if (levelCompleted || currentStarIndex >= stars.Length)
            return;

        stars[currentStarIndex].sprite = goldStar;
        currentStarIndex++;

        if (currentStarIndex == stars.Length)
        {
            levelCompleted = true;

            if (timerManager != null)
                timerManager.StopTimer();

            if (completionUI != null)
                completionUI.ShowCompletion();
        }
    }
}
