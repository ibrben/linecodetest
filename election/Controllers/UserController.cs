using System;
using election.Models;
using election.Models.Response;
using election.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace election.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class UserController : ControllerBase
    {
        private readonly IJWTManagerRepository _jWTManager;
		private IUsersRepository _userRepository;

        public UserController(IJWTManagerRepository jWTManager, IUsersRepository userRepository )
        {
            _jWTManager = jWTManager;
			_userRepository = userRepository;
        }

		[AllowAnonymous]
		[HttpPost]
		[Route("authenticate")]
		public IActionResult Authenticate(Users usersdata)
		{
			var token = _jWTManager.Authenticate(usersdata);

			if (token == null)
			{
				return Unauthorized();
			}

			return Ok(token);
		}

		[AllowAnonymous]
		[HttpPost]
		[Route("register")]
		public IActionResult Reginster(Users userdata)
        {
			if (_userRepository.Contain(userdata.user))
				return new JsonResult(new CommonResponse("error", "Username already exists."));
			_userRepository.AddNewUser(userdata);
			return new JsonResult(new CommonResponse("ok"));
        }
	}
}
