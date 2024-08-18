# Elevator Simulation Console Application

## Overview

This console application simulates the operation of elevators in a building. It uses .NET Core 8, Object-Oriented Programming (OOP) principles, and a clean architecture approach to provide a robust and interactive simulation of elevator operations. The application allows users to call elevators, add or remove passengers, and monitor elevator statuses.

## Features

- **Elevator Simulation**: Simulate elevators moving between floors, with different types of elevators (Standard, High-Speed, Glass, Freight).
- **Passenger Management**: Add or remove passengers from elevators while ensuring weight and capacity limits are adhered to.
- **Real-Time Status Updates**: Display real-time status of elevators including their current floor, number of passengers, weight, and state.
- **Service Management**: Use dependency injection to manage services and repositories for elevators and passengers.

## Project Structure

- **Program.cs**: Entry point for the application that sets up dependency injection and starts the simulation.
- **Application**:
  - **Services**: Contains service classes and interfaces for controlling elevators and passengers.
  - **Interfaces**: Defines interfaces for elevator and passenger services.
- **Domain**:
  - **Entities**: Defines core entities like `Elevator` and `Passenger`.
  - **Enums**: Defines enums for `ElevatorState` and `ElevatorType`.
- **Infrastructure**:
  - **Data**: Contains the `InMemoryDatabase` context for simulating a database.
  - **Repositories**: Contains repository implementations for managing `Elevator` entities.
- **Startup.cs**: Configures dependency injection and service registration.

## Installation

1. **Clone the repository**:
   ```bash
   git clone https://github.com/your-username/elevator-simulation.git
   cd elevator-simulation
   ```

2. **Restore the dependencies**:
   ```bash
   dotnet restore
   ```

3. **Build the project**:
   ```bash
   dotnet build
   ```

4. **Set DVT.ConsoleApp as startup project**:
   ```bash
   dotnet restore
   ```

5. **Run the application**:
   ```bash
   dotnet run
   ```

## Usage

When you run the application, you will be presented with a menu to interact with the elevator system:

1. **Call Elevator**: Request an elevator to a specific floor.
2. **Add Passengers**: Add passengers to a selected elevator and move it to a destination floor.
3. **Remove Passengers**: Remove a specified number of passengers from an elevator.
4. **Exit**: Exit the simulation.

Follow the prompts to enter the necessary details such as floor numbers, elevator IDs, and passenger information.

## Development

- **Code Structure**: The application is structured using clean architecture principles with well-defined layers for services, repositories, and data access.

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request with your changes.
