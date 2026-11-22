using System;
using System.Collections.Generic;
using System.Text;

namespace WordTrainer.Models
{
    internal class Statistics
    {
        public int Correct {  get; private set; }
        public int Wrong { get; private set; }


        public int Total => Correct + Wrong;
        public double SuccesRate => Total == 0 ? 0 : 100.0 * Correct / Total;


        public void AddCorrect() => Correct++;
        public void AddWrong() => Wrong++;
        public void Reset() { Correct = 0; Wrong = 0; }

        public override string ToString() => $"Верно: {Correct} | Ошибок: {Wrong} | Всего: {Total} | Успех: {SuccesRate:F0}%";
    }
}
