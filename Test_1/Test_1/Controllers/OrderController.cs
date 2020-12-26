using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Auth;
using Models.ResponseRequest;
using Models.SingleOrder;
using Newtonsoft.Json.Linq;
using Services;

namespace Test_1.Controllers
{
    
    [Controller]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        public OrderController(IOrderService orderService, IUserService userService)
        {
            _orderService = orderService;
            _userService = userService;
        }
        //Execution of query options:
        //?start=4&quantity=10&status=Status1
        //?status=Status1
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("orders")]
        public IActionResult GetOrders([FromQuery]int? start, [FromQuery]int? quantity, [FromQuery]string status)
        {
            return Ok(_orderService.GetAll(start, quantity, status));
        }
        //Execution of query options: /orers/{id}
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("orders/{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_orderService.GetById(id));
        }
        //Example Json:
        //{
        //    "pickup" : {
        //    "latitude" : 0.8008281904610115,
        //    "longitude" : 6.027456183070403
        //},
        //"id" : "4",
        //    "dimension" : "dimension",
        //    "status" : "Status1"
        //}
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("orders")]
        public IActionResult Post([FromBody]JObject data)
        {
            return Ok(_orderService.AddNew(data));
        }
        [AllowAnonymous]
        [HttpPost("auth")]
        public IActionResult Authenticate([FromBody]User userData)
        {
            var user = _userService.Auth(userData.Username, userData.Password);

            if (user == null)
                return BadRequest(new Response{ code=401, message = "Access denided", details= "Username or password is incorrect" });

            return Ok(user);
        }
    }
}