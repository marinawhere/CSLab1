using app.Interfaces;
using app.Models;

namespace app.Services.Testing;

// Сервис для проведения тестирования знаний пользователя.
public class TestService : ITestService
{
    // Репозиторий словаря для управления словами.
    private readonly IDictionaryRepository _repository;

    // Сервис меню для взаимодействия с пользователем.
    private readonly IMenu _menu;

    // Инициализирует новый экземпляр класса "TestService".
    // "repository" - Репозиторий словаря.
    // "menu" - Сервис меню.
    public TestService(IDictionaryRepository repository, IMenu menu)
    {
        _repository = repository;
        _menu = menu;
    }


    // Запускает процесс тестирования знаний пользователя.
    public void StartTesting()
    {
        if (!_repository.HasWords())
        {
            _menu.DisplayMessage("Словарь пуст. Добавьте слова для начала тестирования.");
            return;
        }

        _menu.DisplayMessage("Начинаем тестирование! Введите 'exit' для выхода в главное меню.");
        while (true)
        {
            Word? word = _repository.GetRandomWord();
            if (word == null)
            {
                _menu.DisplayMessage("Словарь пуст. Добавьте слова для продолжения тестирования.");
                break;
            }

            string userAnswer = _menu.Prompt($"Переведите слово: {word.English}\nВаш ответ: ").Trim();

            if (userAnswer.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                // Пользователь решил выйти из тестирования
                break;
            }

            _menu.DisplayMessage(userAnswer.Equals(word.Russian, StringComparison.OrdinalIgnoreCase)
                ? "Правильно!"
                : $"Неправильно. Правильный ответ: {word.Russian}");

            _menu.DisplayMessage(""); // Пустая строка для разделения вопросов
        }
    }
}