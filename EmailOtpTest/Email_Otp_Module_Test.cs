using EmailOtp;
using Moq;
using Shouldly;
namespace EmailOtpTest;

[TestClass]
public class Email_Otp_Module_Test
{
    private readonly Mock<IConsole> consoleMock = new();
    private readonly Mock<IRandomGenerator> otpGeneratorMock = new();
    private readonly Email_OTP_Module sut;

    public Email_Otp_Module_Test()
    {
        otpGeneratorMock.Setup(m => m.Generate())
            .Returns(123456);
        sut = new Email_OTP_Module(consoleMock.Object, otpGeneratorMock.Object);
    }
    
    [TestMethod]
    [DataRow("abc@gmail.com")]
    [DataRow("abc@hotmail.com")]
    [DataRow("abc@hotmail.com")]
    [DataRow("abc@dso.com")]
    [DataRow("abc@dso.sg")]
    public void Generate_OTP_Email_GivenInvalidEmail_Return_STATUS_EMAIL_INVALID_Status(string inputEmail)
    {
        // arrange & act
        var result = sut.Generate_OTP_Email(inputEmail);
        
        //assert
        result.ShouldBe(EmailStatus.STATUS_EMAIL_INVALID);
        sut.CurrentUserOtp.ShouldBeNull();
    }
    
    [TestMethod]
    [DataRow("abc@dso.org.sg")]
    [DataRow("abc_123@dso.org.sg")]
    [DataRow("34534_123@dso.org.sg")]
    public void Generate_OTP_Email_GivenValidEmail_Return_STATUS_EMAIL_OK_Status(string inputEmail)
    {
        // arrange & act
        var result = sut.Generate_OTP_Email(inputEmail);
        
        //assert
        result.ShouldBe(EmailStatus.STATUS_EMAIL_OK);
        sut.CurrentUserOtp.ShouldBe(123456);
    }
    
    [TestMethod]
    public void Check_OTP_GivenEnteredOtpNotMatch_Return_STATUS_OTP_FAIL_Status()
    {
        // arrange
        consoleMock.Setup(m => m.ReadLine())
            .Returns("456");
        
        // act
        var result = sut.Check_OTP(123);
        
        //assert
        result.ShouldBe(OtpStatus.STATUS_OTP_FAIL);
    }
    
    [TestMethod]
    public void Check_OTP_GivenEnteredOtpMatch_Return_STATUS_OTP_OK_Status()
    {
        // arrange
        consoleMock.Setup(m => m.ReadLine())
            .Returns("123456");
        sut.Generate_OTP_Email("abc@dso.org.sg");
        
        // act
        var result = sut.Check_OTP(123);
        
        //assert
        result.ShouldBe(OtpStatus.STATUS_OTP_OK);
    }
}