using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Gsof.Extensions;
using LiteDB;

namespace Gsof.D2RML.Utils;

public class Database(string path) : IDisposable
{
    private readonly LiteDatabase _database = new LiteDatabase(path);

    public static Database Instance { get; } = new Database(("./data.db"));

    public T? Get<T>(Expression<Func<T, bool>> filter)
    {
        var collection = _database.GetCollection<T>(typeof(T).Name);

        return collection.FindOne(filter);
    }

    public IEnumerable<T> Get<T>()
    {
        var collection = _database.GetCollection<T>(typeof(T).Name);

        return collection.FindAll();
    }

    public void Insert<T>(T t)
    {
        var collection = _database.GetCollection<T>(typeof(T).Name);

        collection.Insert(t);
    }

    public void Update<T>(T t)
    {
        var collection = _database.GetCollection<T>(typeof(T).Name);

        collection.Update(t);
    }

    public int Delete<T>(Expression<Func<T, bool>> predicate)
    {
        var collection = _database.GetCollection<T>(typeof(T).Name);

        return collection.DeleteMany(predicate);
    }


    public void Dispose()
    {
        _database.Dispose();
    }
}