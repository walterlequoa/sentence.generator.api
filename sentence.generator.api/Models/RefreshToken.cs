namespace sentence.generator.api.RequestModel
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string TokenHash { get; set; }
        public string TokenSalt { get; set; }
        public DateTime Ts { get; set; }
        public DateTime ExpiryDate { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
