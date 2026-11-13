class Task
{
    public string Title{get; set;}
    public string? Description{get; set;}
    public DateTimeOffset? TimeDue{get; set;}
    public DateOnly PlannedTime{get; set;}
    public DateTimeOffset CreatedAt{get;} 
    public bool IsCompleted {get; private set;}
   public void SetCompletionStatus(bool isCompleted)
        {
            IsCompleted = isCompleted;
        }
   public Task(string title, string? description, DateOnly plannedDate){
    if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty");
       Title = title;
       Description = description; 
       CreatedAt = DateTimeOffset.Now;
       PlannedTime = plannedDate;
   }
}