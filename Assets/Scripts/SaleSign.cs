using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaleSign : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Start()
    {
        StartCoroutine(CheckSign());
    }

    IEnumerator CheckSign()
    {
        while (!Inventory.HasSign)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(1f);
        }

        spriteRenderer.enabled = true;
    }
}
