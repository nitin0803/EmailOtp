// See https://aka.ms/new-console-template for more information

using EmailOtp;

var otpModule = new Email_OTP_Module();

Console.WriteLine("Welcome to DSO National Laboratories!");
var userEmail = GetEmail();
var emailOtpModule = new Email_OTP_Module();
var generateOtpEmailResult = emailOtpModule.Generate_OTP_Email(userEmail);
Console.WriteLine(generateOtpEmailResult);
while (generateOtpEmailResult == EmailStatus.STATUS_EMAIL_INVALID)
{
    generateOtpEmailResult = GetEmail();
}

if (generateOtpEmailResult == EmailStatus.STATUS_EMAIL_OK)
{
    Console.WriteLine("Lets now validate Otp!!");
    Console.WriteLine($"Please Enter OTP sent on your email address : {userEmail}");
    var enteredOtp = Convert.ToInt32(Console.ReadLine());
    var checkOtpResult = emailOtpModule.Check_OTP(enteredOtp);
    Console.WriteLine(checkOtpResult);
}


string GetEmail()
{
    var enteredEmail = string.Empty;
    while (string.IsNullOrEmpty(enteredEmail))
    {
        Console.WriteLine("Please Enter your email address:");
        enteredEmail = Console.ReadLine();
        if (string.IsNullOrEmpty(enteredEmail))
        {
            Console.WriteLine("You have not entered any email Address!. Try again");
            continue;
        }

        break;
    }

    return enteredEmail;
}