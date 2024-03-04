namespace DDD.Rehydrate.Domain
{
    public class License : DomainEntity
    {
        public string UserId { get; }

        public LicneseType Type { get; }

        public LicenseStatus Status { get; }

        public DateTime IssuedDate { get; }

        public DateTime Expiration { get; }
        public License(string id, string userId, LicneseType type, LicenseStatus status, DateTime issuedDate, DateTime expiration) : base(id)
        {
            Type = type;
            Status = status;
            IssuedDate = issuedDate;
            Expiration = expiration;
            UserId = userId;
        }
    }

    public enum LicneseType { Desktop, Mobile, Web }

    /// <summary>
    /// Would probably be a custom class  not an enum
    /// </summary>
    public enum LicenseStatus { Issued, Suspended }
}
