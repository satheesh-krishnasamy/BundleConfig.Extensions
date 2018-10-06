MVC Bundle configuration extension methods

The only intension of this nuget library is to maintain the bundle config files in json file like in Asp.Net Core.


Benefits
    This nuget package helps you to keep the MVC bundles in json files.
    Avoid lengthier C# code lines to add the MVC bundles.
    Organize the MVC bundles like views, scripts, and css folders.

Limitations

    The changes made to the bundle files will not be auto-refreshed without restarting the web application.

    This auto-refresh MVC bundles on bundle file change feature is not implemented since implementing this feature requires using file-watcher and doing this refreshing job in background. Doing so may result in unpredictable effects.

    For example,
        The refreshed/updated bundle may be in use for a current request. Updating it will require the bundle to be removed and re-added to the MVC bundle table.
        IIS may go for sleep when there are no more active requests for certain amount of time. The background thread which does this file-watch and updating the MVC bundle may also go for sleep or killed by IIS.



