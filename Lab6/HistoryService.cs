using System;

namespace Lab6;

public class HistoryService
{
    private readonly string _filePath;
    private string[] _history;
    private int _currentRow = 0;

    public HistoryService(string filePath)
    {
        if (!File.Exists(filePath)) File.Create(filePath);

        _filePath = filePath;
        _history = File.ReadAllLines(filePath);
        _currentRow = _history.Length - 1;
    }

    public void Add(string expression)
    {
        var history = _history.ToList();
        history.Add(expression);
        File.WriteAllLines(_filePath, history);
        _history = history.ToArray();
        _currentRow = _history.Length - 1;
    }

    public string GetPrevious()
    {
        return _currentRow == 0 ? "Error" : _history[--_currentRow];
    }

    public string GetCurrent()
    {
        return _history.Length == 0 ? "Error" : _history[_currentRow];
    }

    public string GetNext()
    {
        return _currentRow == _history.Length - 1 || _history.Length == 0 ? "Error" : _history[++_currentRow];
    }
}