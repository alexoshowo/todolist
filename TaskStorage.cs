using System.Linq;
class TaskStorage
{
   private readonly List<Task> _tasks = [];
   public IReadOnlyList<Task> Tasks => _tasks.AsReadOnly();
   public void Add(Task task) => _tasks.Add(task);
   public bool Remove(Task task) => _tasks.Remove(task);
   public int Count => _tasks.Count;
   public void Clear() => _tasks.Clear();
   public Task? GetTaskAt(int index)
    {
        if(index < 0 || index >= _tasks.Count)
        {
            return null;
        }
        return _tasks[index];
    }
   public Task? FindTask(string title)
   {
      foreach (Task task in _tasks)
      {
         if (task.Title == title)
         {
               return task;
         }
      }

      return null;
   }
   public bool ContainsTask(string title)
{
    foreach (Task task in _tasks)
    {
        if (task.Title == title)
        {
            return true;
        }
    }

    return false;
}
}
