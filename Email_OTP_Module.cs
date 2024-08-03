using System.Diagnostics;
using System.Text.RegularExpressions;

namespace EmailOtp;

public class Email_OTP_Module
{
    private const string EmailRegexPattern = "[a-zA-Z0-9_.+-]+@dso.org.sg$";
    private static readonly Random OtpGenerator = new();

    private string currentUserEmail;
    private int? currentUserOtp;
    private Stopwatch stopwatch = new();


    public void Start()
    {
        throw new NotImplementedException();
    }

    public void Stop()
    {
        throw new NotImplementedException();
    }

    public string Generate_OTP_Email(string userEmail)
    {
        currentUserEmail = userEmail;
        if (!Regex.IsMatch(userEmail, EmailRegexPattern))
        {
            return EmailStatus.STATUS_EMAIL_INVALID;
        }

        currentUserOtp = OtpGenerator.Next(100000, 999999);
        var body = $"You OTP Code is {currentUserOtp}. The code is valid for 1 minute";
        var result = Send_Email(currentUserEmail, body);
        if (result == EmailStatus.STATUS_EMAIL_FAIL)
        {
            currentUserOtp = null;
        }

        return result;
    }

    public string Check_OTP(int inputOtp)
    {
        var attempt = 1;
        var checkOtpStatus = OtpStatus.STATUS_OTP_FAIL;

        while (attempt < 10)
        {
            var elapsedTime = stopwatch.Elapsed.TotalMinutes;
            if (elapsedTime > 1)
            {
                checkOtpStatus = OtpStatus.STATUS_OTP_TIMEOUT;
                stopwatch.Stop();
                break;
            }

            if (currentUserOtp == inputOtp)
            {
                checkOtpStatus = OtpStatus.STATUS_OTP_OK;
                stopwatch.Stop();
                break;
            }

            Console.WriteLine("Entered OTP does not match!");

            Console.WriteLine("Enter new OTP:");
            inputOtp = Convert.ToInt32(Console.ReadLine());
            attempt++;
        }

        return checkOtpStatus;
    }

    private string Send_Email(string email_address, string email_body)
    {
        var emailRequest = new
        {
            Email = email_address,
            Body = email_body
        };
        Console.WriteLine(email_body);
        stopwatch.Start();
        return EmailStatus.STATUS_EMAIL_OK;
    }
}