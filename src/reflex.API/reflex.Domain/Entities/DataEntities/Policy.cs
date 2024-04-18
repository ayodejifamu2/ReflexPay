namespace reflex.Domain;

public class Policy : BaseEntity
{
    public Uri? policyUrl { get; set; }
    public int policyType { get; set; }
    public DateTime createdAt { get; set; }

}
