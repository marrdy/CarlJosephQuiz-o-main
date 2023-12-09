using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MultipleChoiceEditBox : MonoBehaviour
{
    public MultipleChoice mc;
    public UserInfo author;
    public TMP_InputField descripting;
    public TMP_InputField explain;
    public TMP_InputField a;
    public TMP_InputField b;
    public TMP_InputField c;
    public TMP_InputField d;
    public GameObject YesNoDialog;
    public TMP_Dropdown difficulty;
    public TMP_Dropdown rightchoice;
    public TMP_Dropdown topic;
    public QuizManager qm;
    public int index;
    public void setValue(MultipleChoice setmc, int diff)
    {
        descripting.text = setmc.description;
        a.text = setmc.choice1;
        b.text = setmc.choice2;
        c.text = setmc.choice3;
        d.text = setmc.choice4;
        topic.value = setmc.topic;
        explain.text = setmc.Explanation;
        author = setmc.author;
        rightchoice.value = setmc.rightAns;
        difficulty.value = diff;
    }
    public void save()
    {
        mc.description = descripting.text;
        mc.Explanation = explain.text;
        mc.rightAns = rightchoice.value;
        mc.topic = topic.value;
        mc.choice1 = a.text;
        mc.choice2 = b.text;
        mc.choice3 = c.text;
        mc.choice4 = d.text;
        mc.author = author;
        try
        {

        }
        catch (Exception)
        {
            switch (difficulty.value)
            {
                case 0:
                    qm.SetOfQuiz.MultipleChoices.easy = RemoveAtIndex(new List<MultipleChoice>(qm.SetOfQuiz.MultipleChoices.easy), index);
                    break;
                case 1:
                    qm.SetOfQuiz.MultipleChoices.medium = RemoveAtIndex(new List<MultipleChoice>(qm.SetOfQuiz.MultipleChoices.medium), index);
                    break;
                case 2:
                    qm.SetOfQuiz.MultipleChoices.hard = RemoveAtIndex(new List<MultipleChoice>(qm.SetOfQuiz.MultipleChoices.hard), index);
                    break;
            }
        }

        switch (difficulty.value)
        {
            case 0:
                qm.SetOfQuiz.MultipleChoices.easy[index] = mc;
                break;
            case 1:
                qm.SetOfQuiz.MultipleChoices.medium[index] = mc;
                break;
            case 2:
                qm.SetOfQuiz.MultipleChoices.hard[index] = mc;
                break;
        }

        DataSaver.SaveQuiz(qm.SetOfQuiz);
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
        switch (difficulty.value)
        {
            case 0:
                qm.SetOfQuiz.MultipleChoices.easy = RemoveAtIndex(new List<MultipleChoice>(qm.SetOfQuiz.MultipleChoices.easy), index);
                break;
            case 1:
                qm.SetOfQuiz.MultipleChoices.medium = RemoveAtIndex(new List<MultipleChoice>(qm.SetOfQuiz.MultipleChoices.medium), index);
                break;
            case 2:
                qm.SetOfQuiz.MultipleChoices.hard = RemoveAtIndex(new List<MultipleChoice>(qm.SetOfQuiz.MultipleChoices.hard), index);
                break;
        }

        DataSaver.SaveQuiz(qm.SetOfQuiz);
        Destroy(gameObject);
    }
    private MultipleChoice[] RemoveAtIndex(List<MultipleChoice> list, int index)
    {
        if (index >= 0 && index < list.Count)
        {
            list.RemoveAt(index);
        }
        return list.ToArray();
    }
}
