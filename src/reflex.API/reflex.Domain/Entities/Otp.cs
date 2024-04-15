namespace reflex.Domain;

public class Otp : BaseEntity
{
    public string otp { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public OtpType otpType { get; set; }
}


public enum OtpType{
    emailVerification = 1,
    phoneNumberVerification,
    passwordReset,
    multifactorAuthentication,
    transactionOtp
}
