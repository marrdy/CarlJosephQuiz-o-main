using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class EditIdentificationScript : MonoBehaviour
{
    public Identification ident;
    public UserInfo author;
    public TMP_InputField explain;
    public TMP_InputField descripting;
    public TMP_InputField answer;
    public GameObject YesNoDialog;
    public int difficulty;
    public TMP_Dropdown topic;
    public QuizManager qm;
    public int index;
    public TMP_Text textdiff;
    public PromptMessageScript msg;
    public void setValue(Identification Setident,int diff) 
   {
        descripting.text = Setident.description;
        topic.value = Setident.topic;
        answer.text = Setident.rightAnswer;
        explain.text = Setident.Explanation;
        author = Setident.author;
        difficulty = diff;
        switch (diff) 
        {
            case 0:
                textdiff.text = "Easy";
                break;
            case 1:
                textdiff.text = "Average";
                break;
            case 2:
                textdiff.text = "Hard";
                break;
        }
   }
    public void save() 
    {
        ident.description = descripting.text;
        ident.rightAnswer = answer.text;
        ident.topic = topic.value;
        ident.author = author;
        ident.Explanation = explain.text;
        try 
        {
        
        }
        catch (Exception) 
        {
            switch (difficulty)
            {
                case 0:
                    qm.SetOfQuiz.identifications.easy = RemoveAtIndex(new List<Identification>(qm.SetOfQuiz.identifications.easy), index);
                    break;
                case 1:
                    qm.SetOfQuiz.identifications.medium = RemoveAtIndex(new List<Identification>(qm.SetOfQuiz.identifications.medium), index);
                    break;
                case 2:
                    qm.SetOfQuiz.identifications.hard = RemoveAtIndex(new List<Identification>(qm.SetOfQuiz.identifications.hard), index);
                    break;
            }
        }
        
        switch (difficulty) 
        {
            case 0:
                qm.SetOfQuiz.identifications.easy[index] = ident;
                break;
            case 1:
                qm.SetOfQuiz.identifications.medium[index] = ident;
                break;
            case 2:
                qm.SetOfQuiz.identifications.hard[index] = ident;
                break;
        }

        DataSaver.SaveQuiz(qm.SetOfQuiz);
        GameObject msgprmt = Instantiate(msg.gameObject);
        msgprmt.GetComponent<PromptMessageScript>().message.text = "Quiz Saved successfully";
    }
    public void delete()
    {
        ConfirmationDialog dialog = Instantiate(YesNoDialog).GetComponent<ConfirmationDialog>();

        // Show a message in the dialog
        dialog.Show("Are you sure you want to remove this Question?");

        // Check the result of the dialog
        StartCoroutine(WaitForConfirmationAndRemove(dialog));
    }
    private IEnumerator WaitForConfirmationAndRemove(ConfirmationDialog dialog)
    {
        while (!dialog.GetConfirmationResult())
        {
            yield return null; // Wait for user input
        }

        // Process removal based on the confirmation result
        if (dialog.GetConfirmationResult())
        {
            RemoveQuestionAtIndex();
        }

        Destroy(dialog.gameObject);
    }
    private void RemoveQuestionAtIndex()
    {
        switch (difficulty)
        {
            case 0:
                qm.SetOfQuiz.identifications.easy = RemoveAtIndex(new List<Identification>(qm.SetOfQuiz.identifications.easy), index);
                break;
            case 1:
                qm.SetOfQuiz.identifications.medium= RemoveAtIndex(new List<Identification>(qm.SetOfQuiz.identifications.medium), index);
                break;
            case 2:
                qm.SetOfQuiz.identifications.hard= RemoveAtIndex(new List<Identification>(qm.SetOfQuiz.identifications.hard), index);
                break;
        }

        DataSaver.SaveQuiz(qm.SetOfQuiz);
        GameObject msgprmt = Instantiate(msg.gameObject);
        msgprmt.GetComponent<PromptMessageScript>().message.text = "Quiz deleted successfully";
        Destroy(gameObject);
    }
    private Identification[] RemoveAtIndex(List<Identification> list, int index)
    {
        if (index >= 0 && index < list.Count)
        {
            list.RemoveAt(index);      
        }
        return list.ToArray();
    }
}
