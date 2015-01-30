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

  public class npidto
  {



    public string n { get; set; }
    public string l { get; set; }
    public string f { get; set; }
    public string m { get; set; }
    public string p { get; set; }
    public string su { get; set; }
    public string cr { get; set; }
    public string a { get; set; }
    public string c { get; set; }
    public string s { get; set; }


    public npidto(NPI src)
    {

      this.n = src.npi;
      this.l = src.lname;
      this.f = src.fname;
      this.m = src.mname;
      this.p = src.prefix;
      this.su = src.suffix;
      this.cr = src.Credentials;
      this.a = src.Address;
      this.c = src.City;
      this.s = src.State;


    }

    public npidto()
    {

    }


  }


  



}