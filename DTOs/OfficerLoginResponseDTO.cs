namespace EPoliceConnectAPI.DTOs
{
    public class OfficerLoginResponseDTO
    {
        public string Token { get; set; }
        public string Name { get; set; }
        public int OfficerId { get; set; }
        public string Rank { get; set; }
        public bool IsDesignated { get; set; }
    }
}
