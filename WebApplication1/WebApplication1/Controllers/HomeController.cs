using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Db;
using static System.Math;
using static System.Linq.Enumerable;
using System.Data.SqlClient;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int _age;

        public int MinimumAge
        {
            get { return _age; }
            set
            {
                _age = value;
                OnPropertyChanged();
            }
        }

        public bool PassVotingAge => MinimumAge > 18;

        private int _maxAge;

        public int MaximumAge
        {
            get { return _maxAge; }
            set
            {
                _maxAge = value;
                OnPropertyChanged(nameof(this.MaximumAge));
                OnPropertyChanged(nameof(this.Index));
            }
        }

        public Northwind DbContext { get; } = CallComplexMethod();

        private static Northwind CallComplexMethod()
        {
            return new Northwind();
        }

        private void OnPropertyChanged([CallerMemberName]string v = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(v));
        }

        private void Test()
        {
            List<int> l = new List<int> { 2, 3, 5, 7, 11, 13 };
            Dictionary<string, int> dict = new Dictionary<string, int>()
            {
                ["One"] = 1,
                ["Two"] = 2,
            };
        }

        private double ComplexMathFunction(float value)
        {
            double f = Abs(Log(Cos(value)));

            return f;
        }

        /// <summary>
        /// Main index page of HomeController
        /// </summary>
        /// <returns>Content for now</returns>
        public ActionResult Index()
        {
            //return Content("Hello World");
            Category cat = DbContext.Categories.First();
            DbContext.Configuration.LazyLoadingEnabled = false;

            var st = JsonConvert.SerializeObject(cat);
            return Content(st);
        }

        private void Test2()
        {
            try
            {
                Category cat = DbContext.Categories.FirstOrDefault();
                Display(cat?.CategoryName ?? "Default name");

                if (cat?.Products.FirstOrDefault()?.ProductName?.Length > 5)
                {
                    // Do something
                }
            }
            catch (SqlException ex) when (CanIHandleThisException(ex))
            {

            }
            catch (Exception ex)
            {

            }
        }

        private bool CanIHandleThisException(SqlException ex)
        {
            throw new NotImplementedException();
        }

        private void Display(string v)
        {
            throw new NotImplementedException();
        }
    }
}