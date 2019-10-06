using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private IntroDialogueManager dialogueManager;
    [SerializeField] private CutscenePlayer player;
    [SerializeField] private Dialogue startDialogue;
    [SerializeField] private Dialogue endDialogue;

    private enum IntroState { START, MAIN, END }
    private IntroState state = IntroState.START;
    
    void Start()
    {
        StartCoroutine(StartScene());
    }

    private IEnumerator StartScene()
    {
        yield return new WaitForSeconds(2);
        dialogueManager.LoadDialogue(startDialogue);
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
                print("[Cutscene Manager] CUT! GOOD WORK EVERYONE!");
                break;
        }
    }

    private IEnumerator MainScene()
    {
        yield return new WaitForSeconds(2);

        dialogueManager.Hide(2f);
        player.EnablePlayer();
    }

    private IEnumerator EndScene()
    {
        // Wait for the gunshot (adjust this as needed)
        yield return new WaitForSeconds(1.5f);

        // Play gunshot and show the UI
        dialogueManager.Unide(0f);
        
        yield return new WaitForSeconds(3);

        dialogueManager.LoadDialogue(endDialogue);
    }
}
