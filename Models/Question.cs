namespace University.Models;

public class Question
{
    public int Id { get; init; }
    public required int CourseId { get; init; }
    public required string StudentId { get; init; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public DateTime CreatedAt { get; init; } = DateTime.Now;
    public List<Answer> Answers { get; init; } = [];
}