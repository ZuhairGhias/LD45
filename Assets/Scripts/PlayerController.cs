using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    public float stealDelayMs;

    private bool looting;
    private bool arrested = false;

    private Rigidbody2D rb;
    private CircleCollider2D cc;
    private List<Collider2D> peds;
    public ContactFilter2D contact;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CircleCollider2D>();
        peds = new List<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(float amount)
    {
        //rb.MovePosition(rb.position + (Vector2.right * amount * moveSpeed * Time.deltaTime));
        if (looting || arrested) return;
        transform.Translate(Vector2.right * amount * moveSpeed * Time.deltaTime);
    }

    public void Arrest() {
        print(arrested);
        arrested = true;
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
