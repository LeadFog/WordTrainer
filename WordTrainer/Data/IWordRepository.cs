using System;
using System.Collections.Generic;
using System.Text;
using WordTrainer.Models;

namespace WordTrainer.Data
{
    public interface IWordRepository
    {
        IReadOnlyList<Item> Words { get; }
        IReadOnlyList<string> Categories { get; }
        void Add(Item word);
        void Load();
        void Save();
    }
}
