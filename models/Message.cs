using System;
namespace ComandPattern.models
{
    public class Message
    {
        private String content;
        public Message(){}

        public Message(String aContent) {
            content = aContent;
        }

        public String GetContent()
        {
            return content;
        }

        public void SetContent(String value)
        {
            content = value;
        }

    }
}
