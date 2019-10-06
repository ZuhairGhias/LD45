using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class IntroDialogueManager : MonoBehaviour
{
    [SerializeField] private CutsceneManager cutsceneManager;
    [SerializeField] private TextMeshProUGUI textBox;
    [SerializeField] private TextMeshProUGUI continueBox;
    [SerializeField] private Image background;
    [SerializeField] private float textTypeDelay = 0.01f;
    [SerializeField] private float continueDelay;
    
    private Dialogue dialogue;
    private int dialogueNumber;
    private IEnumerator smoothText;
    
    void Start()
    {
        textBox.text = "";
        continueBox.enabled = false;
    }
    
    void Update()
    {
        if (Input.GetKeyDown("space") && continueBox.enabled)
        {
            LoadNext();
        }
    }

    public void LoadDialogue(Dialogue newDialogue)
    {
        dialogue = newDialogue;
        dialogueNumber = 0;

        LoadNext();
    }

    public void LoadNext()
    {
        continueBox.enabled = false;

        if (smoothText != null)
        {
            StopCoroutine(smoothText);
        }

        if (dialogueNumber >= dialogue.sentences.Count)
        {
            textBox.text = "";
            Done();
            return;
        }

        smoothText = SmoothText(dialogue.sentences[dialogueNumber]);
        StartCoroutine(smoothText);

        dialogueNumber++;
    }

    public IEnumerator SmoothText(string text)
    {
        string newText = "";

        while (newText.Length < text.Length)
        {
            newText = text.Substring(0, newText.Length + 1);
            textBox.text = newText;

            yield return new WaitForSeconds(textTypeDelay);
        }

        yield return new WaitForSeconds(continueDelay);

        continueBox.enabled = true;
    }

    private void Done()
    {
        cutsceneManager.NextScene();
    }

    public void Hide(float duration)
    {
        background.CrossFadeAlpha(0, duration, false);
    }

    public void Unide(float duration)
    {
        background.CrossFadeAlpha(1, duration, false);
    }
}
