using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogCommentSubmissionSystem.Controllers
{
    public class HomeController : Controller
    {
        ProjectDatabaseEntities db = new ProjectDatabaseEntities(); //initializes a databbase object
        BlogCommentSubmissionSystem.Session session = BlogCommentSubmissionSystem.Session.Instance; 
        //returns the object that was already initialized in the Session class

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        //GET: Submission
        public ActionResult Submission()
        {
            var postObj = new PostTable();
            ViewBag.PostObj = postObj;
            if (session.Message != null)
            {
                ViewBag.ErrorMessage = session.Message;
            }

            return View();
        }

        //Get: ViewPosts
        public ActionResult ViewPost()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Authentication(String email, String title, String desc)
        {
            var check = (from user in db.UserTables
                         where user.EmailAddress == email
                         select user).ToList();

            if (check.Count > 0)
            {

                session.Message = "Record Added.";
            }
            else
            {
                session.Message = "Email not found.";
            }

            return RedirectToAction("Submission", "Home");
        }
    }
}