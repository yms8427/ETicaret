using System;
using System.Collections.Generic;
using System.Text;

namespace Yms.Common.Caching.Abstractions
{
    public interface ICacheManager
    {
        void Add<T>(string key, T data) where T : class;
        T Get<T>(string key) where T : class;
        T GetOrCreate<T>(string key, Func<T> function) where T : class;
        bool Update<T>(string key, Action<T> action) where T : class;
    }

    //class Fake
    //{
    //    public int Sum()
    //    {
    //        return 5;
    //    }
    //}

    //class Other
    //{

    //    public int ExternalSum(Func<int> function)
    //    {
    //        return function();
    //    }
    //}

    //class Main
    //{
    //    public void Print()
    //    {
    //        var o = new Other();
    //        var f = new Fake();
    //        Console.WriteLine("Toplam : {0}", o.ExternalSum(f.Sum));
    //        Console.WriteLine("Toplam : {0}", o.ExternalSum(() => 12));
    //        Console.WriteLine("Toplam : {0}", o.ExternalSum(OtherSum));
    //    }

    //    private int OtherSum()
    //    {
    //        return 4;
    //    }
    //}
}
