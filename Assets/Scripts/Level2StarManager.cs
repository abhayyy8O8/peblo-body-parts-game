using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Level2StarManager : MonoBehaviour
{
    [SerializeField] private Image[] stars;
    [SerializeField] private Sprite greyStar;
    [SerializeField] private Sprite goldStar;

    [SerializeField] private string nextLevelSceneName = "Level 3 – Tiny Movers";
    [SerializeField] private float levelCompleteDelay = 1.0f;

    private int eyesPlaced;
    private int earsPlaced;
    private bool nosePlaced;
    private bool mouthPlaced;

    private int currentStarIndex;
    private bool levelCompleted;

    void Start()
    {
        foreach (Image star in stars)
            star.sprite = greyStar;
    }

    public void ReportCorrectPlacement(BodyPartType partType)
    {
        if (levelCompleted) return;

        switch (partType)
        {
            case BodyPartType.Eye:
                eyesPlaced++;
                if (eyesPlaced == 2)
                    AddStar();
                break;

            case BodyPartType.Ear:
                earsPlaced++;
                if (earsPlaced == 2)
                    AddStar();
                break;

            case BodyPartType.Nose:
                if (!nosePlaced)
                {
                    nosePlaced = true;
                    CheckFaceCenter();
                }
                break;

            case BodyPartType.Mouth:
                if (!mouthPlaced)
                {
                    mouthPlaced = true;
                    CheckFaceCenter();
                }
                break;
        }
    }

    void CheckFaceCenter()
    {
        if (nosePlaced && mouthPlaced)
            AddStar();
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
