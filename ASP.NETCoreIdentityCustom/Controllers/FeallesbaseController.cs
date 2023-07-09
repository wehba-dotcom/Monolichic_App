
using Bornholm_Slægts.Models;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;
using Microsoft.IdentityModel.Tokens;

using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

using String = System.String;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ASP.NETCoreIdentityCustom.Areas.Identity.Data;

namespace Bornholm_Slægts.Controllers
{
    public class FeallesbaseController : Controller
    {
        private readonly ApplicationDbContext _db;
        public FeallesbaseController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(string? Firstname,DateTime? DoedDato)
        {
           // ViewData["DateSortParm"] = Firstname == "DateTime" ? "Avisdato" : "DateTime";
            var objList = from b in _db.Feallesbases select b;
           
            if (!String.IsNullOrEmpty(Firstname) && DoedDato != null)
            {
                objList = objList.Where(b => b.Fornavne.Contains(Firstname) && b.Doedsdato == DoedDato);

            }
            else if (String.IsNullOrEmpty(Firstname) && DoedDato != null)
            {
                objList = objList.Where(b => b.Doedsdato == DoedDato);
            }
            else if (!String.IsNullOrEmpty(Firstname) && DoedDato == null)
            {
                objList = objList.Where(b => b.Fornavne.Contains(Firstname));
            }
            
            return View(objList);
        }
       
        //public  IActionResult Index()
        //{
         
        //IQueryable<string> genreQuery = from m in _db.Feallesbases
        //                                orderby m.SognID
        //                                select m.SognID;

        //         var objList = from m in _db.Feallesbases
        //             select m;
        //    Feallesbase feals = new Feallesbase();
        //    if (!string.IsNullOrEmpty(feals.SearchString))
        //    {
        //                objList = objList.Where(s => s.Fornavne.Contains(feals.SearchString));
        //    }

        //    if (!string.IsNullOrEmpty(feals.SognsID))
        //    {
        //                objList = objList.Where(x => x.SognID == feals.SognsID);
        //    }
        //    feals.SognIDs = new SelectList( genreQuery.Distinct().ToList());
        //    feals.Fealles =  objList.ToList();
           
        //    return View(objList);

            
        //}


    public IActionResult Create()
        {
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if(id==0 || id == null)
            {
                return NotFound();

            }
            var obj = _db.Feallesbases.Find(id);
            if(obj==null)
            {
                return NotFound();
            }
            return View(obj);
        }

        public IActionResult Update(int? ID)
        {
            if (ID == null && ID == 0)
            {
                return NotFound();
            }
            var obj = _db.Feallesbases.Find(ID);
            return View(obj);
        }
        [HttpGet]
        public IActionResult Search(DateTime DeadDate, string Firstname)
        {
            var objList = from b in _db.Feallesbases select b;
            if (DeadDate != null && Firstname == "")
            {
                objList = objList.Where(s => s.Doedsdato == (DeadDate));
                return View(objList);
            }

            else if (DeadDate == null && Firstname != "")

            {
                objList = objList.Where(s => s.Fornavne.Contains(Firstname));
                return View(objList);
            }else if(DeadDate !=null && Firstname !="" )
            {
                objList = objList.Where(s => s.Fornavne.Contains(Firstname) & s.Doedsdato==DeadDate);
                return View(objList);
            }
            return View("Index");

        }

        [HttpPost]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Feallesbases.Find(id);
            if ( id==null)
            {
                return NotFound();
            }
           
            _db.Feallesbases.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
       
        [HttpGet]
        public IActionResult SearchGet(int? ID)
        {
            var obj = _db.Feallesbases.Find(ID);
            if (obj == null)
            {
                return NotFound();
            }
            
           if(obj==null)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Create(Feallesbase feallesbase)
        {
            _db.Feallesbases.Add(feallesbase);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult UpdatePost(Feallesbase feallesbase)
        {
           
          
            _db.Entry(feallesbase).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
