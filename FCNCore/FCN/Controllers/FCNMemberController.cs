using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FCN.Models;
using FCN.Services;

namespace FCN.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FCNMemberController : ControllerBase
    {

        private readonly IFCNMemberService _fCNMemberService;

        public FCNMemberController(IFCNMemberService fcnMemberService)
        {
            _fCNMemberService = fcnMemberService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]FCNMemberData userParam)
        {
            var user = _fCNMemberService.Authenticate(userParam.Email, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        // GET: api/FCNMember
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IEnumerable<FCNMemberData> GetFCNMember()
        {
            IEnumerable<FCNMemberData> members = _fCNMemberService.GetFCNMember();            
            return members;
        }

        // GET: api/FCNMember/5
        [HttpGet("{id}")]
        public ActionResult<FCNMemberData> GetFCNMember(int id)
        {
            FCNMemberData data = _fCNMemberService.GetFCNMemberByID(id);
            if (data == null)
            {
                return NotFound();
            }

            var currentUserId = int.Parse(User.Identity.Name);
            if (id != currentUserId && !User.IsInRole("Admin"))
            {
                return Forbid();
            }
            return Ok(data);
        }

        // POST: api/FCNMember
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult<FCNMemberData> PostFCNMember([FromBody] FCNMemberData data)
        {
            FCNMemberData newMember = _fCNMemberService.Create(data, data.Password);
            if (newMember != null)
            {
                return CreatedAtAction("GetFCNMember", new { newMember.ID }, newMember);
            }
            else
                return BadRequest();
        }

        // PUT: api/FCNMember/5
        [HttpPut("{id}")]
        public ActionResult<FCNMemberData> PutFCNMember(int id, [FromBody] FCNMemberData data)
        {
            var currentUserId = int.Parse(User.Identity.Name);
            if (id != currentUserId && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            FCNMemberData updatedMember;
            bool cmdStatus = _fCNMemberService.Update(data);

            if (cmdStatus)
            {
                updatedMember = _fCNMemberService.GetFCNMemberByID(id); ;
                return Ok(updatedMember);
            }
            else
                return BadRequest();
        }

        // DELETE: api/FCNMember/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult DeleteFCNMember(int id)
        {
            bool cmdStatus = _fCNMemberService.Delete(id);
            if (cmdStatus)
                return NoContent();
            else
                return BadRequest();
        }
    }
}
