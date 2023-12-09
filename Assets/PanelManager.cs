using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public Animator animator;
    public GameObject[] ScreenSections;
    
    int SectionIndex =0;
    public void ChangeSection(int index) 
    {
        SectionIndex = index;
        animator.SetTrigger("In");
    }

    IEnumerator Cotransition() 
    {
       foreach(GameObject go in ScreenSections) 
        {
            go.SetActive(false);
            yield return new WaitForSeconds(0.1f);
        }
        ScreenSections[SectionIndex].SetActive(true);
        animator.SetTrigger("Out");
    }
}
