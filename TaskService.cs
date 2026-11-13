class TaskService
{
    private readonly Task _task;
    private readonly TaskStorage _storage;

    public bool AddTask(string title, string? description, DateOnly plannedDate)
    {   
        if (string.IsNullOrWhiteSpace(title))
        {
                throw new ArgumentException("Title cannot be empty");
                return false;
        }
                
        Task task = new Task(title, description, plannedDate);
        _storage.Add(task);
    }
}