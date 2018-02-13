# aspnetcore-sqlserver-cache-sample
https://docs.microsoft.com/ja-jp/aspnet/core/performance/caching/distributed#using-a-sql-server-distributed-cache

## キャッシュテーブルの作成
```
$ dotnet sql-cache create "Server=(localdb)\mssqllocaldb;Database=MyApp20180212;Trusted_Connection=True;MultipleActiveResultSets=true" dbo TestCache
```
