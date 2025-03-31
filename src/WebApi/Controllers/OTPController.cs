using Microsoft.AspNetCore.Mvc;

[Route("api/auth")]
[ApiController]
public class OTPController : ControllerBase
{
    private const string StaticOTP = "123456"; // 🔥 Static OTP

    [HttpPost("verify-otp")]
    public IActionResult VerifyOTP([FromBody] OTPRequest request)
    {
        if (request.Otp == StaticOTP)
        {
            return Ok(new { message = "OTP Verified Successfully", verified = true });
        }

        return BadRequest(new { message = "Invalid OTP", verified = false });
    }
}

public class OTPRequest
{
    public string Otp { get; set; }
}
