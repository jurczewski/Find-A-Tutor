using Find_A_Tutor.Infrastructure.Commands.SchoolSubject;
using Find_A_Tutor.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Find_A_Tutor.Api.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "HasAdminRole")]
    public class SchoolSubjectController : ApiControllerBase
    {
        private readonly ISchoolSubjectService _schoolSubjectService;

        public SchoolSubjectController(ISchoolSubjectService schoolSubjectService)
        {
            _schoolSubjectService = schoolSubjectService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get(string name)
        {
            var schoolSubject = await _schoolSubjectService.BrowseAsync(name);

            return Json(schoolSubject);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateSchoolSubject command)
        {
            command.SchoolSubjectId = Guid.NewGuid();
            await _schoolSubjectService.CreateAsync(command.SchoolSubjectId, command.Name);

            return Created($"/PrivateLesson/{command.Name}", null);
        }

        [HttpPut("{schoolSubjectId}")]
        public async Task<IActionResult> Put(Guid schoolSubjectId, [FromBody]UpdateSchoolSubject command)
        {
            await _schoolSubjectService.UpdateAsync(schoolSubjectId, command.Name);

            return NoContent();
        }

        [HttpDelete("{schoolSubjectId}")]
        public async Task<IActionResult> Delete(Guid schoolSubjectId)
        {
            await _schoolSubjectService.DeleteAsync(schoolSubjectId);

            return NoContent();
        }

    }
}
