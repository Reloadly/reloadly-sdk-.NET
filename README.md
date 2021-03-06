<img src="icon.png" width="100" height="88" align="right" alt="reloadly-dotnet-icon"/>

# Reloadly SDK for .NET Core

[![CircleCI][circle-ci-badge]][circle-ci-url]
[![MIT][mit-badge]][mit-url]
[![nuget-airtime-badge]][nuget-airtime-url]
[![nuget-authentication-badge]][nuget-authentication-url]
[![nuget-core-badge]][nuget-core-url]
[![codecov](https://codecov.io/gh/Reloadly/reloadly-sdk-.NET/branch/main/graph/badge.svg?token=M6750A3FJX)](https://codecov.io/gh/Reloadly/reloadly-sdk-.NET)

The **Reloadly SDK for .NET** enables developers to easily work with [Reloadly Services][reloadly-main-site]
and build scalable solutions. You can get started in minutes using NuGet.

* [SDK Homepage][sdk-website] (Coming soon)
* [Sample Code][sample-code]
* [API Docs][docs-api]
* [Issues][sdk-issues]
* [Giving Feedback](#giving-feedback)
* [Getting Help](#getting-help)

## Getting Started

#### Sign up for Reloadly

Before you begin, you need a Reloadly account. Please see the [Sign Up for Reloadly][reloadly-signup-help] section of
the knowledge-base for information about how to create a Reloadly account and retrieve
your [Reloadly APIs credentials][api-credentials-help].

#### Minimum Requirements

To run the SDK you will need **.NET Core 2.0** or higher.

## Using the SDK Modules

The SDK is made up of several modules such as **Authentication, Airtime, etc...**, you can alternatively add
dependencies for the specific services you use only. For example : Authentication & Airtime
***(currently all modules have the same version, but this may not always be the case)***

Add specific dependencies to your project's build file:

```
Install-Package "Reloadly.Airtime"
Install-Package "Reloadly.Authentication"
```

## Getting Help

GitHub [issues][sdk-issues] is the preferred channel to interact with our team. Also check these community resources for
getting help:

* Checkout & search our [knowledge-base][reloadly-knowledge-base]
* Talk to us live on our chat tool on our [website][reloadly-main-site] (bottom right)
* Ask a question on [StackOverflow][stack-overflow] and tag it with `reloadly-dotnet-sdk`
* Articulate your feature request or upvote existing ones on our [Issues][features] page
* Take a look at our [youtube series][youtube-series] for plenty of helpful walkthroughs and tips
* Open a case via with the [Reloadly Support Center][support-center]
* If it turns out that you may have found a bug, please open an [issue][sdk-issues]

## Documentation

Please see:

- The [Usage and Sample Code](SAMPLE-CODE) page for code reference including how to set up, customize and use the SDK.
- The [API docs][api-docs] for the most up-to-date API documentation.
- The [example console application](Reloadly.Console.Example/README) for a reference implementation.

## Giving Feedback

We need your help in making this SDK great. Please participate in the community and contribute to this effort by
submitting issues, participating in discussion forums and submitting pull requests through the following channels:

* Submit [issues][sdk-issues] - this is the preferred channel to interact with our team
* Come join the Reloadly .NET community chat on [Gitter][gitter]
* Articulate your feature request or upvote existing ones on our [Issues][features] page
* Send feedback directly to the team at oss@reloadly.com

## License

This project is licensed under the MIT license. See the [LICENSE](LICENSE) file for more info.

[reloadly-main-site]: https://www.reloadly.com/

[sdk-website]: https://sdk.reloadly.com/dotnet

[reloadly-signup-help]: https://faq.reloadly.com/en/articles/2307724-how-do-i-register-for-my-free-account

[api-credentials-help]: https://faq.reloadly.com/en/articles/3519543-locating-your-api-credentials

[sdk-issues]: https://github.com/Reloadly/reloadly-sdk-.NET/issues

[sdk-license]: http://www.reloadly.com/software/apache2.0/

[gitter]: https://gitter.im/reloadly/reloadly-sdk-dotnet

[sample-code]: https://github.com/Reloadly/reloadly-sdk-.NET/blob/main/SAMPLE-CODE.md

[docs-api]: https://developers.reloadly.com

[features]: https://github.com/reloadly/reloadly-sdk-.NET/issues?q=is%3Aopen+is%3Aissue+label%3A%22feature-request%22

[api-docs]: https://developers.reloadly.com

[dotnetdoc]: https://reloadly.dev/reloadly-dotnet

[mit-badge]: http://img.shields.io/:license-mit-blue.svg?style=flat

[mit-url]: https://github.com/reloadly/reloadly-sdk-dotnet/raw/main/LICENSE

[maven-badge]: https://img.shields.io/maven-central/v/software.reloadly/reloadly-dotnet/reloadly.svg

[maven-url]: https://search.maven.org/search?q=g:software.reloadly

[circle-ci-badge]: https://circleci.com/gh/Reloadly/reloadly-sdk-.NET.svg?style=svg&circle-token=c127c399c38db62c8fd53c085a6b932b2ecf1e4a

[circle-ci-url]: https://circleci.com/gh/Reloadly/reloadly-sdk-.NET/tree/main

[codecov-badge]: https://codecov.io/gh/reloadly/reloadly-sdk-dotnet/branch/main/graph/badge.svg?token=8U89VKQ2BF

[codecov-url]: https://app.codecov.io/gh/reloadly/reloadly-sdk-dotnet

[youtube-series]: https://www.youtube.com/watch?v=TbXC4Ic8x30&t=141s&ab_channel=Reloadly

[reloadly-knowledge-base]: https://faq.reloadly.com

[stack-overflow]: http://stackoverflow.com/questions/tagged/reloadly-reloadly-sdk

[support-center]: https://faq.reloadly.com/en/articles/3423196-contacting-support

[nuget-airtime-badge]: https://img.shields.io/nuget/v/Reloadly.Airtime?label=Reloadly.Airtime

[nuget-airtime-url]: https://www.nuget.org/packages/Reloadly.Airtime

[nuget-authentication-badge]: https://img.shields.io/nuget/v/Reloadly.Authentication?label=Reloadly.Authentication

[nuget-authentication-url]: https://www.nuget.org/packages/Reloadly.Authentication

[nuget-core-badge]: https://img.shields.io/nuget/v/Reloadly.Core?label=Reloadly.Core

[nuget-core-url]: https://www.nuget.org/packages/Reloadly.Core




