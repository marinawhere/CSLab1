using app.Interfaces;
using app.Models;

namespace app.Services.WordManagement;

// Сервис для управления словами в словаре (добавление и удаление).
public class WordService : IWordService
{
    // Репозиторий словаря для управления словами.
    private readonly IDictionaryRepository _repository;

    // Сервис меню для взаимодействия с пользователем.
    private readonly IMenu _menu;

    // Инициализирует новый экземпляр класса "WordService".
    // "repository" - Репозиторий словаря.
    // "menu" - Сервис меню.
    public WordService(IDictionaryRepository repository, IMenu menu)
    {
        _repository = repository;
        _menu = menu;
    }

    // Добавляет новое слово в словарь.
    public void AddNewWord()
    {
        Console.WriteLine();
        string english = _menu.Prompt("Введите новое слово на английском: ").Trim();

        if (string.IsNullOrEmpty(english))
        {
            _menu.DisplayMessage("Слово не может быть пустым.");
            return;
        }

        string russian = _menu.Prompt("Введите перевод этого слова на русский: ").Trim();

        if (string.IsNullOrEmpty(russian))
        {
            _menu.DisplayMessage("Перевод не может быть пустым.");
            return;
        }

        int memoryLevel;
        while (true)
        {
            string levelInput = _menu.Prompt("Введите начальный уровень запоминания (от 1 до 3): ").Trim();
            if (int.TryParse(levelInput, out memoryLevel) && memoryLevel is >= 1 and <= 3)
            {
                break;
            }

            _menu.DisplayMessage("Неверный уровень. Пожалуйста, введите число от 1 до 3.");
        }

        bool success = _repository.AddWord(new Word(english, russian, memoryLevel));
        _menu.DisplayMessage(success
            ? $"Слово '{english}' успешно добавлено в словарь."
            : $"Слово '{english}' уже существует в словаре.");
    }

    // Удаляет слово из словаря.
    public void DeleteWord()
    {
        Console.WriteLine();
        string english = _menu.Prompt("Введите слово на английском, которое хотите удалить из словаря: ").Trim();

        if (string.IsNullOrEmpty(english))
        {
            _menu.DisplayMessage("Слово не может быть пустым.");
            return;
        }

        bool success = _repository.DeleteWord(english);
        _menu.DisplayMessage(success
            ? $"Слово '{english}' успешно удалено из словаря."
            : $"Слово '{english}' не найдено в словаре.");
    }
}