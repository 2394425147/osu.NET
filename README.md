<div align="center">

# osu-sharp

[![License](https://img.shields.io/badge/License-GPLv3-blue?style=flat-square)](https://www.gnu.org/licenses/gpl-3.0)
[![NuGet](https://img.shields.io/nuget/v/osu-sharp?color=blue&style=flat-square)](https://www.nuget.org/packages/osu-sharp)
[![API Coverage](https://img.shields.io/badge/API%20Coverage-65%25-yellowgreen?style=flat-square)](#api-coverage)

A modern and well documented API wrapper for the osu! API v2.<br/>
This wrapper <ins>currently only supports public scope endpoints</ins>.<br/>

[Installation](#-installation) • [Getting Started](#-getting-started) • [Contribute](#-contribute) • [API Coverage](#-api-coverage)<br/>
</div>

<div align="center">
<i>Made with ❤️ by minisbett for the osu! community</i>
</div>

### ✨ Features
✔️ **Extensive API documentation** – Detailed documentations, beyond the official API docs  
✔️ **Seamless Integration** – Designed with .NET Generic Host in mind  
✔️ **Easy Error Handling** – Result pattern for API responses with error-handling assistance  
✔️ **Flexible Authentication Flow** – Easy-to-use authorization infrastructure  
✔️ **Actively Maintained** – Contributions welcome!  
### 📦 Installation  
osu-sharp is available via NuGet:
```sh
# via the dotnet CLI
dotnet add package osu-sharp

# via the Package Manager CLI
Install-Package osu-sharp
```

## 🚀 Getting Started

This library is primary designed to be integrated with the [.NET Generic Host](https://learn.microsoft.com/en-us/dotnet/core/extensions/generic-host?tabs=appbuilder). It can also be used stand-alone, an overview on how to get started without the .NET Generic Host can be found [below](#️-using-osu-sharp-stand-alone).

Every model and every endpoint is well documented, including:
- Documentation of [almost](#-contribute) everything, beyond what the official osu! API documentation provides
- References to the osu! API documentation & osu-web source-code
- API notes found in the official osu! API documentation
- Information about the API errors to expect on each endpoint

As for the authorization flow, there are multiple `IOsuAccessTokenProvider` to choose from:
| IOsuAccessTokenProvider    | Authorization Flow | Usage |
| -------- | ------- | ------- |
| `OsuClientAccessTokenProvider`  | Authorization using client ID & secret    | `new(id, secret)`<br/> `.FromEnvironmentVariables(id, secret)` |
| `OsuStaticAccessTokenProvider` | Authorization using a static access token     | `new(accessToken)` |
| `OsuDelegateAccessTokenProvider`    | Authorization using an access token provided via a delegate (eg. for fetching from a database)    | `new(cancellationToken => ...)`

> [!TIP]
> You can also write your own access token provider by inheriting `IOsuAccessTokenProvider`.

### ⚙️ Using osu-sharp with the .NET Generic Host
The API wrapper provides an extension method for registering the `OsuApiClient`. It is registered as a scoped service, and the access tokens are provided via a singleton `IOsuAccessTokenProvider`. Optionally, the API client can be configured.

Here is an example on how to register an `OsuApiClient`:
```cs
IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(logging =>
    {
        logging.AddSimpleConsole();
    })
    .ConfigureServices((context, services) =>
    {
        services.AddHostedService<TestService>();
        services.AddOsuApiClient(
            OsuClientAccessTokenProvider.FromEnvironmentVariables("OSU_ID", "OSU_SECRET"),
            (options, services) =>
            {
                options.EnableLogging = true;
            });
    })
  .Build();
```
The `OsuApiClient` can then be consumed via dependency injection:
```cs
public class TestService(OsuApiClient client) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        APIResult<UserExtended> result = await client.GetUserAsync("mrekk", cancellationToken);
    }
}
```

### 🏗️ Using osu-sharp stand-alone
To use the `OsuApiClient` without the .NET Generic Host, there are certain criteria to be considered, as this library was primarily designed with it in mind.

Briefly said, you create an instance of the `IOsuAccessTokenProvider` providing the desired authorization flow, and using that you create an instance of the `OsuApiClient`:
```cs
OsuClientAccessTokenProvider provider = OsuClientAccessTokenProvider.FromEnvironmentVariables("OSU_ID", "OSU_SECRET");

OsuApiClientOptions options = new()
{
    EnableLogging = false // false by default, do *not* set it to true for stand-alone usage
};

OsuApiClient client = new(provider, options, null! /* ILogger instance, set to null for stand-alone usage*/);
```
> [!IMPORTANT]
> Since the logging is based on the `Microsoft.Extensions.Logging.ILogger<T>`, a part of the .NET Generic Host, logging needs to be disabled and the logger set to null.

## ⚠️ Error Handling

The response returned from the endpoint methods are of type `APIResult<T>`. This type wraps the data returned from the osu! API, or provides the error if the API returned one. Additionally, osu-sharp interprets the error message provided by the osu! API and provides an `APIErrorType` for common errors. This can help handle different kinds of errors in individual ways.

> [!TIP]
> The xmldoc for the entrypoint methods always provide the `APIErrorType` the endpoints are expected to return, as well as when they do it, so you always know which errors to expect.

Here is an example on how to handle the response of a `GetUserBeatmapScoreAsync` API call:
```cs
APIResult<UserBeatmapScore> result = await client.GetUserBeatmapScoreAsync(4697929, 7981241, cancellationToken: cancellationToken);
if (result.IsSuccess)
    logger.LogInformation("PP: {PP}", result.Value!.Score.PP);
else if (result.Error.Type is APIErrorType.BeatmapNotFound)
    logger.LogError("Beatmap not found.");
else if (result.Error.Type is APIErrorType.UserOrScoreNotFound)
    logger.LogError("The user was not found or has no scores on the beatmap.");
else
    logger.LogError("{Message}", result.Error.Message);
```

## 🌱 Contribute

This library is continuously maintained, and contributions are always welcome. Whether it's improving documentation, adding new features, or updating existing code, every contribution helps keep the project up-to-date and easy-to-use.

**📝 Improve Documentation**  
Some parts of the documentation are still missing. If you encounter some, and you can provide information about it, any contributions filling the gaps are much appreciated!

**🔧 Add or Update API Endpoints**  
Not all endpoints the API provides are implemented. If you require a missing endpoint, feel free to propose it using a [GitHub issue](https://github.com/minisbett/osu-sharp/issues) or implement it via a [pull request](https://github.com/minisbett/osu-sharp/pulls). Similarily, feel free to contribute if you notice an outdated endpoint, as the osu! API evolves over time.

**🆙 Update API Models**  
If any API models are outdated due to changes in the osu! API, feel free to report it via a [GitHub issue](https://github.com/minisbett/osu-sharp/issues) or update them via a [pull request](https://github.com/minisbett/osu-sharp/pulls).

**🗣️ Report Issues**  
If something isn't working as expected, open an issue with a detailed description so the problem can be addressed promptly.

## 📜 API Coverage

Below is a list of all planned and implemented osu! API endpoints. If you'd like to suggest a missing endpoint or add one yourself, feel free to create an issue or pull request.  

> ✅ = Implemented | ❌ = Not Implemented  

#### Beatmap Packs 🎵
- ❌ `/beatmaps/packs`
- ✅ `/beatmaps/packs/{tag}`

#### Beatmaps 🎼
- ✅ `/beatmaps?id[]`
- ✅ `/beatmaps/lookup?checksum`
- ✅ `/beatmaps/lookup?filename`
- ✅ `/beatmaps/{beatmap}`
- ✅ `/beatmaps/{beatmap}/attributes`
- ✅ `/beatmaps/{beatmap}/scores`
- ✅ `/beatmaps/{beatmap}/scores/users/{user}`
- ✅ `/beatmaps/{beatmap}/scores/users/{user}/all`

#### Beatmap Sets 📦
- ✅ `/beatmapsets/lookup`
- ✅ `/beatmapsets/{beatmapset}`

#### Changelogs 📜
- ✅ `/changelog`
- ✅ `/changelog/{buildOrStream}`
- ✅ `/changelog/{stream}/{build}`

#### Comments 💬
- ❌ `/comments`
- ✅ `/comments/{comment}`

#### Events 📅
- ❌ `/events`

#### Forums 📝
- ❌ `/forums/topics`
- ❌ `/forums/topics/{topic}`
- ❌ `/forums`
- ❌ `/forums/{forum}`

#### Home 🏠
- ❌ `/search`

#### Matches 🎮
- ❌ `/matches`
- ❌ `/matches/{match}`

#### Multiplayer 🌍
- ❌ `/rooms/{room}/playlist/{playlist}/scores`

#### News 📰
- ❌ `/news`
- ✅ `/news/{news}`
- ✅ `/news/{news}?id`

#### Rankings 🏆
- ✅ `/rankings/kudosu`
- ❌ `/rankings/{mode}/{type}`
- ✅ `/spotlights`

#### Scores 📊
- ❌ `/scores`

#### Users 👤
- ✅ `/users/{user}/kudosu`
- ✅ `/users/{user}/scores/{type}`
- ✅ `/users/{user}/beatmapsets/{type}`
- ✅ `/users/{user}/recent_activity`
- ✅ `/users/{user}/{mode?}`
- ✅ `/users?id[]`

#### Wiki 📖
- ✅ `/wiki/{locale}/{path}`