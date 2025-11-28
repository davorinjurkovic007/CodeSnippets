// C# Async Await Mistakes | Part 1 -- handle exceptions
// https://youtu.be/07CFRGlISVU?si=x7HKbDTZj030tDLp

//using AsyncWaitAllExample;

Console.WriteLine("Hello, World!");

//var cts = new CancellationTokenSource();
//
//var getRepo1Task = GitHubClient.GetReposAsync(1, cts.Token);
//var getRepo2Task = GitHubClient.GetReposAsync(2);
//var getRepo3Task = GitHubClient.GetReposAsync(3);
//
//cts.Cancel();
//
//var results = await Task.WhenAll(getRepo1Task, getRepo2Task, getRepo3Task);
//
//foreach (var result in results)
//{
//    Console.WriteLine(result);
//}

var tasks = new[]
{
    GitHubClient.GetReposAsync(1),
    GitHubClient.GetReposAsync(2),
    GitHubClient.GetReposAsync(3),
};

var whenAllTask = Task.WhenAll(tasks);

try
{
    var results = await whenAllTask;   
}
catch
{
    throw whenAllTask.Exception!;
}

Console.WriteLine($"{nameof(GitHubClient.GetHashCode)})");


public static class GitHubClient
{
    public static async Task<int> GetReposAsync(int repoNumber, CancellationToken cancellationToken= default)
    {
        await Task.Delay(TimeSpan.FromSeconds(repoNumber), cancellationToken);

        if(repoNumber == 2)
        {
            throw new OperationCanceledException($"Error gettin repo {repoNumber}");
        }

        throw new Exception($"Error getting repo {repoNumber}");
    }
}