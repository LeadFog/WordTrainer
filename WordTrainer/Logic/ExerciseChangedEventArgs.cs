using WordTrainer.Exercises;

namespace WordTrainer.Logic;

public class ExerciseChangedEventArgs : EventArgs
{
    public Exercise Exercise { get; }
    public ExerciseChangedEventArgs(Exercise e) => Exercise = e;
}
