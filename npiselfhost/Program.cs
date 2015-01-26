using Microsoft.Owin.Hosting;
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Cors;


namespace npiselfhost
{
  class Program
  {
    static void Main(string[] args)
    {

      string baseAddress = "http://*:80/";
      
      // Start OWIN host 
      var app = WebApp.Start<Startup>(url: baseAddress);

      NPIContext.Init();
      
      Console.ReadLine(); 

    }
  }
}
