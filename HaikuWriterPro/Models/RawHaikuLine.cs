namespace Models
{
    public class RawHaikuLine
    {
        public int haikuLineId { get; set; }
        public string line { get; set; }
        public string tags { get; set; }
        public int syllable { get; set; }
        public bool approved { get; set; }
        public string username { get; set; }
    }
}