using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using University.Data;
using University.Models;
using University.Models.DTOs;

namespace University.Endpoints;

public static class QaEndpoint
{
    public static RouteGroupBuilder MapQaEndpoint(this IEndpointRouteBuilder endpoints)
    {
        RouteGroupBuilder group = endpoints.MapGroup("/qa").WithParameterValidation().RequireAuthorization();
        group.MapGet("/questions", async (ApplicationDbContext dbContext) => await dbContext.Questions.Include(q => q.Answers).AsNoTracking().ToArrayAsync());
        
        group.MapPost("/questions", [EndpointSummary("Post the question")] [EndpointName("Question post")] [EndpointDescription("This endpoint is to post a question if you a valid user.")] async Task<Results<NotFound, Created>> ([FromBody] QuestionDto questionBody, ApplicationDbContext dbContext, UserManager<IdentityUser> userManager, ClaimsPrincipal claimsPrincipal) =>
        {
            if (await userManager.GetUserAsync(claimsPrincipal) is not { } user)
            {
                return TypedResults.NotFound();
            }
            Question question = new Question
            {
                CourseId = questionBody.CourseId,
                StudentId = user.Id,
                Title = questionBody.Title,
                Content = questionBody.Content,
            };
            dbContext.Questions.Add(question);
            await dbContext.SaveChangesAsync();
            return TypedResults.Created();
        });

        group.MapPost("/answers", async Task<Results<NotFound, Created>> ([FromBody] AnswerDto answerBody, ApplicationDbContext dbContext, UserManager<IdentityUser> userManager, ClaimsPrincipal claimsPrincipal) =>
        {
            if (await userManager.GetUserAsync(claimsPrincipal) is not { } user)
            {
                return TypedResults.NotFound();
            }
            Question? question = await dbContext.Questions.FindAsync(answerBody.QuestionId);
            if(question is null) return TypedResults.NotFound();
            Answer answer = new Answer
            {
                QuestionId = answerBody.QuestionId,
                ProfessorId = user.Id,
                Content = answerBody.Content,
            };
            dbContext.Answers.Add(answer);
            await dbContext.SaveChangesAsync();
            return TypedResults.Created($"qa/answers/{answer.Id}");
        });
        
        group.MapGet("/answers", async ([FromQuery] int? questionId, ApplicationDbContext dbContext) =>
        {
            if (questionId is null)
            {
                return TypedResults.Ok(await dbContext.Answers.AsNoTracking().ToArrayAsync());
            }
            Answer[] answer = await dbContext.Answers.Where(a => a.QuestionId == questionId).AsNoTracking().ToArrayAsync();
            return TypedResults.Ok(answer);
        });

        group.MapDelete("/questions/{questionId:int}", async Task<Results<NotFound, NoContent>> (int questionId, ApplicationDbContext dbContext) =>
        {
            Question? question = await dbContext.Questions.Include(q => q.Answers).FirstOrDefaultAsync(q => q.Id == questionId);
            if(question is null) return TypedResults.NotFound();
            dbContext.Questions.Remove(question);
            dbContext.Answers.RemoveRange(question.Answers);
            await dbContext.SaveChangesAsync();
            return TypedResults.NoContent();
        });
        
        group.MapDelete("/answers/{answerId:int}", async Task<Results<NotFound, NoContent>> (int answerId, ApplicationDbContext dbContext) =>
        {
            Answer? answer = await dbContext.Answers.FindAsync(answerId);
            if(answer is null) return TypedResults.NotFound();
            dbContext.Answers.Remove(answer);
            await dbContext.SaveChangesAsync();
            return TypedResults.NoContent();
        });
        
        return group;
    }
}