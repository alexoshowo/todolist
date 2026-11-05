class Task
{
    public string Title{get; set;}
    public string? Notes{get; set;}
    public DateTimeOffset? TimeDue{get; set;}
    public DateOnly? PlannedTime{get; set;}
    public DateTimeOffset CreatedAt{get;} 
    public bool IsCompleted {get; private set;}
    public void Complete()
    {
        IsCompleted = true;
    }
    public void Reopen()
    {
        IsCompleted = false;
    }
   public Task(string title, string? notes){
    if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty");
       Title = title;
       Notes = notes; 
       CreatedAt = DateTimeOffset.Now;
   }
}