using System;
using UnityEngine;

public class ChooseCategory : MonoBehaviour
{
    public bool exercise;
    public IdentificationQuizScript identexecercise;
    public MultipleChoiceQuiz mcExercises;
    public QuizManager qm;
    public MultipleChoice[] mc;
    public Identification[] id;
    public PanelManager pm;
    public int difflvl;
    public int gamemode; //0 identification , 1 multiple choice
    public void difficultySelection(int difficultyLevel)
    {
        difflvl = difficultyLevel;
        if (gamemode == 0) 
        {
            switch (difficultyLevel)
            {
                case 0:
                    id = qm.SetOfQuiz.identifications.easy;
                    break;
                case 1:
                    id = qm.SetOfQuiz.identifications.medium;
                    break;
                case 2:
                    id = qm.SetOfQuiz.identifications.hard;
                    break;
            }
            mcExercises.exerciseMode = false;
            identexecercise.startGame();
            pm.ChangeSection(7);
          
        }
        else 
        {
            switch (difficultyLevel)
            {
                case 0:
                    mc = qm.SetOfQuiz.MultipleChoices.easy;
                    break;
                case 1:
                    mc = qm.SetOfQuiz.MultipleChoices.medium;
                    break;
                case 2:
                    mc = qm.SetOfQuiz.MultipleChoices.hard;
                    break;
            }
            mcExercises.exerciseMode = false;
            mcExercises.startGame();
            pm.ChangeSection(8);
            
        }
        
       
    
    }
    public void selectCat(int para) 
    {
        gamemode = para;
        if (exercise)
        {
            if (para == 0) 
            {
                pm.ChangeSection(7);
                id = qm.SetOfQuiz.identifications.easy;
                identexecercise.exerciseMode = true;
               identexecercise.startGame();
            }
            else 
            {
                pm.ChangeSection(8);
                mc= qm.SetOfQuiz.MultipleChoices.easy;
                mcExercises.exerciseMode = true;
                mcExercises.startGame();
            }
        }
        else 
        {
            pm.ChangeSection(3);
        }
        
    }
}


