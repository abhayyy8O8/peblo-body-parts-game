using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Level1StarManager : MonoBehaviour
{
    [SerializeField] private Image[] stars;
    [SerializeField] private Sprite greyStar;
    [SerializeField] private Sprite goldStar;

    [SerializeField] private string nextLevelSceneName = "Level 2 – Face of Fun";
    [SerializeField] private float levelCompleteDelay = 1.0f;

    private bool headPlaced;
    private int handsPlaced;
    private int legsPlaced;

    private int currentStarIndex;
    private bool levelCompleted;

    void Start()
    {
        foreach (Image star in stars)
        {
            star.sprite = greyStar;
        }
    }

    public void ReportCorrectPlacement(BodyPartType partType)
    {
        if (levelCompleted) return;

        switch (partType)
        {
            case BodyPartType.Head:
                if (!headPlaced)
                {
                    headPlaced = true;
                    AddStar();
                }
                break;

            case BodyPartType.Hand:
                handsPlaced++;
                if (handsPlaced == 2)
                    AddStar();
                break;

            case BodyPartType.Leg:
                legsPlaced++;
                if (legsPlaced == 2)
                    AddStar();
                break;
        }
    }

    void AddStar()
    {
        if (currentStarIndex >= stars.Length)
            return;

        stars[currentStarIndex].sprite = goldStar;
        currentStarIndex++;

        if (currentStarIndex == stars.Length)
        {
            levelCompleted = true;
            StartCoroutine(LoadNextLevel());
        }
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(levelCompleteDelay);
        SceneManager.LoadScene(nextLevelSceneName);
    }
}
