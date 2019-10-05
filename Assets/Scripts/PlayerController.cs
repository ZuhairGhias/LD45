using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    public float stealDelayMs;

    public bool looting;
    public bool arrested = false;

    private Rigidbody2D rb;
    private CircleCollider2D cc;
    private List<Collider2D> peds;
    private SpriteRenderer sr;
    public ContactFilter2D contact;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CircleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        peds = new List<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(float amount)
    {
        if (looting || arrested) return;
        transform.Translate(Vector2.right * amount * moveSpeed * Time.deltaTime);
    }

    public void Hide()
    {
        if (looting || arrested) return;
        Color color = sr.color;
        color.a = 0.5f;
        sr.color = color;
        cc.enabled = false;
    }

    public void Unhide()
    {
        if (looting || arrested) return;
        Color color = sr.color;
        color.a = 1f;
        sr.color = color;
        cc.enabled = true;
    }

    public void Arrest()
    {
        arrested = true;
        print("Player arrested");
        cc.enabled = false;
    }

    public void Steal()
    {
        if (looting) return;
        StartCoroutine(StealRoutine());
    }
    private IEnumerator StealRoutine()
        {
        looting = true;
        print("Trying to steal");
        yield return new WaitForSeconds(stealDelayMs/1000);
        cc.OverlapCollider(contact, peds);
        foreach(Collider2D ped in peds){
            if (ped.GetComponent<Pedestrian>() != null) ped.GetComponent<Pedestrian>().Loot();
        }
        looting = false;
    }
}
