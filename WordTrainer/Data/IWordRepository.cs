using System;
using System.Collections.Generic;
using System.Text;
using WordTrainer.Models;

namespace WordTrainer.Data
{
    public interface IWordRepository
    {
        IReadOnlyList<Word> Words { get; }
        IReadOnlyList<string> Categories { get; }
        void Add(Word word);
        void Load();
        void Save();
    }
}
