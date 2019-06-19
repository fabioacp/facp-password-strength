# FACP - Password Strength Checker

## Table of Contents
  - [Introduction](#introduction)
    - [How it works](#how-it-works)
    - [Strength](#strength)
    - [Data Breaches](#data-breaches)
  - [Technologies](#technologies)
    - [dotNet Core](#dotnet-core)
    - [Test Frameworks](#test-frameworks)
  - [Application Instructions](#application-instructions)
    - [Clone](#clone)
    - [Restore](#restore)	
    - [Build](#build)
    - [Test](#test)
    - [Run](#run)


## Introduction

We are often asked to create an account by entering a login and password to access a web site or sensitive data of a system. Because some passwords are easy to break using brute-force techniques, it is common to give feedback to users to show the strength of the chosen password by signaling to the user whether or not their password is so secure.

This repository contains details about this password strength checker and also instructions on how it works.

### How it works

### Strength
This a score based strength validation.

- Characters type

  - Digits: '0', '1', '2', '3', '4', '5', '6', '7', '8', '9';
  - Lower cases: 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z';
  - Upper cases: 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z';
  - Punctuations:'!', '"', '#', '%', '&', ''', '(', ')', '*', ',', '-', '.', '/', ':', ';', '?', '@', '[', '\', ']', '_', '{', '}';
  - Separators: ' ';
  - Symbols: '$', '+', '<', '=', '>', '^', '`', '|', '~';
  - Special Characteres: Extended ASCII Codes

- Score
  - Digits: +1
  - Lower cases: +1
  - Upper cases: +1
  - Punctuations: +1;
  - Separators: +1;
  - Symbols: +1;
  - Special Characteres: +1;
  
  There is an extra score when the password repeats a character type; (score +1);
  
- Rules

  |Strength|Score rules
  |-|-|
  |Very Weak|score <= 3|
  |Weak|score > 3 && score <= 5|
  |Reasonable|score > 5 && score <= 10|
  |Strong|score > 10 && score <= 11|
  |Very Strong|score > 11|

Examples:

- Case 1:

  - Password: `Test`;

  - `T`:  Upper case = +1
  - `e`:  Lower case = +1
  - `s`:  Extra lower case not in sequence order= +1

  - Total score: `3`;
  - Rule: `score <= 3` 
  - Strength: `Very Weak`
  
- Case 2:

  - Password: `R@nD0n!#m1c`;

	- `R`:  Upper case = +1
	- `D`:  Extra upper case = +1
	- `n`:  Lower case = +1
	- `c`:  Extra lower case = +1
	- `@`:  Punctuations = +1
	- `!`:  Extra Punctuations = +1
	- `0`:  Digit = +1

  - Total score: `7`;
  - Rule: `score > 5 && score <= 10`;
  - Strength: `Reasonable`.

### Data Breaches
A data breach occurs when there is an unauthorized entry point into a corporation’s databased that allows cyber hackers to access customer data such as passwords, credit card numbers, Social Security numbers, banking information, driver’s license numbers, medical records, and other sensitive information.

As additional functionality, this service checks whether the password has appeared in data breaches, and provide the number of times the password has appeared in those breaches.

To check that, this program uses “Have  I Been Pwnd API V2” using a mathematical property called `k-anonymity` and within the scope of Pwned Passwords, it works like this: imagine if you wanted to check whether the password "P@ssw0rd" exists in the data set. The SHA-1 hash of that string is "21BD12DC183F740EE76F27B78EB39C8AD972A757" so what we're going to do is take just the first 5 characters, in this case, that means "21BD1". That gets sent to the Pwned Passwords API and it responds with 475 hash suffixes (that is everything after "21BD1") and a count of how many times the original password has been seen. 

For example:
	
- (21BD1) 0018A45C4D1DEF81644B54AB7F969B88D65:1 (password "lauragpe")
- (21BD1) 00D4F6E8FA6EECAD2A3AA415EEC418D38EC:2 (password "alexguo029")
- (21BD1) 011053FD0102E94D6AE2F8B83D76FAF94F6:1 (password "BDnd9102")
- (21BD1) 012A7CA357541F0AC487871FEEC1891C49C:2 (password "melobie")
- (21BD1) 0136E006E24E7D152139815FB0FC6A50B15:2 (password "quvekyny")

You can see it in action by trying a search for "P@ssw0rd" which will return the screen in the previous image. If we drop down and take a look at the dev tools, here's the actual request that's been made:

![Example](/images/request.png)

The password has been hashed client side and just the first 5 characters passed to the API (I'll talk more about the mechanics of that shortly). Here's what then comes back in the response:

![Example](/images/response.png)

More details [here](https://www.troyhunt.com/ive-just-launched-pwned-passwords-version-2/)

## Technologies

### dotNet Core

This application uses .Net Core and c#. To run the application you need to make you have the .Net Core 2.2 SDK installed 

More details [Here](https://dotnet.microsoft.com/download/dotnet-core/2.2) 

### Test Frameworks

- XUnit
  xUnit.net is a free, open source, community-focused unit testing tool for the .NET Framework. Written by the original inventor of NUnit v2, xUnit.net is the latest technology for unit testing C#, F#, VB.NET, and other .NET languages. xUnit.net works with ReSharper, CodeRush, TestDriven.NET, and Xamarin. It is part of the .NET Foundation and operates under their code of conduct. It is licensed under Apache 2 (an OSI approved license).

  More details [Here](https://xunit.net/) 
  
- Moq
  Moq (pronounced "Mock-you" or just "Mock") is the only mocking library for .NET developed from scratch to take full advantage of .NET Linq expression trees and lambda expressions, which makes it the most productive, type-safe and refactoring-friendly mocking library available. And it supports mocking interfaces as well as classes. Its API is extremely simple and straightforward and doesn't require any prior knowledge or experience with mocking concepts.

  More details [here](https://github.com/moq/moq4)

## Application Instructions

### Clone
From a terminal, change to the local directory where you want to clone your repository.

`git init`  

`git clone https://github.com/fabioacp/facp-password-strength.git`

`cd facp-password-strength/`    

### Restore
Restores the dependencies and tools of a project.

`cd FACP.PasswordStrength`

`dotnet restore` 

### Build
Builds the project and all of its dependencies.
 
`cd FACP.PasswordStrength`

`dotnet build`

### Test
.NET test driver used to execute unit tests.

`cd tests/FACP.PasswordStrength.Tests/`

`dotnet test`

### Run
Run the console application
 
`cd src/FACP.PasswordStrength.UIConsole/`

`dotnet run`
