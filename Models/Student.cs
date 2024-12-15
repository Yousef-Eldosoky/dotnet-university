namespace University.Models;

public class Student
{
    public required string Id { get; init; }
    public required string Name { get; set; }
    public required double GPA { get; set; }
}