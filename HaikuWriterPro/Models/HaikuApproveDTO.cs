namespace Models
{
    public class HaikuApproveDTO
    {
        public int haikuId { get; set; }
        public string haikuline1 { get; set; }
        public string haikuline2 { get; set; }
        public string haikuline3 { get; set; }
        public string tags { get; set; }
        public bool approved { get; set; }
        public string username { get; set; }
    }
}