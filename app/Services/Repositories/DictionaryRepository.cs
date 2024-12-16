using app.Interfaces;
using app.Models;

namespace app.Services.Repositories;

// Репозиторий для управления словарём слов.
public class DictionaryRepository : IDictionaryRepository
{
    // Список слов в словаре.
    private readonly List<Word> _words;

    // Сервис хранения данных.
    private readonly IStorage _storage;

    // Генератор случайных чисел для получения случайного слова.
    private readonly Random _random;

    // Приватный конструктор для инициализации зависимостей 
    // "storage" - Сервис хранения данных. "words" - Начальный список слов
    private DictionaryRepository(IStorage storage, List<Word> words)
    {
        _storage = storage;
        _words = words;  // Не делать действия в конструкторе, на подумать DONE
        _random = new Random();
    }

    // Асинхронный метод для создания экземпляра DictionaryRepository с загруженными данными
    // "storage" - сервис хранения данных
    // Возвращает инициализированный DictionaryRepository
    public static async Task<DictionaryRepository> CreateAsync(IStorage storage)
    {
        var words = await storage.LoadAsync();
        return new DictionaryRepository(storage, words);
    }

    // Добавляет новое слово в словарь.
    // "word" - Слово для добавления.
    // Возвращает true, если слово успешно добавлено; иначе false.
    public bool AddWord(Word word)
    {
        if (_words.Any(w => w.English.Equals(word.English, StringComparison.OrdinalIgnoreCase)))
        {
            // Слово уже существует в словаре
            return false;
        }

        _words.Add(word);
        _storage.SaveAsync(_words);
        return true;
    }

    // Удаляет слово из словаря по его английскому названию.
    // "english" - Английское слово для удаления.
    // Возвращает true, если слово успешно удалено; иначе false.
    public bool DeleteWord(string english)
    {
        var word = _words.FirstOrDefault(w => w.English.Equals(english, StringComparison.OrdinalIgnoreCase));
        if (word != null)
        {
            _words.Remove(word);
            _storage.SaveAsync(_words);
            return true;
        }

        // Слово не найдено в словаре
        return false;
    }

    // Получает случайное слово из словаря.
    // Возвращает случайное слово, или null, если словарь пуст.
    public Word? GetRandomWord()
    {
        if (_words.Count == 0)
        {
            return null;
        }

        int index = _random.Next(_words.Count);
        return _words[index];
    }

    // Проверяет, содержит ли словарь хотя бы одно слово.
    // Возвращает true, если словарь содержит слова; иначе false.
    public bool HasWords()
    {
        return _words.Count > 0;
    }

    // Получает все слова из словаря.
    // Возвращает список всех слов в словаре.
    public List<Word> GetAllWords()
    {
        return _words;
    }
}