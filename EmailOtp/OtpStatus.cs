namespace EmailOtp;

public class OtpStatus
{
    public const string STATUS_OTP_OK = "OTP is valid and checked";
    public const string STATUS_OTP_FAIL = "OTP is wrong after 10 tries";
    public const string STATUS_OTP_TIMEOUT = "timeout after 1 min";
}