using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PromptMessageScript : MonoBehaviour
{
    public TMP_Text message;
    private void Start()
    {
        StartCoroutine(destroyme());
    }
    IEnumerator destroyme() 
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
