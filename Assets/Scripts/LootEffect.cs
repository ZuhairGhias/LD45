using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LootEffect : MonoBehaviour
{
    [SerializeField] private float timeOut;
    [SerializeField] private Animation anim;
    [SerializeField] private TextMeshPro lootText;

    // Start is called before the first frame update
    void Start()
    {
       
        Destroy(this.gameObject, timeOut);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string text)
    {
        lootText.text = text;
    }

}
