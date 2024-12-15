namespace University.Models.DTOs;

public class AnswerDto
{
    public required int QuestionId { get; init; }
    public required string Content { get; init; }
}