namespace EmailOtp;

public class OtpGenerator : IRandomGenerator
{
    public int Generate()
    {
        Random random = new();
        return random.Next(100000, 999999);
    }
}