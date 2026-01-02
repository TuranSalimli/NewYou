namespace NewYou.Application.DTOs;

public class ToDoItemDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Note { get; set; }
    public string Priority { get; set; } 
    public string Status { get; set; }
    public DateTime? Deadline { get; set; }
    public string ProjectName { get; set; }
}