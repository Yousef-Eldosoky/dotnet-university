namespace University.Models;

public class Answer
{
    public int Id { get; init; }
    public required int QuestionId { get; init; }
    public required string ProfessorId { get; init; }
    public required string Content { get; set; }
    public DateTime CreatedAt { get; init; } = DateTime.Now;
}