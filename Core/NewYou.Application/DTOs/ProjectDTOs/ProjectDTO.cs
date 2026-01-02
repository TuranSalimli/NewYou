namespace NewYou.Application.DTOs.ProjectDTOs;

public class ProjectDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? ColorHex { get; set; }
    public int ToDoItemsCount { get; set; }
    public DateTime CreatedDate { get; set; }
}