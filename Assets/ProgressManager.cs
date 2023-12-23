using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    public TMP_Text totalRating;
    public TMP_Text username;
    public progressTab ratings;
    public progressTab correct;
    public progressTab wrong;
    public UserManager um;
    public GameObject YesNoDialog;
    public void setProgress() 
    {
        //calculating the variables of player's progress
        int easycorrect = um.activeUser.score.easyRight;
        int easywrong = um.activeUser.score.easyWrong;
        int medcorrect = um.activeUser.score.mediumRight;
        int medwrong = um.activeUser.score.mediumWrong;
        int hardcorrect = um.activeUser.score.hardRight;
        int hardwrong = um.activeUser.score.hardWrong;
        int allcorrect = easycorrect + medcorrect + hardcorrect;
        int allwrong = easywrong + medwrong + hardwrong;
        float totalrating= (allcorrect + allwrong > 0) ? ((float)allcorrect / (allcorrect + allwrong)) * 100 : 0;
        float rateEasy = (easycorrect + easywrong > 0) ? ((float)easycorrect / (easycorrect + easywrong)) * 100 : 0;
        float rateMed= (medcorrect + medwrong > 0) ? ((float)medcorrect / (medcorrect + medwrong)) * 100 : 0;
        float rateHard= (hardcorrect + hardwrong > 0) ? ((float)hardcorrect / (hardcorrect + hardwrong)) * 100 : 0;
        //display the calculation for progress

        //total rating
        username.text = um.activeUser.username;
        totalRating.text = totalrating.ToString("0")+"%";
        //difficulty ratings
        ratings.easy.text = rateEasy.ToString("0") + "%";
        ratings.meduim.text = rateMed.ToString("0") + "%";
        ratings.hard.text = rateHard.ToString("0") + "%";
        // difficulty corrects
        correct.easy.text = easycorrect.ToString("0") + "";
        correct.meduim.text = medcorrect.ToString("0") + "";
        correct.hard.text = hardcorrect.ToString("0") + "";
        //difficulty wrongs
        wrong.easy.text = easywrong.ToString("0") + "";
        wrong.meduim.text = medwrong.ToString("0") + "";
        wrong.hard.text = hardwrong.ToString("0") + "";

    }
    public void deletedata() 
    {
        ConfirmationDialog dialog = Instantiate(YesNoDialog).GetComponent<ConfirmationDialog>();

        // Show a message in the dialog
        dialog.Show("All Student account, teacher account, custome quizes, and scores will be deleted and cannot be retrived. Are you sure you want to delete the data?");

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
            DataSaver.DeleteData();
            Application.Quit();
        }

        // Destroy the dialog
        Destroy(dialog.gameObject);
    }
}
