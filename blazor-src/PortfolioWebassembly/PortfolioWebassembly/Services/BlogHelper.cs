using PortfolioWebassembly.ViewModels;


namespace PortfolioWebassembly.Services
{
    public static class BlogHelper
    {
        public static List<Blog> GetBlogs()
        {
            return
                [
                    new Blog
                    {
                        Id = 3,
                        Title = "Is there a better way to read value from dictionaries in C# without error?",
                        Description = "Are you using TryGetValue or ContainsKey in C# dictionaries the right way? Small choices can have big impacts on performance. Discover which approach is faster, where it shines, and how it might transform your code.",
                        Tags = ["c#", "dictionary", "benchmark"],
                        Image = "csharp dictionary tryget vs containskey blog banner.png",
                        Date = "22 November, 2024",
                        Link = "csharp-dictionary-containskey-vs-trygetvalue-benchmark",
                        Content = @"<p>
            When working with dictionaries in C#, retrieving a value safely and efficiently is critical, especially in performance-sensitive applications. Two common approaches for reading values safely are:
        </p>
        <ol>
            <li>
                Checking if the key exists with <code>ContainsKey</code> and then retrieving the value.
            </li>
            <li>
                Using the <code>TryGetValue</code> method.
            </li>
        </ol>
        <p>But which one performs better? Let’s dive into the details with a benchmark comparison.</p>
        <h5 class=""mt-4"">
            The two approaches
        </h5>
        <ol>
            <li>
                <div>
                    <p><b>Using <code>ContainsKey</code> and retrieving the value</b></p>
                    <p>
                        This approach separates the key existence check and value retrieval:
                    </p>
    <pre class=""line-numbers"">
        <code class=""language-csharp"">if (dictionary.ContainsKey(key))
        {
            var value = dictionary[key];
            // Use the value
        }</code>
    </pre>
                    <ul class=""my-3"">
                        <li>Performs two separate operations: key check and retrieval.</li>
                    </ul>
                </div>
            </li>
            <li>
                <div>
                    <p><b>Using <code>TryGetValue</code></b></p>
                    <p>
                        The <code>TryGetValue</code> method allows you to check for a key and retrieve its associated value in a single call:
                    </p>
    <pre class=""line-numbers"">
        <code class=""language-csharp"">if (dictionary.TryGetValue(key, out var value))
        {
            // Use the value
        }</code>
    </pre>
                    <ul class=""my-3"">
                        <li>Combines existence check and value retrieval.</li>
                        <li>Avoids a second lookup.</li>
                    </ul>
                </div>
            </li>
        </ol>
        
        <h5>Why does performance differ?</h5>
        <p>
            At its core, a dictionary lookup involves searching for the key in its underlying hash table. In the <code>TryGetValue</code> method, this search is performed once, combining the key check and retrieval into a single operation. On the other hand, the <code>ContainsKey</code> approach searches for the key twice—once during the existence check and again during the value retrieval—leading to redundant work.
        </p>
        <h5>
            Benchmarking the approaches
        </h5>
        <p>
            To measure the performance difference, we set up a simple benchmark using a dictionary with a large number of entries. The benchmark tests the time taken to retrieve values using both approaches.
        </p>
        <p>I created the following class to benchmark the prformance of these two methods:</p>
    <pre class=""line-numbers"">
        <code class=""language-csharp"">using BenchmarkDotNet.Attributes;
        
        namespace Benchmarking
        {
            public class DictionaryUsage
            {
                Dictionary<string, string> dictionary;
                const int countOfElements = 10000;
        
                public DictionaryUsage() 
                {
                    // Initialize dictionary
                    dictionary = new Dictionary<string, string>();
                    for (int i = 0; i < countOfElements; i++)
                    {
                        dictionary.Add($""key-{i}"", $""value-{i}"");
                    }
        
                }
        
                [Benchmark(Baseline = true)]
                public void ReadWithoutCheck()
                {
                    for (int i = 0; i < countOfElements; i++)
                    {
                        _ = dictionary[$""key-{i}""];
                    }
                }
        
                [Benchmark]
                public void ReadWithContainsKey()
                {
                    for(int i=0; i< countOfElements; i++)
                    {
                        if(dictionary.ContainsKey($""key-{i}""))
                        {
                            _ = dictionary[$""key-{i}""];
                        }
                    }
                }
        
                [Benchmark]
                public void ReadWithTryGetValue()
                {
                    for (int i = 0; i < countOfElements; i++)
                    {
                        if (dictionary.TryGetValue($""key-{i}"", out var val))
                        {
                            //
                        }
                    }
                }
            }
        }</code>
    </pre>
        <p>And run this program to benchmark the performance as follows:</p>
    <pre class=""line-numbers"">
        <code class=""language-csharp"">internal class Program
        {
            static void Main(string[] args)
            {
                var summary = BenchmarkRunner.Run<DictionaryUsage>();
            }
        }</code>
    </pre>

        <p>Here is the result of benchmark:</p>

        <div class=""text-center mb-3 mt-2"">
            <img src=""../assets/images/blogs/csharp dictionary tryget vs containskey benchmark result.png"" alt=""result of the benchmark run""/>
        </div>

        <p><b>Summary of the observations:</b></p>
        <p>Here I took <code>ReadWithoutCheck</code> function as baseline, where we read directly from the dictionary without checking whether it has the key or not. 
            <br/>(Note: this is the most unsafe way to read value from dictionary, as it is possible that the dictionary may not contain the key-value pair you are looking for, thus throwing an exception)
            <br/><br/>It takes <b>346 &micro;s</b> to read 10k values from the dictionary.
        </p>
        <ul>
            <li>
                <code>ReadWithContainsKey</code> function is more than 2x slower compared to baseline, taking <b>759 &micro;s</b>.
            </li>
            <li>
                <code>ReadWithTryGetValue</code> function is almost as fast as baseline, taking only <b>387 &micro;s</b>.
            </li>
        </ul>

        <p>Here, it is visibly clear that using <code>TryGetValue</code> gives a safe code at no additional performance overhead.</p>

        <h5>Conclusion</h5>
        <p>
            The results clearly show that <code>TryGetValue</code> is the more efficient choice. By combining the key existence check and value retrieval into a single operation, it avoids the redundant key lookup that occurs with <code>ContainsKey</code>.
            When performance matters, especially in scenarios involving frequent dictionary lookups, prefer <code>TryGetValue</code> over <code>ContainsKey</code>. It’s a small change, but it can make a big difference in your application’s efficiency.
        </p>"
                    },
                    new Blog
                    {
                        Id = 2,
                        Title = "Git is not picking up changes when renamed a file with upper case to lower case change?",
                        Description = "Ever renamed a file in Visual Studio from uppercase to lowercase, only to find Git doesn't recognize the change? This case-insensitive quirk can disrupt your version control. Here's a quick, foolproof way to fix it, ensuring smooth tracking across all systems.",
                        Tags = ["git", "visual-studio", "github"],
                        Image = "git mv.png",
                        Date = "09 November, 2024",
                        Link = "renamed-file-not-picked-by-git-changes",
                        Content = @"<p>
            If you've ever renamed a file in Visual Studio, changing its case (e.g., from FILE.CS to File.cs), you may have noticed an odd quirk: Git doesn't recognize this as a change when tracked within Visual Studio. This is because Git, by default, doesn't detect case-only changes on case-insensitive file systems like Windows. Here’s a quick guide on how to resolve this issue using the git mv command.
        </p>
        <h5 class=""mt-4"">
            Why Git Ignores Case Changes
        </h5>
        <p>
            Git tracks files based on case-sensitive names. But on case-insensitive systems (Windows and macOS, by default), renaming from uppercase to lowercase may not trigger Git’s tracking mechanism, as it treats FILE.CS and File.cs as the same file. This results in Visual Studio not showing the rename as a change, potentially causing issues when syncing with remote repositories or collaborating with team members.
        </p>
        <h5 class=""mt-4"">
            The Solution: Use <code>git mv</code>
        </h5>
        <p>
            The most reliable way to ensure Git recognizes case-only renames is to use the <code>git mv</code> command. Here’s what to do for renaming a file from uppercase to lowercase (or vice versa) and making sure Git tracks the change:
        </p>
        <pre class=""line-numbers"">
        <code class=""language-bash"">git mv FILE.cs File.cs</code>
        </pre>
        <p>
            Now you'll be able to see the file getting tracked in the git changes tab when you open Visual Studio.
        </p>"
                    },
                    new Blog
                    {
                        Id = 1,
                        Title = "How to delete all files in a folder ending with specific name?",
                        Description = "Managing files from the Command Prompt (CMD) can be a real time-saver, especially when you need to delete multiple files with a specific naming pattern. Let's find out how.",
                        Tags = ["cmd", "windows", "file"],
                        Image = "del cmd.png",
                        Date = "09 November, 2024",
                        Link = "delete-files-from-command-promt-ending-with-specific-name",
                        Content = @"<p>
                                        Managing files from the Command Prompt (CMD) can be a real time-saver, especially when you need to delete multiple files with a specific naming pattern. Let’s say you want to delete all files ending with ""_copy"" — you can do this either within a single directory or across all subdirectories. Here’s how to handle both scenarios using CMD.
                                    </p>
                                    <h5 class=""mt-4"">
                                        Command to delete only inside current folder
                                    </h5>
                                    <p>
                                        If you only need to delete files ending with ""_copy"" in the current directory, you can use the del command directly. Open your command prompt in the current directory and enter the following:
                                    </p>
                                    <pre class=""line-numbers"">
        <code class=""language-bash"">del *_copy.*</code>
                                    </pre>
                                    <p>
                                        This command deletes all files in the current directory with names ending in _copy followed by any extension, such as file_copy.txt, image_copy.jpg, etc.
                                    </p>
                                    <h5 class=""mt-4"">
                                        Command to recursively delete from all folders inside the current folder
                                    </h5>
                                    <p>
                                        If your target files are spread across subdirectories and you want to delete them all in one go, you’ll need a different approach using the for loop. This method tells CMD to delete files matching the pattern across the current folder and all subfolders.
                                    </p>
                                    <pre class=""line-numbers"">
        <code class=""language-bash"">for /r %i in (*_copy.*) do del ""%i""</code>
                                    </pre>
                                    <p>
                                        Here’s what each part does:
                                        <ul>
                                            <li><code>for /r</code>: Searches the current directory and all subdirectories.</li>
                                            <li><code>%i in (*_copy.*)</code>: Finds files that end with _copy and have any file extension.</li>
                                            <li><code>do del ""%i""</code>: Deletes each matching file it finds.</li>
                                        </ul>
                                    </p>
                                    <p>
                                        Be careful: this command deletes files permanently without a prompt, so double-check before running it.
                                    </p>"
                    }
                ];

        }

        public static Blog GetBlog(string slug)
        {
            return GetBlogs().FirstOrDefault(b => b.Link == slug) ?? throw new KeyNotFoundException($"Blog with slug '{slug}' not found");
        }
    }
}
