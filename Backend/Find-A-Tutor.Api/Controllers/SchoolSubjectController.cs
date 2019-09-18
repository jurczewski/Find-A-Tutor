using Find_A_Tutor.Infrastructure.Commands.SchoolSubject;
using Find_A_Tutor.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Find_A_Tutor.Core.Domain;

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

        /// <summary>
        /// Get a SchoolSubject that contains given name.
        /// </summary>
        /// <param name="name">Name of a subject.</param>     
        /// <returns>Return a list of SchoolSubjects</returns>
        /// <response code="200">OK - request has succeeded.</response>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<SchoolSubject>), 200)]
        public async Task<IActionResult> Get(string name = "")
        {
            Result<IEnumerable<SchoolSubject>> schoolSubjectResult = await _schoolSubjectService.BrowseAsync(name);

            return !schoolSubjectResult.IsSuccess ? NotFound() : (IActionResult)Json(schoolSubjectResult);
        }

        [HttpGet("{schoolSubjectId}")]
        public async Task<IActionResult> Get(Guid schoolSubjectId)
        {
            var schoolSubjectResult = await _schoolSubjectService.GetAsync(schoolSubjectId);
            return !schoolSubjectResult.IsSuccess ? NotFound() : (IActionResult)Json(schoolSubjectId);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateSchoolSubject command)
        {
            command.SchoolSubjectId = Guid.NewGuid();
            var schoolSubjectResult = await _schoolSubjectService.CreateAsync(command.SchoolSubjectId, command.Name);

            return schoolSubjectResult.IsSuccess ? Created($"/PrivateLesson/{command.Name}", null) : (IActionResult)Json(schoolSubjectResult);
        }

        [HttpPut("{schoolSubjectId}")]
        public async Task<IActionResult> Put(Guid schoolSubjectId, [FromBody]UpdateSchoolSubject command)
        {
            var schoolSubjectResult = await _schoolSubjectService.UpdateAsync(schoolSubjectId, command.Name);

            return schoolSubjectResult.IsSuccess ? NoContent() : (IActionResult)Json(schoolSubjectResult);
        }

        [HttpDelete("{schoolSubjectId}")]
        public async Task<IActionResult> Delete(Guid schoolSubjectId)
        {
            var schoolSubjectResult = await _schoolSubjectService.DeleteAsync(schoolSubjectId);

            return schoolSubjectResult.IsSuccess ? NoContent() : (IActionResult)Json(schoolSubjectResult);
        }

    }
}
