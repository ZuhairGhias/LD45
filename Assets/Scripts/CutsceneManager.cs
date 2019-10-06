using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private IntroDialogueManager dm;
    [SerializeField] private Dialogue startDialogue;
    [SerializeField] private Dialogue endDialogue;

    private enum IntroState { STARTSCENE, MAINSCENE, ENDSCENE }
    private IntroState state = IntroState.STARTSCENE;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator StartScene()
    {
        yield return new WaitForSeconds(4);
        dm.LoadDialogue(startDialogue);

    }

    public void NextScene()
    {

        switch (state)
        {
            case IntroState.STARTSCENE:
                state = IntroState.MAINSCENE;
                StartCoroutine(MainScene());
                break;
            case IntroState.MAINSCENE:
                state = IntroState.ENDSCENE;
                StartCoroutine(EndScene());
                break;
            case IntroState.ENDSCENE:
                print("Ended");
                break;
        }
    }

    private IEnumerator MainScene()
    {
        print("Playing knocking sounds");
        yield return new WaitForSeconds(2);
        dm.Hide();
        print("There should be some gameplay here");
        yield return new WaitForSeconds(2);
        dm.Unide();
        NextScene();

    }

    private IEnumerator EndScene()
    {
        yield return new WaitForSeconds(4);
        dm.LoadDialogue(endDialogue);

    }
}
