using app.Factories;
using app.Interfaces;
using app.Services.Menus;
using app.Services.Repositories;
using app.Services.Storage;
using app.Services.Testing;
using app.Services.WordManagement;

namespace app;

// Точка входа в консольное приложение для изучения иностранных слов.
public static class Program
{
    // Метод Main, точка входа приложения.
    public static async Task Main()
    {

        // Инициализация хранилища данных словаря.
        // Убедитесь, что путь к файлу корректен для вашей операционной системы.
        IStorage storage = new JsonStorage("C:\\Users\\Marina\\source\\repos\\app\\app\\Properties\\dictionary.json");
        // Асинхронное создание репозитория словаря, который использует хранилище данных.
        IDictionaryRepository repository = await DictionaryRepository.CreateAsync(storage);
        // Создание сервиса меню для взаимодействия с пользователем через консоль.
        IMenu menu = new ConsoleMenu();
        // Создание сервиса тестирования, который использует репозиторий и меню.
        ITestService testService = new TestService(repository, menu);
        // Создание сервиса управления словами, который использует репозиторий и меню.
        IWordService wordService = new WordService(repository, menu);
        // Флаг для контроля выхода из основного цикла приложения.
        bool exit = false;
        // Определение действия для команды выхода, которое изменяет флаг exit на true.
        void ExitAction() => exit = true;
        // Инициализация фабрики команд с необходимыми зависимостями и действием выхода.
        CommandFactory commandFactory = new CommandFactory(testService, wordService, menu, ExitAction);
        // Приветственное сообщение пользователю.
        Console.WriteLine("Добро пожаловать в программу для изучения иностранных слов!");
        // Основной цикл приложения, который продолжается до тех пор, пока флаг exit не станет true.
        while (!exit)
        {
            // Отображение меню пользователю.
            menu.Show();
            // Получение выбора пользователя.
            string choice = menu.GetUserChoice();
            // Получение соответствующей команды из фабрики на основе выбора пользователя.
            ICommand command = commandFactory.GetCommand(choice);
            // Выполнение команды.
            command.Execute();
        }
    }
}