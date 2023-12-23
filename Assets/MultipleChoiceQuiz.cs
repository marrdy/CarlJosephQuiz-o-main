using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MultipleChoiceQuiz : MonoBehaviour
{
    public bool exerciseMode;
    public GameObject failedExe;
    public GameObject passedExe;
    public GameObject quizButton;
    public TMP_Text TimeDisplay;
    public Color RightColor;
    public Color WrongColor;
    public TMP_Text QuestionDisplay;
    public TMP_Text a;
    public TMP_Text b;
    public TMP_Text c;
    public TMP_Text d;
    public TMP_Text explainDisplay;
    public TMP_Text totalQuizes;
    public TMP_Text authorName;
    public TMP_Text gotScore;
    public GameObject NextTab;
    public GameObject mcPannel;
    public GameObject gameoverPanel;
    public ChooseCategory CC;
    public UserManager um;
    public ProgressManager pm;
    public PanelManager PM;
    public MultipleChoice[] multiolechoice;
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
        multiolechoice = CC.mc;
      //  GameObject msg;
        if (CC.gamemode == 1)
        {
            multiolechoice = ShuffleArray(CC.mc);
            //if (multiolechoice.Length <=9) 
            //{
            //    message.Message.text = "The Quiz is not yet available, please wait for teachers to submit their Quizes";
            //    msg = Instantiate(message.gameObject);
            //    return;
            //}
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
        a.color = Color.white;
        b.color = Color.white;
        c.color = Color.white;
        d.color = Color.white;
        if (currentQuestionIndex >= multiolechoice.Length)
        {
            FindAnyObjectByType<SMScript>().playtrack("win");
            gameEnd();
            NextTab.SetActive(false);
            return;
        }
        
        NextTab.SetActive(false);
        SetTheQuestion(currentQuestionIndex);
    }
    public void SetTheQuestion(int index)
    {

        try
        {
            QuestionDisplay.text = multiolechoice[index].description;
            explainDisplay.text = multiolechoice[index].Explanation;
            a.text = multiolechoice[index].choice1;
            b.text = multiolechoice[index].choice2;
            c.text = multiolechoice[index].choice3;
            d.text = multiolechoice[index].choice4;
            totalQuizes.text = index + "/" + multiolechoice.Length;
            authorName.text = multiolechoice[index].author.username;
        }
        catch (Exception)
        {
            gameEnd();
        }

    }
    public void gameEnd()
    {
        Debug.Log("GAME END");
        foreach (UserInfo i in um.listofUsers)
        {
            if (i.username == um.activeUser.username)
            {
                Debug.Log("user found");
                if (!i.excerciseDone)
                {
                    i.excerciseDone = accumelatedScore >= 5;
                    i.score = um.activeUser.score;
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

                    gotScore.text = accumelatedScore + "/" + multiolechoice.Length;
                    pm.setProgress();
                }
                else
                {
                    if (exerciseMode) 
                    {
                        if (accumelatedScore >= 5)
                        {
                            passedExe.SetActive(true);
                        }
                        else
                        {
                            passedExe.SetActive(true);
                        }
                        return;
                    }
                    
                    
                    
                    
                    GameRuning = false;
                    gameoverPanel.SetActive(true);
                    gotScore.text = accumelatedScore + "/"+multiolechoice.Length;
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
    public void SubmitAnswer(int ans)
    {
        if (!GameRuning) return;
        GameRuning = false;
        switch (multiolechoice[currentQuestionIndex].rightAns) 
        {
            case 0:
                a.color = RightColor;
                b.color = WrongColor;
                c.color = WrongColor;
                d.color = WrongColor;
                break;
            case 1:
                a.color = WrongColor;
                b.color = RightColor;
                c.color = WrongColor;
                d.color = WrongColor;
                break;
            case 2:
                a.color = WrongColor;
                b.color = WrongColor;
                c.color = RightColor;
                d.color = WrongColor;
                break;
            case 3:
                a.color = WrongColor;
                b.color = WrongColor;
                c.color = WrongColor;
                d.color = RightColor;
                break;
        }
        if (ans == multiolechoice[currentQuestionIndex].rightAns)
        {
            FindAnyObjectByType<SMScript>().playtrack("right");
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
                switch (multiolechoice[currentQuestionIndex].topic)
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
