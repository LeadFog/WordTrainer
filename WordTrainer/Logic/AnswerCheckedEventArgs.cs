using System;
using System.Collections.Generic;
using System.Text;

namespace WordTrainer.Logic
{
    public class AnswerCheckedEventArgs : EventArgs
    {
        public bool IsCorrect { get; }
        public string UserAnswer { get; }
        public string CorrectAnswer { get; }

        public AnswerCheckedEventArgs(bool isCorrect, string userAnswer, string correctAnswer)
        {
            IsCorrect = isCorrect;
            UserAnswer = userAnswer;
            CorrectAnswer = correctAnswer;
        }
    }
}
