using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IdentificationQuizScript : MonoBehaviour
{
    public bool exerciseMode;
    public GameObject failedExe;
    public GameObject passedExe;
    public GameObject quizButton;
    public TMP_Text TimeDisplay;
    public Color RightColor;
    public Color WrongColor;
    public TMP_Text QuestionDisplay;
    public TMP_InputField answerField;
    public TMP_Text InputAnswerText;
    public TMP_Text explainDisplay;
    public TMP_Text totalQuizes;
    public TMP_Text authorName;
    public TMP_Text gotScore;
    public TMP_Text DifficultyDisplay;
    
    public GameObject NextTab;
    public GameObject identificationPanel;
    public GameObject gameoverPanel;
    public ChooseCategory CC;
    public UserManager um;
    public ProgressManager pm;
    public PanelManager PM;
    public Identification[] identifications;
    int currentQuestionIndex;
    int accumelatedScore;
    bool GameRuning;
    public float StartingTime;
    public float currentTime;
    public BoxedMessage message;
    // Update is called once per frame
    void Update()
    {
        if (GameRuning)
        {
            currentTime -= Time.deltaTime;
            UpdateTimeDisplay();
            if (currentTime <= 0)
            {
                gameEnd();
            }
        }
        
    }

    void UpdateTimeDisplay()
    {
        // Convert seconds to minutes and seconds
        int minutes = Mathf.FloorToInt(currentTime / 60F);
        int seconds = Mathf.FloorToInt(currentTime - minutes * 60);

        // Display the time in MM:SS format
        TimeDisplay.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void startGame() 
    {
        if (!exerciseMode) 
        {
            switch (CC.difflvl)
            {
                case 0:
                    DifficultyDisplay.text = "Easy";
                    break;
                case 1:
                    DifficultyDisplay.text = "Medium";
                    break;
                case 2:
                    DifficultyDisplay.text = "Hard";
                    break;

            }

        }
        else 
        {
            DifficultyDisplay.text = "Exercise Mode";
        }
       
       
        identifications = CC.id;
       // GameObject msg;
        if (CC.gamemode == 0)
        {
            identifications = ShuffleArray(CC.id);
            //if (identifications.Length <= 9)
            //{
            //    message.Message.text = "The Quiz is not yet available, please wait for teachers to submit their Quizes";
            //    msg = Instantiate(message.gameObject);
            //    return;
            //}
            identifications = ShuffleArray(CC.id);
            currentTime = StartingTime;
            GameRuning = true;
            PM.ChangeSection(CC.gamemode == 0 ? 7 : 8);
            SetTheQuestion(0);
    
        }
     
    }
    public void NextQuestion() 
    {
        GameRuning = true;
        currentQuestionIndex++;
        if(currentQuestionIndex >= identifications.Length) 
        {
            FindAnyObjectByType<SMScript>().playtrack("win");
            gameEnd();
            return;
        }
        InputAnswerText.color = Color.black;
        NextTab.SetActive(false);
        SetTheQuestion(currentQuestionIndex);
    }
    public void SetTheQuestion(int index) 
    {

        try 
        {
            QuestionDisplay.text = identifications[index].description;
            explainDisplay.text = identifications[index].Explanation;
           totalQuizes.text = index + "/" + identifications.Length;
            authorName.text = identifications[index].author.username;
        }
        catch (Exception) 
        {
            gameEnd();
        }
       
    }
    public void gameEnd() 
    {
        
        foreach(UserInfo i in um.listofUsers) 
        {
            if(i.username == um.activeUser.username) 
            {
                if (!i.excerciseDone) 
                {
                    i.excerciseDone = accumelatedScore >= 5;
                    DataSaver.SaveUserInfo(um.listofUsers);
                    GameRuning = false;
                    if (accumelatedScore >= 5) 
                    {
                        passedExe.SetActive(true);
                        quizButton.SetActive(true);
                    }
                    else 
                    {
                        failedExe.SetActive(true);
                    }
                   
                    gotScore.text = accumelatedScore + "/" + identifications.Length;
                    pm.setProgress();
                }
                else 
                {
                    if (exerciseMode) return;
                    i.score = um.activeUser.score;
                    DataSaver.SaveUserInfo(um.listofUsers);
                    GameRuning = false;
                    gameoverPanel.SetActive(true);
                    gotScore.text = accumelatedScore + "/"+identifications.Length;
                    pm.setProgress();
                    return;
                }
              
            }
        }
        
    }
    public void closeQuiz() 
    {
        accumelatedScore = 0;
        gameoverPanel.SetActive(false);
        passedExe.SetActive(false);
        failedExe.SetActive(false);
        currentQuestionIndex = 0;
    }
    public void SubmitAnswer() 
    {
        if (!GameRuning) return;
        GameRuning = false;
        if (answerField.text == identifications[currentQuestionIndex].rightAnswer) 
        {
            FindAnyObjectByType<SMScript>().playtrack("right");
            InputAnswerText.color = RightColor;
            accumelatedScore++;
            if (!exerciseMode) 
            {
                // record the score if it is not exercise mode
                switch (CC.difflvl)
                {
                    case 0:
                        um.activeUser.score.easyRight++;
                        break;
                    case 1:
                        um.activeUser.score.mediumRight++;
                        break;
                    case 2:
                        um.activeUser.score.hardRight++;
                        break;
                }
            }
            
        }
        else 
        {
            FindAnyObjectByType<SMScript>().playtrack("wrong");
            InputAnswerText.color = WrongColor;
            // record the wrong if it is not exercise mode
            if (!exerciseMode) 
            {
                switch (CC.difflvl)
                {
                    case 0:
                        um.activeUser.score.easyWrong++;
                        break;
                    case 1:
                        um.activeUser.score.mediumWrong++;
                        break;
                    case 2:
                        um.activeUser.score.hardWrong++;
                        break;
                }
                switch (identifications[currentQuestionIndex].topic)
                {
                    case 0:
                        um.activeUser.score.phrasesWrong++;
                        break;
                    case 1:
                        um.activeUser.score.PluralWrong++;
                        break;
                    case 2:
                        um.activeUser.score.idiomWrong++;
                        break;
                }
            }

        }
           
        NextTab.SetActive(true);
    }
    public T[] ShuffleArray<T>(T[] array)
    {
        int n = array.Length;
        System.Random random = new System.Random();

        for (int i = n - 1; i > 0; i--)
        {
            int randomIndex = random.Next(0, i + 1);

            // Swap elements
            T temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }

        return array;
    }
    public void closing()
    {
        gameEnd();
        closeQuiz();
    }
}
