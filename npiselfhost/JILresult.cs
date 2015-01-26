﻿using System;
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
  public class JILResult : IHttpActionResult
  {
    dynamic _value;
    HttpRequestMessage _request;

    public JILResult(IEnumerable<NPI> value, HttpRequestMessage request)
    {
      _value = value;
      _request = request;
    }
    public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
    {

      var json = JSON.Serialize<IEnumerable<NPI>>(_value);

      
      var response = new HttpResponseMessage()
      {
        Content = new StringContent(json),
        RequestMessage = _request
      };
      response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
      return Task.FromResult(response);
    }
  }

  public class JilFormatter : MediaTypeFormatter
  {
    private readonly Options _jilOptions;
    public JilFormatter()
    {
      _jilOptions = new Options(dateFormat: DateTimeFormat.ISO8601);
      SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));

      SupportedEncodings.Add(new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true));
      SupportedEncodings.Add(new UnicodeEncoding(bigEndian: false, byteOrderMark: true, throwOnInvalidBytes: true));
    }
    public override bool CanReadType(Type type)
    {
      if (type == null)
      {
        throw new ArgumentNullException("type");
      }
      return true;
    }

    public override bool CanWriteType(Type type)
    {
      if (type == null)
      {
        throw new ArgumentNullException("type");
      }
      return true;
    }

    public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, System.Net.Http.HttpContent content, IFormatterLogger formatterLogger)
    {
      return Task.FromResult(this.DeserializeFromStream(type, readStream));
    }


    private object DeserializeFromStream(Type type, Stream readStream)
    {
      try
      {
        using (var reader = new StreamReader(readStream))
        {
          MethodInfo method = typeof(JSON).GetMethod("Deserialize", new Type[] { typeof(TextReader), typeof(Options) });
          MethodInfo generic = method.MakeGenericMethod(type);
          return generic.Invoke(this, new object[] { reader, _jilOptions });
        }
      }
      catch
      {
        return null;
      }

    }


    public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, System.Net.Http.HttpContent content, TransportContext transportContext)
    {
      using (TextWriter streamWriter = new StreamWriter(writeStream))
      {
        JSON.Serialize(value, streamWriter, _jilOptions);
        return Task.FromResult(writeStream);
      }
    }
  }

}