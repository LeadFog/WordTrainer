using System;
using System.Collections.Generic;
using System.Text;
using WordTrainer.Models;
using System.IO;

namespace WordTrainer.Data
{
    public class TxtWordRepository : IWordRepository
    {
        private readonly string _filePath; 
        private readonly List<Item> _words = new(); 
        public IReadOnlyList<Item> Words => _words; 
        public TxtWordRepository(string filepath) => _filePath = filepath; 


        public IReadOnlyList<string> Categories => _words.Select(w => w.Category).Distinct().OrderBy(c => c).ToList();
     

        public void Add(Item word)
        {
            if (_words.Any(w => w.Text.Equals(word.Text, StringComparison.OrdinalIgnoreCase) && w.Category == word.Category))
                throw new InvalidOperationException("fdfsf");
            _words.Add(word);
        }
        private void Seed()
        {
            _words.AddRange(new Item[]
            {
            new Word("cat",   "кот",     "Животные"),
            new Word("dog",   "собака",  "Животные"),
            new Word("horse", "лошадь",  "Животные"),
            new Word("bear",  "медведь", "Животные"),
            new Word("apple", "яблоко",  "Фрукты"),
            new Word("pear",  "груша",   "Фрукты"),
            new Word("grape", "виноград","Фрукты"),
            new Word("lemon", "лимон",   "Фрукты"),
            new Word("run",   "бежать",  "Глаголы"),
            new Word("jump",  "прыгать", "Глаголы"),
            new Word("read",  "читать",  "Глаголы"),
            new Word("write", "писать",  "Глаголы"),
            new Phrase(
                    new Word("eat", "есть", "Глаголы"),
                    new Word("apple", "яблоко", "Фрукты")
                ), new Phrase(
    new Word("eat", "есть", "Глаголы"),
    new Word("pear", "грушу", "Фрукты"))
        });
            Save();
        }

        public void Load()
        {
            _words.Clear();
            if(!File.Exists(_filePath)) { Seed(); return; }

            var loaded = File.ReadAllLines(_filePath).Select(line => line.Split(';')).Where(parts => parts.Length == 3).Select(parts => new Word(parts[0], parts[1], parts[2]));
            _words.AddRange(loaded);
        }

        public void Save()
        {
            var lines = _words.Select(w => $"{w.Text};{w.Translation};{w.Category}"); 
            File.WriteAllLines( _filePath, lines);
        }
    }
}
