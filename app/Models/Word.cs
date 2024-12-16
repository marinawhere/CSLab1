namespace app.Models;

// Представляет слово с его переводом и уровнем запоминания.
public class Word
{
    // Английское слово.
    public string English { get; set; }

    // Перевод слова на русский язык.
    public string Russian { get; set; }

    // Уровень запоминания слова (от 1 до 3).
    public int MemoryLevel { get; set; }

    // Инициализирует новый экземпляр класса "Word" с указанными значениями.
    // "english" - Английское слово.
    // "russian" - Перевод на русский язык.
    // "memoryLevel" - Уровень запоминания.
    public Word(string english, string russian, int memoryLevel)
    {
        English = english;
        Russian = russian;
        MemoryLevel = memoryLevel;
    }
}