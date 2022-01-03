using do_an_ao_hoa.Models;
using MongoDB.Driver;
using NhomLucky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NhomLucky.Controllers
{
    public class BookController : Controller
    {
        public BookController()
        {
            MongoHelper.ConnectToMongoService();
            MongoHelper.book_collection = MongoHelper.database.GetCollection<Book>("books");
        }
        // GET: Book
        public ActionResult Index()
        {
            var filter = Builders<Book>.Filter.Ne("_id", "");
            var result = MongoHelper.book_collection.Find(filter).ToList();
            return View(result);
        }
        public ActionResult L()
        {
            var filter = Builders<Book>.Filter.Ne("_id", "");
            var result = MongoHelper.book_collection.Find(filter).ToList();
            return View(result);
        }
        // GET: Book/Details/5
        public ActionResult Details(string id)
        {
            var filter = Builders<Book>.Filter.Eq("_id", id);
            var result = MongoHelper.book_collection.Find(filter).FirstOrDefault();
            return View(result);
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            return View();
        }
        public static Random r = new Random();
        private Object random(int v)
        {
            string code = "abcefghijklmnopqrstuvwxyz123456789";
            return new string(Enumerable.Repeat(code, v).Select(s => s[r.Next(s.Length)]).ToArray());
        }

        // POST: Book/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            Object _id = random(24);
            var file = Request.Files["file"];
            string image = string.Empty;
            if (file != null)
            {
                try
                {
                    string path = Server.MapPath("~/Assets/img/" + file.FileName);
                    file.SaveAs(path);
                    image = file.FileName;
                }
                catch (Exception)
                {
                }
            }
            MongoHelper.book_collection.InsertOneAsync(new Book()
            {
                _id = _id,
                code = collection["code"],
                name = collection["name"],
                image = image,
                dongia = collection["dongia"]
            });
            return RedirectToAction("Index");
        }
        // GET: Book/Edit/5
        public ActionResult Edit(string id)
        {
            var filter = Builders<Book>.Filter.Eq("_id", id);
            var result = MongoHelper.book_collection.Find(filter).FirstOrDefault();
            return View(result);
        }

        // POST: Book/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            try
            {
                string image = collection["image"];

                var file = Request.Files["file"];
                if (file != null)
                {
                    try
                    {
                        string path = Server.MapPath("~/Assets/img/" + file.FileName);
                        file.SaveAs(path);
                        image = file.FileName;
                    }
                    catch (Exception)
                    {
                    }
                }
                var filter = Builders<Book>.Filter.Eq("_id", id);
                var update = Builders<Book>.Update
                    .Set("code", collection["code"])
                    .Set("name", collection["name"])
                    .Set("image", image)
                    .Set("dongia", collection["dongia"]);
                MongoHelper.book_collection.UpdateOneAsync(filter, update);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //GET: Book/Delete/5
        //public ActionResult Delete(string id)
        //{
        //    var filter = Builders<Book>.Filter.Eq("_id", id);
        //    var result = MongoHelper.book_collection.Find(filter).FirstOrDefault();
        //    return View(result);
        //}

        // GET: Book/Delete/5
        [HttpPost]
        public JsonResult Delete(string id)
        {
            try
            {
                var filter = Builders<Book>.Filter.Eq("_id", id);
                var result = MongoHelper.book_collection.Find(filter).FirstOrDefault();
                MongoHelper.book_collection.DeleteOneAsync(filter);
            }
            catch (Exception)
            {
                return Json(new
                {
                    status = 0
                });

            }

            return Json(new
            {
                status = 1
            });
        }
    }
}
