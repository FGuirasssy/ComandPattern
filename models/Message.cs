using System;
namespace ComandPattern.models
{
    public class Message
    {
        private String content;
        private int id;
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

        public void SetId(int value) {
            id = value;
        }

        public int GetId() {
            return id;
        }

    }
}
