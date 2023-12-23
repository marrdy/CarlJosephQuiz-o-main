using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class IdentifcationEditor : MonoBehaviour
{
    public UserManager um;
    public QuizManager qm;
    public EditIdentificationScript EIS;
    public TMP_Dropdown addInDiff;
    public TMP_Dropdown topic;
    public void LoadQuizes() 
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        
         qm.SetOfQuiz = DataSaver.LoadQuiz(qm.SetOfQuiz);
       

        Identification[] easy = qm.SetOfQuiz.identifications.easy;
        for (int i = 0; i < easy.Length; i++)
        {
            int currentIndex = i;
            if(easy[i].author.username == um.activeUser.username) 
            {
                EIS.setValue(easy[i],0);
                EIS.index = i;
                EIS.qm = qm;
                GameObject ies = Instantiate(EIS.gameObject);
                ies.transform.SetParent(transform);
                ies.transform.localScale = new Vector3(1, 1, 1);
                
            }
         
        }
        Identification[] medium = qm.SetOfQuiz.identifications.medium;
        for (int i = 0; i < medium.Length; i++)
        {
            int currentIndex = i;
            if (medium[i].author.username == um.activeUser.username)
            {
                EIS.setValue(medium[i],1);
                EIS.index = i;
                EIS.qm = qm;
                GameObject ies = Instantiate(EIS.gameObject);
           
                ies.transform.SetParent(transform);
                ies.transform.localScale = new Vector3(1, 1, 1); 
            }

        }
        Identification[] hard = qm.SetOfQuiz.identifications.hard;
        for (int i = 0; i < hard.Length; i++)
        {
            int currentIndex = i;
            if (hard[i].author.username == um.activeUser.username)
            {
                EIS.setValue(hard[i],2);
                EIS.index = i;
                EIS.qm = qm;
                GameObject ies = Instantiate(EIS.gameObject);
                ies.transform.SetParent(transform);
                ies.transform.localScale = new Vector3(1,1,1);
            }

        }


    }
    
    public void addNew() 
    {
        GameObject ies;
        Identification newQuiz = new Identification();
        
        switch (addInDiff.value) 
        {
            case 0:
                Array.Resize(ref qm.SetOfQuiz.identifications.easy, qm.SetOfQuiz.identifications.easy.Length + 1);
                newQuiz.author = um.activeUser;
                qm.SetOfQuiz.identifications.easy[^1] = newQuiz;
                
                EIS.setValue(qm.SetOfQuiz.identifications.easy[qm.SetOfQuiz.identifications.easy.Length-1], 0);
                EIS.index = qm.SetOfQuiz.identifications.easy.Length - 1;
                EIS.qm = qm;
                ies = Instantiate(EIS.gameObject);
                ies.transform.SetParent(transform);
                ies.transform.localScale = new Vector3(1, 1, 1);
                
                break;
            case 1:
                Array.Resize(ref qm.SetOfQuiz.identifications.medium, qm.SetOfQuiz.identifications.medium.Length + 1);
                newQuiz.author = um.activeUser;
                qm.SetOfQuiz.identifications.medium[^1] = newQuiz;
                EIS.setValue(qm.SetOfQuiz.identifications.medium[qm.SetOfQuiz.identifications.medium.Length-1], 1);
                EIS.index = qm.SetOfQuiz.identifications.medium.Length - 1;
                EIS.qm = qm;
                ies = Instantiate(EIS.gameObject);
                ies.transform.SetParent(transform);
                ies.transform.localScale = new Vector3(1, 1, 1);
               
                break;
            case 2:
                Array.Resize(ref qm.SetOfQuiz.identifications.hard, qm.SetOfQuiz.identifications.hard.Length + 1);
                newQuiz.author = um.activeUser;
                qm.SetOfQuiz.identifications.hard[^1] = newQuiz;
                EIS.setValue(qm.SetOfQuiz.identifications.hard[qm.SetOfQuiz.identifications.hard.Length-1], 2);
                EIS.index = qm.SetOfQuiz.identifications.hard.Length - 1;
                EIS.qm = qm;
                ies = Instantiate(EIS.gameObject);
                ies.transform.SetParent(transform);
                ies.transform.localScale = new Vector3(1, 1, 1);
               
                break;
        }
        
    }
    
}
