using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class IntroDialogueManager : MonoBehaviour
{

    [SerializeField] private CutsceneManager cm;

    private Dialogue dialogue;
    [SerializeField] private TextMeshProUGUI textbox;
    [SerializeField] private TextMeshProUGUI continuebox;
    [SerializeField] private Image background;
    private int dialogueNumber;
    private IEnumerator smoothText;
    [SerializeField] private float delayMs;
    [SerializeField] private float continueDelay;

    // Start is called before the first frame update
    void Start()
    {
        textbox.text = "";
        continuebox.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && continuebox.enabled) LoadNext();
    }

    public void LoadDialogue(Dialogue dialogue)
    {
        this.dialogue = dialogue;
        dialogueNumber = 0;
        LoadNext();
    }

    public void LoadNext()
    {
        continuebox.enabled = false;
        if (smoothText != null) StopCoroutine(smoothText);
        if (dialogueNumber >= dialogue.sentences.Count)
        {
            textbox.text = "";
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
            textbox.text = newText;
            yield return new WaitForSeconds(delayMs / 1000);
        }
        yield return new WaitForSeconds(continueDelay);
        continuebox.enabled = true;
        

    }

    private void Done()
    {
        cm.NextScene();
    }

    public void Hide()
    {
        background.CrossFadeAlpha(0, 3, false);
    }

    public void Unide()
    {
        background.CrossFadeAlpha(1, 3, false);
    }
}
