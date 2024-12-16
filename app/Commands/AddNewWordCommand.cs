using app.Interfaces;

namespace app.Commands;


// Команда для добавления нового слова в словарь.
public class AddNewWordCommand : ICommand
{
    // Сервис для управления словами в словаре.
    private readonly IWordService _wordService;

    // Конструктор команды AddNewWordCommand.
    // "wordService" - Сервис для управления словами.
    public AddNewWordCommand(IWordService wordService)
    {
        _wordService = wordService;
    }

    // Выполняет команду добавления нового слова.
    public void Execute()
    {
        _wordService.AddNewWord();
    }
}