using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Web.Http;
using Jil;
using System.Net.Http.Headers;
using System.IO;
using System.Reflection;
using System.Net.Http.Formatting;
using System.Net;
using System.Text;

namespace npiselfhost
{
  public class JILResult<G> : IHttpActionResult
  {
    dynamic _value;
    HttpRequestMessage _request;

    public JILResult(IEnumerable<G> value, HttpRequestMessage request)
    {
      _value = value;
      _request = request;
    }
    public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
    {

      var json = JSON.Serialize<IEnumerable<G>>(_value);

      
      var response = new HttpResponseMessage()
      {
        Content = new StringContent(json),
        RequestMessage = _request
      };
      response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
      return Task.FromResult(response);
    }
  }

  

}