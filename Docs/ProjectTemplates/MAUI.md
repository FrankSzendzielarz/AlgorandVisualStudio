# Algorand MAUI Solution

This guide shows how to use the Algorand MAUI template.

## Create the project

In a fresh instance of  Visual Studio 2022, hit File->New->Project

Type Algorand in the search box:

![image](https://user-images.githubusercontent.com/33515470/191026712-62f8a241-9d66-4dae-bed4-238183c84d98.png)

Select **Algorand MAUI Solution** and hit Next. **Important** Do not use Algorand MAUI App for this demo.

Give the project a name and hit Create:

![image](https://user-images.githubusercontent.com/33515470/191030371-74ecac3a-5d0f-467b-b8c1-2e3fb3d60e51.png)


## Project Structure

This is different to the other demos in that it gives you a solution architecture rather than a straightforward application.

With mobile apps accessing an Algorand node, there are a number of additional issues to overcome:

- how do you hide the access token so that it is not available in the application itself
- how do you throttle access to the algod token
- what if we want to tie server access to authentication mechanisms, so that only signed in users can use the algod node?

This template offer a skeleton to help address these issues. The MAUI App accesses the Algod node through a reverse proxy,
and the reverse proxy is included as part of the solution:

![image](https://user-images.githubusercontent.com/33515470/191032097-27fb90c0-39f9-49ef-b806-10476ccfd631.png)

Assuming you have a local sandbox nothing needs to change there. If you have an Algod node somewhere else the config
is found in the appsettings.json of the AlgorandReverseProxy project:

![image](https://user-images.githubusercontent.com/33515470/191032300-ec787ea4-ea09-4325-bc6c-e9dc1382760a.png)

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ReverseProxy": {
    "Routes": {
      "route1": {
        "ClusterId": "cluster1",
        "Match": {
          "Path": "algod/{*remainder}"
        },
        "Transforms": [
          { "PathRemovePrefix": "/algod" },
          {
            "RequestHeader": "X-Algo-API-Token",
            "Set": "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
          }

        ]

      }
    },
    "Clusters": {
      "cluster1": {
        "Destinations": {
          "destination1": {
            "Address": "http://localhost:4001/"
          }
        }
      }
    }
  },
  "Kestrel": {
    "Endpoints": {
      "Http": {
        "Url": "http://0.0.0.0:5000"
      },
      "Https": {
        "Url": "https://0.0.0.0:5001",
        
      }
    }
  }
}

```

For details on the configuration, please refer to [YARP](https://microsoft.github.io/reverse-proxy/index.html)

The MAUI application uses one Account, which you must update here:

![image](https://user-images.githubusercontent.com/33515470/191033084-b0ea2f30-c7a2-4c54-8fd8-3e3455111f2d.png)

Looking at MainPage.xaml.cs, please find the constructor and change the mnemonic according to guidance there:

```csharp
        public MainPage()
        {
            InitializeComponent();

            //Initialise with the URL of the server
            algorand = AlgorandUtilities.InitialiseAlgorandClient("https://10.0.2.2:5001/algod/");

            //After starting sandbox, run
            //  ./sandbox goal account list
            //to get the active accounts
            //and then run
            //  ./sandbox goal account export -a <one of the above addresses>
            //to find a mnemonic to a private key:
            string creatorMnemonic = "erosion turtle ride kingdom mass copy fantasy exile bronze swing more convince purity update fix citizen coffee tonight sibling wide fault shop hat above leopard";
            creator = new Account(creatorMnemonic);


        }
```
