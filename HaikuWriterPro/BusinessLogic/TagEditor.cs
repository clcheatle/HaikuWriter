using System;
using Models;

namespace BusinessLogic
{
    public class TagEditor
    {
        public string TagLine { get; set; }
        public Haiku HaikuHolder { get; set; }
        public HaikuLine HaikuLineHolder { get; set; }
        public TagEditor(Haiku haiku, string newTag)
        {
            HaikuHolder = haiku;
            TagLine = AddTag(haiku.Tags, newTag);
        }
        public TagEditor(HaikuLine haikuline, string newTag)
        {
            HaikuLineHolder = haikuline;
            TagLine = AddTag(HaikuLineHolder.Tags, newTag);
        }

        public static string AddTag(string oldTag, string newTag)
        {
            if(oldTag.Contains(newTag)){
                return oldTag;
            }
            else{
                string addedTag = oldTag + ", " + newTag;
                return addedTag;
            }
            
        }

    }
}
