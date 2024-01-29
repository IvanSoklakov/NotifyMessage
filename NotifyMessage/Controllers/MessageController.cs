using Microsoft.AspNetCore.Mvc;
using NLog;
using NotifyMessage.BuisnessLogicLayer.Interfaces;
using NotifyMessage.DataAccesLayer.Models;

namespace NotifyMessage.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MessageController : Controller
    {
        private readonly NLog.ILogger _logger;
        private readonly IMessageServices _service;

        public MessageController(IMessageServices service)
        {
            _logger = LogManager.GetCurrentClassLogger(typeof(MessageController));
            _service = service; 
        }

        [HttpPost("SendMessage")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult SendMessage([FromBody] Message message)
        {
            try
            {     
                if(message == null || message.Recipients == null || !message.Recipients.Any())
                {
                    return BadRequest("Некорректно введены данные!");
                }
                _service.SendMessage(message);

                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("GetMessage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetMessage([FromQuery] int rcpt)
        {
            try
            {
                var resultMessage = _service.GetMessage(rcpt);

                if (resultMessage == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                return Ok(resultMessage);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
