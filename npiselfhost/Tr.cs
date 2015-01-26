using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace npiselfhost
{
  class Node<T>
  {
    public Dictionary<char, Node<T>> children;
    public List<T> values;

    public Node()
    {
      this.values = new List<T>();
      this.children = new Dictionary<char, Node<T>>();
    }

  }

  class Tr<T>
  {

    public Node<T> root;
    public int maxdepth;

    public Tr()
    {
      this.root = new Node<T>();
      this.maxdepth = 6;
    }

    public Tr(int maxdepth)
    {
      this.root = new Node<T>();
      this.maxdepth = maxdepth;
    }

    public IEnumerable<T> SearchExact(string key)
    {
      var node = root;

      for (var i = 0; i < key.Length; i++)
      {

        if (!node.children.TryGetValue(key[i], out node))
        {
          //return new List<T>();
          return Enumerable.Empty<T>();
        }


      }
      
      return node.values;

    }

    public IEnumerable<T> Search(string key)
    {

      var node = root;

      for (var i = 0; i < key.Length; i++)
      {

        if (!node.children.TryGetValue(key[i], out node))
        {
          //return new List<T>();
          return Enumerable.Empty<T>();
        }


      }


      //return CollectAll(node, key.Length);

      var results = new List<T>();

      CollectAll(node, key.Length, results);

      return results;

    }

    public void CollectAll(Node<T> node, int depth, List<T> results)
    {


      results.AddRange(node.values);


      foreach (var child in node.children.Values)
      {
        CollectAll(child, ++depth, results);
      }



      /*
    var result = new List<T>();



    result = node.children.Values.SelectMany(x => CollectAll(x, ++depth)).ToList();

    result.AddRange(node.values);

    return result;
          */

      /*
      foreach (var child in node.children.Values)
      {
        foreach (var val in CollectAll(child, ++depth))
        {
          yield return val;
        }
      }

      foreach (var val in node.values)
      {
        yield return val;
      }
      */

    }

    public void Insert(string key, T value)
    {
      if (key.Length > 0)
      {
        Insert(key, value, root, 0);
      }

    }

    public void Insert(string key, T value, Node<T> node, int depth)
    {

      if ((depth > maxdepth) || ((depth + 1) > key.Length))
      {
        node.values.Add(value);
        return;
      }

      Node<T> n = new Node<T>();

      if (!node.children.TryGetValue(key[depth], out n))
      {
        n = new Node<T>();
        node.children.Add(key[depth], n);

      }

      Insert(key, value, n, ++depth);



    }



  }
}