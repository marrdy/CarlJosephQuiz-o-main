using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MultipleChoiceEditor : MonoBehaviour
{
    public UserManager um;
    public QuizManager qm;
    public MultipleChoiceEditBox MCEB;
    public TMP_Dropdown addInDiff;
    
    public void LoadQuizes()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
       
            qm.SetOfQuiz = DataSaver.LoadQuiz(qm.SetOfQuiz);

       
           
     
        MultipleChoice[] easy = qm.SetOfQuiz.MultipleChoices.easy;
        for (int i = 0; i < easy.Length; i++)
        {
            
            if (easy[i].author.username == um.activeUser.username)
            {
                MCEB.setValue(easy[i], 0);
                MCEB.index = i;
                MCEB.qm = qm;
                MCEB.difficulty = 0;
                GameObject ies = Instantiate(MCEB.gameObject);
                ies.transform.SetParent(transform);
                ies.transform.localScale = new Vector3(1, 1, 1);

            }

        }
        MultipleChoice[] medium = qm.SetOfQuiz.MultipleChoices.medium;
        for (int i = 0; i < medium.Length; i++)
        {
            
            if (medium[i].author.username == um.activeUser.username)
            {
                MCEB.setValue(medium[i], 1);
                MCEB.index = i;
                MCEB.qm = qm;
                MCEB.difficulty = 1;
                GameObject ies = Instantiate(MCEB.gameObject);

                ies.transform.SetParent(transform);
                ies.transform.localScale = new Vector3(1, 1, 1);

            }

        }
        MultipleChoice[] hard = qm.SetOfQuiz.MultipleChoices.hard;
        for (int i = 0; i < hard.Length; i++)
        {
            
            if (hard[i].author.username == um.activeUser.username)
            {
                MCEB.setValue(hard[i], 2);
                MCEB.index = i;
                MCEB.qm = qm;
                MCEB.difficulty = 2;
                GameObject ies = Instantiate(MCEB.gameObject);
                ies.transform.SetParent(transform);
                ies.transform.localScale = new Vector3(1, 1, 1);
            }

        }


    }
    public void addNew()
    {
        GameObject ies;
        MultipleChoice newQuiz = new MultipleChoice();

        switch (addInDiff.value)
        {
            case 0:
                Array.Resize(ref qm.SetOfQuiz.MultipleChoices.easy, qm.SetOfQuiz.MultipleChoices.easy.Length + 1);
                newQuiz.author = um.activeUser;
                qm.SetOfQuiz.MultipleChoices.easy[^1] = newQuiz;

                MCEB.setValue(qm.SetOfQuiz.MultipleChoices.easy[qm.SetOfQuiz.MultipleChoices.easy.Length - 1], 0);
                MCEB.index = qm.SetOfQuiz.MultipleChoices.easy.Length - 1;
                MCEB.qm = qm;
                ies = Instantiate(MCEB.gameObject);
                ies.transform.SetParent(transform);
                ies.transform.localScale = new Vector3(1, 1, 1);

                break;
            case 1:
                Array.Resize(ref qm.SetOfQuiz.MultipleChoices.medium, qm.SetOfQuiz.MultipleChoices.medium.Length + 1);
                newQuiz.author = um.activeUser;
                qm.SetOfQuiz.MultipleChoices.medium[^1] = newQuiz;
                MCEB.setValue(qm.SetOfQuiz.MultipleChoices.medium[qm.SetOfQuiz.MultipleChoices.medium.Length - 1], 1);
                MCEB.index = qm.SetOfQuiz.MultipleChoices.medium.Length - 1;
                MCEB.qm = qm;
                ies = Instantiate(MCEB.gameObject);
                ies.transform.SetParent(transform);
                ies.transform.localScale = new Vector3(1, 1, 1);

                break;
            case 2:
                Array.Resize(ref qm.SetOfQuiz.MultipleChoices.hard, qm.SetOfQuiz.MultipleChoices.hard.Length + 1);
                newQuiz.author = um.activeUser;
                qm.SetOfQuiz.MultipleChoices.hard[^1] = newQuiz;
                MCEB.setValue(qm.SetOfQuiz.MultipleChoices.hard[qm.SetOfQuiz.MultipleChoices.hard.Length - 1], 2);
                MCEB.index = qm.SetOfQuiz.MultipleChoices.hard.Length - 1;
                MCEB.qm = qm;
                ies = Instantiate(MCEB.gameObject);
                ies.transform.SetParent(transform);
                ies.transform.localScale = new Vector3(1, 1, 1);

                break;
        }

    }
}
