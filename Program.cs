namespace todolist
{
    class Program
    {
        static void Main()
        {
            
TaskStorage storage = new TaskStorage();
TaskService service = new TaskService(storage);

bool isRunning = true;

while (isRunning)
{
    Console.Clear();

    Console.WriteLine("=== TASK TRACKER ===");
    Console.WriteLine("1. Show all tasks");
    Console.WriteLine("2. Add task");
    Console.WriteLine("3. Manage task");
    Console.WriteLine("0. Exit");
    Console.Write("\nSelect an option: ");

    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            ShowTasks();
            break;

        case "2":
            AddTask();
            break;

        case "3":
            ManageTask();
            break;

        case "0":
            isRunning = false;
            break;

        default:
            Console.WriteLine("Invalid option.");
            Pause();
            break;
    }
}

// -------------------------
// Show all tasks
// -------------------------

void ShowTasks()
{
    Console.Clear();

    IReadOnlyList<Task> tasks = service.GetAllTasks();

    Console.WriteLine("=== YOUR TASKS ===\n");

    if (tasks.Count == 0)
    {
        Console.WriteLine("No tasks yet.");
    }
    else
    {
        for (int i = 0; i < tasks.Count; i++)
        {
            Task task = tasks[i];

            string status = task.IsCompleted ? "[DONE]" : "[PENDING]";

            Console.WriteLine($"{i + 1}. {status} {task.Title}");
            Console.WriteLine($"   Planned date: {task.PlannedDate}");
            Console.WriteLine($"   Description: {task.Description ?? "None"}");
            Console.WriteLine();
        }
    }

    Pause();
}

// -------------------------
// Add a new task
// -------------------------

void AddTask()
{
    Console.Clear();

    Console.WriteLine("=== ADD TASK ===\n");

    Console.Write("Title: ");
    string title = Console.ReadLine() ?? "";

    Console.Write("Description: ");
    string? description = Console.ReadLine();

    Console.Write("Planned date (YYYY-MM-DD): ");
    string dateInput = Console.ReadLine() ?? "";

    if (!DateOnly.TryParse(dateInput, out DateOnly plannedDate))
    {
        Console.WriteLine("Invalid date format.");
        Pause();
        return;
    }

    try
    {
        service.AddTask(title, description, plannedDate);
        Console.WriteLine("\nTask added successfully.");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"\nError: {ex.Message}");
    }

    Pause();
}

// -------------------------
// Select and manage a task
// -------------------------

void ManageTask()
{
    Console.Clear();

    IReadOnlyList<Task> tasks = service.GetAllTasks();

    Console.WriteLine("=== MANAGE TASK ===\n");

    if (tasks.Count == 0)
    {
        Console.WriteLine("There are no tasks to manage.");
        Pause();
        return;
    }

    for (int i = 0; i < tasks.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {tasks[i].Title}");
    }

    Console.Write("\nSelect task number: ");

    if (!int.TryParse(Console.ReadLine(), out int selection))
    {
        Console.WriteLine("Please enter a valid number.");
        Pause();
        return;
    }

    int index = selection - 1;

    if (index < 0 || index >= tasks.Count)
    {
        Console.WriteLine("Task does not exist.");
        Pause();
        return;
    }

    Task selectedTask = tasks[index];

    bool managing = true;

    while (managing)
    {
        Console.Clear();

        Console.WriteLine($"=== {selectedTask.Title} ===");
        Console.WriteLine($"Status: {(selectedTask.IsCompleted ? "Completed" : "Pending")}");
        Console.WriteLine("\n1. Complete / Reopen");
        Console.WriteLine("2. Edit task");
        Console.WriteLine("3. Delete task");
        Console.WriteLine("0. Back");
        Console.Write("\nSelect an option: ");

        string? choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                service.SetCompletionStatus(
                    selectedTask,
                    !selectedTask.IsCompleted
                );

                Console.WriteLine("Task status updated.");
                Pause();
                break;

            case "2":
                EditTask(selectedTask);
                break;

            case "3":
                service.DeleteTask(selectedTask);
                Console.WriteLine("Task deleted.");
                Pause();
                managing = false;
                break;

            case "0":
                managing = false;
                break;

            default:
                Console.WriteLine("Invalid option.");
                Pause();
                break;
        }
    }
}

// -------------------------
// Edit an existing task
// -------------------------

void EditTask(Task task)
{
    Console.Clear();

    Console.WriteLine("=== EDIT TASK ===\n");

    Console.Write($"New title ({task.Title}): ");
    string? titleInput = Console.ReadLine();

    Console.Write($"New description ({task.Description}): ");
    string? descriptionInput = Console.ReadLine();

    Console.Write($"New planned date ({task.PlannedDate}): ");
    string? dateInput = Console.ReadLine();

    string newTitle = string.IsNullOrWhiteSpace(titleInput)
        ? task.Title
        : titleInput;

    string? newDescription = string.IsNullOrEmpty(descriptionInput)
        ? task.Description
        : descriptionInput;

    DateOnly newDate = task.PlannedDate;

    if (!string.IsNullOrWhiteSpace(dateInput))
    {
        if (!DateOnly.TryParse(dateInput, out newDate))
        {
            Console.WriteLine("Invalid date. Changes were not saved.");
            Pause();
            return;
        }
    }

    try
    {
        service.UpdateTask(task, newTitle, newDescription, newDate);
        Console.WriteLine("\nTask updated successfully.");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"\nError: {ex.Message}");
    }

    Pause();
}

// -------------------------
// Utility
// -------------------------

void Pause()
{
    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
}

        }
    }
}