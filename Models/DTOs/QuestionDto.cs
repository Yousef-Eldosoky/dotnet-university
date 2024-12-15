namespace University.Models.DTOs;

public class QuestionDto
{
    public required int CourseId { get; init; }
    public required string Title { get; set; }
    public required string Content { get; set; }
}