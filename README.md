# Rock Paper Scissors Lizard Spock Game API

Welcome to the Rock Paper Scissors Lizard Spock (RPSSL) Game API! This .NET 7 Web API provides a backend service for the classic game with an added twist.

## Table of Contents
- [Features](#features)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [API Endpoints](#api-endpoints)
- [Testing & UI](#testing--ui)
- [Contribution & Feedback](#contribution--feedback)

## Features

- **Choices Overview**: View all available game choices.
- **Random Choice**: Get a random game choice made by the computer.
- **Play**: Submit your move and get the game result against the computer's choice.
- **Scoreboard**: View 10 most recent game results.
- **Scoreboard Reset**: Reset the game scoreboard.

## Prerequisites

- .NET SDK 7.0+
- Docker (optional)

## Getting Started

### Clone the Repository

```bash
git clone https://github.com/radnenad/RPSSL.git
cd RPSSL
```

### Build and Run with .NET CLI

```bash
dotnet build
dotnet run --project Web.API/Web.API.csproj
```

### Docker Build and Run

For Docker enthusiasts:

```bash
docker build -t rpssl-game-api .
docker run -p 5100:80 rpssl-game-api
```
Once the application is running, navigate to http://localhost:5100 in your browser or in your API testing tool.

## API Endpoints

- **Choices Overview**: `GET /choices`
  - Returns an array of available choices. e.g., rock, paper, scissors, lizard, spock.
  
- **Random Choice**: `GET /choice`
  - Returns a random choice made by the computer.
  
- **Play**: `POST /play`
  - Body: `{ "player": <choice-id> }`
  - Returns the result of the game based on your choice against the computer's.
  
- **Scoreboard**: `GET /scoreboard`
  - Returns a list of recent game results.
  
- **Scoreboard Reset**: `PUT /scoreboard/reset`
  - Resets the scoreboard and returns a `204 No Content` upon successful reset.

## Testing & UI

To test the API, you can:
- Access the live version of the API hosted on Render: [RPSSL Game API on Render](https://app-rpssl.onrender.com)
- Use the provided frontend UI: [RPSSL Game UI](https://codechallenge.boohma.com/). To do so, enter the root URL of the live API version when prompted.

## Contribution & Feedback

Your feedback and contributions are welcome! Please feel free to submit pull requests or raise issues on the GitHub repo.
