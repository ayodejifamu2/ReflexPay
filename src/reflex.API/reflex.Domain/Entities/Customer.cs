
namespace reflex.Domain;

public class Customer : BaseEntity
{
    //profile
    public string? customerPhoneNumber { get; set; }
    public string? emailAddress { get; set; }
    public string userName { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string customerSex { get; set; }

    //profile verification
    public bool isEmailVerified { get; set; }
    public DateTime emailVerifiedDate { get; set; }
    public bool isPhoneNumVerified { get; set; }
    public DateTime phoneNumVerifiedDate { get; set; }

    //sign up details
    public bool isPasswordSet { get; set; } = false;
    public HashCode userPassword { get; set; }
    public DateTime passwordCreatedAt { get; set; }
    public DateTime passwordUpdatedAt { get; set; }
    public DateTime lastLogin { get; set; }
    public loginType lastLoginType { get; set; }
    public bool isLoggedIn { get; set; }
    public int failedLoginAttempt { get; set; }
    public bool isAccountLocked { get; set; }

    //transaction pin
    public bool isTPinSet { get; set; }
    public string transactionPin { get; set; }
    public DateTime transactionPinCreatedAt { get; set; }
    public DateTime transactionPinUpdateAt { get; set; }
    public int wrongTPinCount { get; set; }
    public bool transactionPinBlocked { get; set; }
    public DateTime transactionPinBlockedDate { get; set; }

    //login pin
    public bool isLPinSet { get; set; }
    public string loginPin { get; set; }
    public DateTime loginPinCreatedAt { get; set; }
    public DateTime loginPinUpdateAt { get; set; }
    public int wrongLPinCount { get; set; }
    public bool loginPinBlocked { get; set; }
    public DateTime loginPinBlockedDate { get; set; }

    //other profile details
    public string customerAddress { get; set; }
    public DateTime addressCreatedAt { get; set; }
    public DateTime addressUpdatedAt { get; set; }
    public int addressUpdateCount { get; set; }
    public int customerCountryId { get; set; }
    public int customerStateId { get; set; }



}



public enum loginType{
    pinOtpVerification = 1,
    passwordOtpVerification

}