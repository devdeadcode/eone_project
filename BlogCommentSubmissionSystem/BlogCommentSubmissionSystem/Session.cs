using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogCommentSubmissionSystem
{
    //Using singleton pattern to initialize an object only once
    public sealed class Session //seals by not allowing to extend this class
        {
        static readonly Session _instance = new Session(); //readonly indicates that it can allocate only once
        private string message;
        public static Session Instance
        {
            get
            {
                return _instance;
            }
        }

        public string Message
        {
            get { return message; }
            set { message = value; }
            }

        }
   
}