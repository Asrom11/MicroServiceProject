using Api.Controllers.Feedback;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Api.Controllers;

[ApiController]
[Route("api/feedback")]
public class FeedbackController: ControllerBase
{
    private readonly IFeedbackService _feedbackService;
    public FeedbackController(IFeedbackService feedbackService)
    {
        _feedbackService = feedbackService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetFeedbackByVacancy([FromQuery] Guid vacancyId)
    {
        var res = await _feedbackService.GetFeedbackByVacancyId(vacancyId);
        return Ok(res);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateFeedback([FromBody] CreateFeedbackRequest feedback)
    {
        var res = await _feedbackService.CreateFeedback(new VacancyFeedback()
        {
            ApplicantId = feedback.UserId,
            VacancyId = feedback.VacancyId,
            Comment = feedback.Comment,
            Rating = feedback.Rating,
            SubmittedDate = DateTime.Now.ToUniversalTime()
        });
        return Ok();  
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateFeedback([FromBody] UpdateFeedbackRequest feedback)
    {
        await _feedbackService.UpdateFeedback(feedback.FeedBackId,new VacancyFeedback()
        {
            ApplicantId = feedback.UserId,
            VacancyId = feedback.UserId,
            Comment = feedback.Comment,
            Rating = feedback.Rating
        });
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteFeedback(int id)
    {
        return Ok();
    }
}