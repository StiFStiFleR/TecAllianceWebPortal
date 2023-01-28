using Microsoft.AspNetCore.Mvc;
using TecAllianceWebPortal.Attributes;
using TecAllianceWebPortal.Communication.Requests;
using TecAllianceWebPortal.Services.Interfaces;

namespace TecAllianceWebPortal.Controllers
{
    [FakeAuth]
    [ApiController]
    [Route("[controller]")]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NoteController(INoteService noteService)
        {
            _noteService = noteService ?? throw new ArgumentNullException(nameof(noteService));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllForUser()
        {
            try
            {
                Request.Cookies.TryGetValue("email", out string? email);
                var notes = await _noteService.GetAllNotesForUser(email!);
                return new JsonResult(notes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NoteRequest request)
        {
            try
            {
                Request.Cookies.TryGetValue("email", out string? email);
                var note = await _noteService.CreateNote(request, email!);
                return new JsonResult(note);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] NoteRequest request)
        {
            try
            {
                Request.Cookies.TryGetValue("email", out string? email);
                var note = await _noteService.UpdateNote(request, email!);
                return new JsonResult(note);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Request.Cookies.TryGetValue("email", out string? email);
                await _noteService.DeleteNote(id, email!);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}