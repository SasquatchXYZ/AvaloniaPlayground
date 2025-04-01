using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using SimpleToDoList.Models;

namespace SimpleToDoList.Services;

public static class ToDoListFileService
{
    // This is a hard-coded path to the file.  It may not be available on every platform. In your real world App you may
    // want to make this configurable.
    private static readonly string _jsonFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Avalonia.SimpleToDoList", "MyToDoList.txt");

    /// <summary>
    /// Stores the given items into a file on disc
    /// </summary>
    /// <param name="itemsToSave">The items to save</param>
    public static async Task SaveToFileAsync(IEnumerable<ToDoItem> itemsToSave)
    {
        // Ensure all directories exists
        Directory.CreateDirectory(Path.GetDirectoryName(_jsonFileName)!);

        // We use a FileStream to write all items to disc
        await using var fs = File.Create(_jsonFileName);
        await JsonSerializer.SerializeAsync(fs, itemsToSave);
    }
}

