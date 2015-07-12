using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogCommentSubmissionSystem.Controllers
{
    public class HomeController : Controller
    {
        ProjectDatabaseEntities1 db = new ProjectDatabaseEntities1(); //initializes a databbase object
        BlogCommentSubmissionSystem.Session session = BlogCommentSubmissionSystem.Session.Instance; 
        //returns the object that was already initialized in the Session class

        // GET: Home/Starting page
        public ActionResult Index()
        {
            return View();
        }

        //GET: Submission and submits valid data
        public ActionResult Submission()
        {
            var postObj = new PostTable();
            ViewBag.PostObj = postObj;
            if (session.Message != null)
            {
                ViewBag.ErrorMessage = session.Message;
                ViewBag.ErrorCode = session.MessageCode;
            }

            return View();
        }

        //Get: ViewPosts and allows to view all the posts in a table
        public ActionResult ViewPost()
        {
            var result = (from post in db.PostTables
                          select post).ToList();
            ViewBag.Posts = result;
            return View();
        }

        //Checks for the authentication
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Authentication(String email, String title, String desc)
        {
            var check = (from user in db.UserTables
                         where user.EmailAddress == email
                         select user).SingleOrDefault();

            if (check != null)
            {
                PostTable post = new PostTable();
                post.Description = desc;
                post.PostedDate = DateTime.Now;
                post.Title = title;
                post.UserId = check.UserId;
                db.PostTables.Add(post);
                db.SaveChanges();
                session.Message = "Record added. Thank you for your post!";
                session.MessageCode = 1;
                
            }
            else
            {
                session.Message = "Sorry, invalid user e-mail!";
                session.MessageCode = 0;
            }

            return RedirectToAction("Submission", "Home");
        }

        //Get: Details Controller and shows information 
        public ActionResult Details(int Id)
        {
            var result = (from post in db.PostTables
                          where post.PostId == Id
                          select post).SingleOrDefault();
            ViewBag.PostDetails = result;
            return View();
        }
    }
}