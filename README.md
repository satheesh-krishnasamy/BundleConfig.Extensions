# MVC Bundle configuration extension methods
The only intension of this nuget library is to maintain the bundle config files in json file like in Asp.Net Core.

## Dependencies
1. Asp.Net MVC version 3 to 5
2. Newtonsoft.Json


## What is the difference?

Before the usage of this library the lengthier bundle config file look like this..

![Bundle config before](https://github.com/satheesh-krishnasamy/BundleConfig.Extensions/blob/master/Images/Bundles%20MVC%20way.png)

### Steps to use this library
1. Keep the bundle configurations in the json file.

![New Bundle configuratios in json files.](https://github.com/satheesh-krishnasamy/BundleConfig.Extensions/blob/master/Images/Bundle%20Config%20Json.png)

2. Add the bundles to the MVC bundle collection.

![Add the bundles to the MVC bundle collection.](https://github.com/satheesh-krishnasamy/BundleConfig.Extensions/blob/master/Images/Bundles%20MVC%20another%20way.png)
## Examples
You can find the sample usage of this library at https://github.com/satheesh-krishnasamy/BundleConfig.Extensions/tree/master/Sample.MVC.App

## Benefits
    This nuget package helps you to keep the MVC bundles in json files.
    Avoid lengthier C# code lines to add the MVC bundles.
    Organize the MVC bundles like views, scripts, and css folders.

## Limitations

The changes made to the bundle files will not be auto-refreshed without restarting the web application. This auto-refresh MVC bundles on bundle file change feature is not implemented since implementing this feature requires using file-watcher and doing this refreshing job in background. Doing so may result in unpredictable effects.

For example,
The refreshed/updated bundle may be in use for a current request. Updating it will require the bundle to be removed and re-added to the MVC bundle table.
IIS may go for sleep when there are no more active requests for certain amount of time. The background thread which does this file-watch and updating the MVC bundle may also go for sleep or killed by IIS.


## Authors
Satheeshkumar Krishnsamy

## License
This project is licensed under the MIT License.
