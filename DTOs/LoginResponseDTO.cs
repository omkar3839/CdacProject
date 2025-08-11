namespace EPoliceConnectAPI.DTOs
{
    public class LoginResponseDTO
    {
        public string Token { get; set; }
        public string FullName { get; set; }
        public int CivilianId { get; set; }
    }
}
