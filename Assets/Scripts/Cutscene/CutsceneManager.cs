using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CutsceneManager : MonoBehaviour
{
    [Header("Dialogue System")]
    [SerializeField] private IntroDialogueManager dialogueManager;
    [SerializeField] private Dialogue startDialogue;
    [SerializeField] private Dialogue endDialogue;
    [SerializeField] private Dialogue restartDialogue;
    [SerializeField] private Dialogue reendDialogue;
    
    [Header("Sounds")]
    [SerializeField] private AudioSource audioSourceGlobal;
    [SerializeField] private AudioSource audioSourceTV;
    [SerializeField] private AudioSource audioSourceDoor;
    [SerializeField] private AudioClip gunshot;
    [SerializeField] private AudioClip endTrack;

    [Header("Other")]
    [SerializeField] private CutscenePlayer player;
    [SerializeField] private TextMeshProUGUI doorNumberText;
    [SerializeField] private TextMeshProUGUI TVText;

    private enum IntroState { START, MAIN, END }
    private IntroState state = IntroState.START;
    
    void Start()
    {
        StartCoroutine(StartScene());
    }

    private IEnumerator StartScene()
    {
        yield return new WaitForSeconds(2);

        if (GameManager.GameComplete)
        {
            dialogueManager.LoadDialogue(restartDialogue);

            TVText.text = "Pickpocketing on the rise";

            int playthroughCount = PlayerPrefs.GetInt("PlaythoughCount", 1);
            doorNumberText.text = playthroughCount.ToString();
        }
        else
        {
            dialogueManager.LoadDialogue(startDialogue);
        }
        
    }

    public void NextScene()
    {
        switch (state)
        {
            case IntroState.START:
                state = IntroState.MAIN;
                StartCoroutine(MainScene());
                break;

            case IntroState.MAIN:
                state = IntroState.END;
                StartCoroutine(EndScene());
                break;

            case IntroState.END:
                SceneManager.LoadScene("GameScene");
                break;
        }
    }

    private IEnumerator MainScene()
    {
        yield return new WaitForSeconds(2);

        dialogueManager.Hide(2f);
        player.EnablePlayer();

        audioSourceTV.Play();
        audioSourceDoor.Play();
    }

    private IEnumerator EndScene()
    {
        yield return new WaitForSeconds(3f);

        audioSourceGlobal.Stop();
        audioSourceTV.Stop();

        audioSourceGlobal.PlayOneShot(gunshot);

        dialogueManager.Unide(0f);
        
        yield return new WaitForSeconds(3);

        if (GameManager.GameComplete)
        {
            dialogueManager.LoadDialogue(reendDialogue);

            audioSourceGlobal.clip = endTrack;
            audioSourceGlobal.Play();
        }
        else
        {
            dialogueManager.LoadDialogue(endDialogue);
        }
        
    }
}
