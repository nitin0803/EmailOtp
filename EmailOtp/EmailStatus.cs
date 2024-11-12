namespace EmailOtp;

public class EmailStatus
{
    public const string STATUS_EMAIL_OK = "email containing OTP has been sent successfully.";
    public const string STATUS_EMAIL_FAIL = "email address does not exist or sending to the email has failed.";
    public const string STATUS_EMAIL_INVALID = "email address is invalid.";
}