﻿using Dapr.Actors;

namespace IDemoActorInterface;

/// <summary>
/// Interface for Actor method.
/// </summary>
public interface IDemoActor : IActor
{
    /// <summary>
    /// Method to save data.
    /// </summary>
    /// <param name="data">DAta to save.</param>
    /// <returns>A task that represents the asynchronous save operation.</returns>
    Task SaveData(MyData data);

    /// <summary>
    /// Method to get data.
    /// </summary>
    /// <returns>A task that represents the asynchronous save operation.</returns>
    Task<MyData> GetData();

    /// <summary>
    /// A test method which throws exception.
    /// </summary>
    /// <returns>A task that represents the asynchronous save operation.</returns>
    Task TestThrowException();

    /// <summary>
    /// A test method which validates calls for methods with no arguments and no return types.
    /// </summary>
    /// <returns>A task that represents the asynchronous save operation.</returns>
    Task TestNoArgumentNoReturnType();

    /// <summary>
    /// Registers a reminder.
    /// </summary>
    /// <returns>A task that represents the asynchronous save operation.</returns>
    Task RegisterReminder();

    /// <summary>
    /// Unregisters the registered reminder.
    /// </summary>
    /// <returns>Task representing the operation.</returns>
    Task UnregisterReminder();

    /// <summary>
    /// Registers a timer.
    /// </summary>
    /// <returns>A task that represents the asynchronous save operation.</returns>
    Task RegisterTimer();

    /// <summary>
    /// Unregisters the registered timer.
    /// </summary>
    /// <returns>A task that represents the asynchronous save operation.</returns>
    Task UnregisterTimer();
}