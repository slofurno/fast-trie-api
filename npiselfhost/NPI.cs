using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProtoBuf;
using System.IO;


namespace npiselfhost
{


  [ProtoContract]
  public class NPI
  {
    [ProtoMember(1)]
    public string npi { get; set; }
    [ProtoMember(2)]
    public string lname { get; set; }
    [ProtoMember(3)]
    public string fname { get; set; }
    [ProtoMember(4)]
    public string mname { get; set; }
    [ProtoMember(5)]
    public string prefix { get; set; }
    [ProtoMember(6)]
    public string suffix { get; set; }
    [ProtoMember(7)]
    public string Credentials { get; set; }
    [ProtoMember(8)]
    public string Address { get; set; }
    [ProtoMember(9)]
    public string City { get; set; }
    [ProtoMember(10)]
    public string State { get; set; }



    public NPI()
    {

    }

    public NPI(string npi, string lname, string fname, string mname, string prefix, string suffix, string cred, string add, string city, string state)
    {

      this.npi = npi;
      this.lname = lname;
      this.fname = fname;
      this.mname = mname;
      this.prefix = prefix;
      this.suffix = suffix;
      this.Credentials = cred;
      this.Address = add;
      this.City = city;
      this.State = state;

    }

  }

  public class NPIContext
  {

    private static Tr<NPI> _npitree;
    private static bool started = false;
    private static bool ready = false;
    

    public NPIContext()
    {

      

    }


    public static void Init()
    {

      if (_npitree == null && started == false)
      {
        started = true;

        _npitree = new Tr<NPI>(6);

        ProtoBuf.Serializer.PrepareSerializer<NPI>();




        using (var file = File.OpenRead("npi.bin"))
        {

          var actorlist = Serializer.DeserializeItems<NPI>(file, PrefixStyle.Base128, 1).ToList();
          var count = actorlist.Count();
          Console.WriteLine("loading " + count + " records");

          foreach (var actor in actorlist)
          {
            //actor.lname = actor.lname.ToLower();
            //actor.fname = actor.fname.ToLower();

            _npitree.Insert(actor.lname.ToLowerInvariant(), actor);

          }

        }

        ready = true;
        Console.WriteLine("search trie built");
        
      }

    }

    public IEnumerable<NPI> Get(string name)
    {
      if (!ready) {
        return Enumerable.Empty<NPI>();
      }
      var s = name.Split('_');
      if (s.Length < 1)
      {
        s = new string[] { " ", s[0] };
      }
      var results = _npitree.Search(s[1]);

      if (results != null)
      {
        return results.Where(x => x.fname.IndexOf(s[0]) == 0);
      }

      return Enumerable.Empty<NPI>();

    }

    public IEnumerable<NPI> GetByLast(string lname)
    {

      if (!ready)
      {
        return Enumerable.Empty<NPI>();
      }
   
      var results = _npitree.Search(lname);

      return results;

    }

    public IEnumerable<NPI> GetByLastExact(string lname)
    {

      if (!ready)
      {
        return Enumerable.Empty<NPI>();
      }

      var results = _npitree.SearchExact(lname);

      return results;

      

    }


  }



}