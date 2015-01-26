using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Diagnostics;
using System.Linq;
using System;
using System.Web.Http.Cors;
using Jil;
using System.Net.Http.Headers;


namespace npiselfhost
{

  
  public class npiController : ApiController
  {

    NPIContext db = new NPIContext();


    // GET api/values/5 
    [HttpGet]
    [Route("api/npi/{name}")]
    public IHttpActionResult Get(string name)
    {
      var sw = new Stopwatch();
      sw.Start();
      var names = name.ToLowerInvariant().Split('_');
      Console.WriteLine("first>" + names[0] + ", last>" + names[1]);
      var lresult = db.GetByLast(names[1]);
      var el1 = sw.Elapsed.TotalMilliseconds;



      var result = lresult.Where(x => x.fname.StartsWith(names[0], StringComparison.OrdinalIgnoreCase));

      sw.Stop();
      Console.WriteLine("first part : " + el1 + " ;  " + result.Count() + " results served in " + sw.Elapsed.TotalMilliseconds + "ms");
      

      return new JILResult(result, Request);      
        

      //return "value";
    }

    // GET api/values/5 
    [HttpGet]
    [Route("api/npi/exact/{name}")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public HttpResponseMessage GetXML(string name)
    {
      var sw = new Stopwatch();
      sw.Start();
      var names = name.ToLowerInvariant().Split('_');
      Console.WriteLine("first>" + names[0] + ", last>" + names[1]);
      var lresult = db.GetByLastExact(names[1]);
      var el1 = sw.Elapsed.TotalMilliseconds;



      var result = lresult.Where(x => x.fname.Equals(names[0], StringComparison.OrdinalIgnoreCase));


      var json = JSON.Serialize<IEnumerable<NPI>>(result);


      var response = Request.CreateResponse();
      response.Content = new StringContent(json);
      response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
     


      sw.Stop();
      Console.WriteLine("first part : " + el1 + " ;  " + result.Count() + " results served in " + sw.Elapsed.TotalMilliseconds + "ms");


      return response;


      //return "value";
    }

  }
}