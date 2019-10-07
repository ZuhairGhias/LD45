using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScreen : MonoBehaviour
{
    public static bool skipTutorial = false;

    [Header("Tutorial References")]
    [SerializeField] private Image background;
    [SerializeField] private List<GameObject> tutorialScreens;
    [SerializeField] private GameObject continueText;
    [SerializeField] private AudioSource soundtrackSource;
    [SerializeField] private GameManager gameManager;

    [Header("Timing Settings")]
    [SerializeField] private float fadeOutTime = 2f;
    [SerializeField] private float tutorialReadDelay = 3f;

    private int currentTutorialScreen = 0;
    private bool isContinueEnabled = false;
    private bool isTutorialActive = true;

    private void Start()
    {
        if (skipTutorial)
        {
            FadeOut();
        }
        else
        {
            StartTutorial();
        }
    }

    private void Update()
    {
        if (!isTutorialActive || !isContinueEnabled)
        {
            return;
        }

        if (Input.GetKeyDown("space"))
        {
            NextScreen();
        }
    }

    private void StartTutorial()
    {
        currentTutorialScreen = -1;
        NextScreen();
    }

    private void NextScreen()
    {
        currentTutorialScreen++;

        continueText.SetActive(false);
        isContinueEnabled = false;

        foreach(GameObject screen in tutorialScreens)
        {
            screen.SetActive(false);
        }
        
        if (currentTutorialScreen == tutorialScreens.Count)
        {
            skipTutorial = true;
            FadeOut();

            return;
        }

        StartCoroutine(EnableContinue());

        tutorialScreens[currentTutorialScreen].SetActive(true);
    }

    private void FadeOut()
    {
        isContinueEnabled = false;

        background.color = Color.black;
        background.CrossFadeAlpha(0f, fadeOutTime, false);

        soundtrackSource.Play();
        gameManager.StartRound();
    }

    private IEnumerator EnableContinue()
    {
        yield return new WaitForSeconds(tutorialReadDelay);

        continueText.SetActive(true);
        isContinueEnabled = true;
    }
}
