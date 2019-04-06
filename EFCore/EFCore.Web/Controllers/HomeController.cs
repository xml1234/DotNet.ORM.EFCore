using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EFCore.Data;
using EFCore.DomainModels;
using Microsoft.AspNetCore.Mvc;
using EFCore.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Web.Controllers
{
    public class HomeController : Controller
    {
        public readonly MyContext _Context;
        public readonly MyContext _Context2;

        public HomeController(MyContext context, MyContext context2)
        {
            _Context = context;
            _Context2 = context2;
        }

        public IActionResult Index()
        {

            #region 关联插入

            ////Include关联表预加载
            //var list = _Context.Provinces
            //    .Include(x => x.Cities)
            //    .ThenInclude(x => x.CityCompanies)//Include
            //    .ThenInclude(x => x.Company)
            //    .ToList();



            //var province = new Province
            //{
            //    Name = "liaoning",
            //    Population = 300_000,
            //    Cities = new List<City>
            //    {
            //        new City {AreaCode = "123", Name = "shenyang"},
            //        new City {AreaCode = "234", Name = "dalian"}
            //    }
            //};
            //_Context.Provinces.Add(province);
            //_Context.SaveChanges();

            #endregion




            ////执行存储过程
            //_Context.Database.ExecuteSqlCommand("exec ....");


            #region 修改

            ////查找Provinces下面Cities里面有数据数量大于0的数据并且取出第一条数据
            //var entity = _Context.Provinces.Include(x => x.Cities).First(x => x.Cities.Any());

            //var city = entity.Cities[0];
            //city.Name += " update";

            ////只会更新一条数据，关联属性不会被修改
            //_Context2.Entry(city).State = EntityState.Modified;
            ////这样写会更新3条数据
            ////_Context2.Cities.Update(city);

            //_Context2.SaveChanges();




            ////离线修改
            //var entity = _Context.Provinces.FirstOrDefault();
            //entity.Population += 100;
            //_Context2.Provinces.Update(entity);
            //_Context2.SaveChanges();

            #endregion

            #region 查询





            ////它的作用是当连接的表为空时也会有一条空的数据，达到了left join的效果
            //var list = from c1 in _Context.Cities
            //           join c2 in _Context.CityCompanies on c1.Id equals c2.CityId
            //           into dc
            //           from c3 in dc.DefaultIfEmpty()
            //           select new
            //           {
            //               c1.Id,
            //               c3.CityId
            //           };
            //list.ToList();


            ////inner join 
            //var list = _Context.Cities.Join(_Context.CityCompanies, c1 => c1.Id, c2 => c2.CityId,
            //    (c1, c2) => new { c1.Id, c2.CityId }).ToList();


            //var list = _Context.Provinces.Where(x => EF.Functions.Like(x.Name, "%北%")).ToList();


            //linq
            //var list = (from p in _Context.Provinces
            //    where p.Name == "北京"
            //    select p).ToList();

            #endregion

            #region 批量插入

            //var province = new Province
            //{
            //    Name = "天津",
            //    Population = 800_000
            //};

            //var company = new Company
            //{
            //    Name = "Taida",
            //    DateTime = new DateTime(2019, 1, 1),
            //    Person = "Secret Man"
            //};

            //_Context.AddRange(province, company);





            //var province1 = new Province
            //{
            //    Name = "上海",
            //    Population = 3_000_000
            //};

            //var province2 = new Province
            //{
            //    Name = "广东",
            //    Population = 4_000_000
            //};

            ////_Context.Provinces.AddRange(province, province1, province2);
            //_Context.Provinces.AddRange(new List<Province>
            //{
            //    province, province1, province2
            //});

            //_Context.SaveChanges();


            #endregion



            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
