using Find_A_Tutor.Infrastructure.Commands.PrivateLesson;
using Find_A_Tutor.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Find_A_Tutor.Api.Controllers
{
    [Route("[controller]")]
    public class PrivateLessonController : ApiControllerBase
    {
        private readonly IPrivateLessonService _privateLessonService;
        public PrivateLessonController(IPrivateLessonService privateLessonService)
        {
            _privateLessonService = privateLessonService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string description)
        {
            var privateLessons = await _privateLessonService.BrowseAsync(description);

            return Json(privateLessons);
        }

        //todo: get by subject
        //[HttpGet("/subject/{name}")]
        //public async Task<IActionResult> GetBySubject(string name)
        //{
        //    var privateLessons = await _privateLessonService.GetAsyncBySubject(name);

        //    return Json(privateLessons);
        //}

        [HttpGet("{privateLessonId}")]
        public async Task<IActionResult> Get(Guid privateLessonId)
        {
            var privateLessonResult = await _privateLessonService.GetAsync(privateLessonId);
            return !privateLessonResult.IsSuccess ? NotFound() : (IActionResult)Json(privateLessonResult);
        }

        [HttpPost]
        [Authorize(Policy = "HasStudentRole")]
        public async Task<IActionResult> Post([FromBody]CreatePrivateLesson command)
        {
            command.PrivateLessonId = Guid.NewGuid();
            var createdResult = await _privateLessonService.CreateAsync(command.PrivateLessonId, UserId, command.RelevantTo, command.Description, command.Subject);

            return createdResult.IsSuccess ? Created($"/PrivateLesson/{command.PrivateLessonId}", null) : (IActionResult)Json(createdResult);
        }

        [HttpPut("{privateLessonId}")]
        [Authorize(Policy = "HasStudentRole")]
        public async Task<IActionResult> Put(Guid privateLessonId, [FromBody]UpdatePrivateLesson command)
        {
            var result = await _privateLessonService.UpdateAsync(privateLessonId, command.RelevantTo, command.Description, command.Subject);

            return result.IsSuccess ? NoContent() : (IActionResult)Json(result);
        }

        [HttpDelete("{privateLessonId}")]
        [Authorize(Policy = "HasAdminRole")]
        public async Task<IActionResult> Delete(Guid privateLessonId)
        {
            var result = await _privateLessonService.DeleteAsync(privateLessonId);

            return result.IsSuccess ? NoContent() : (IActionResult)Json(result);
        }

        [HttpPut("/assign/{privateLessonId}")]
        [Authorize(Policy = "HasTutorRole")]
        public async Task<IActionResult> AssignTutor(Guid privateLessonId)
        {
            var result = await _privateLessonService.AssignTutor(privateLessonId, UserId);

            return result.IsSuccess ? NoContent() : (IActionResult)Json(result);
        }

        [HttpPut("/unassign/{privateLessonId}")]
        [Authorize(Policy = "HasTutorRole")]
        public async Task<IActionResult> RemoveAssignedTutor(Guid privateLessonId)
        {
            var result = await _privateLessonService.RemoveAssignedTutor(privateLessonId, UserId);

            return result.IsSuccess ? NoContent() : (IActionResult)Json(result);
        }
    }
}
