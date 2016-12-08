﻿using System;
using System.Collections.Generic;

using RemoteExecution;

// Simple test client to demonstrate remoting functionality with some test input
namespace TestClient
{
    public class Program
    {
        const string RemoteExecutionName = "MyRemoteServer";

        public static void Main(string[] args)
        {
            Console.WriteLine("Creating proxy objects...");

            var proxy0 = RemoteExecutionClient.CreateRemoteInstance<int>(RemoteExecutionName);
            Console.WriteLine($"Created remote object with ID: {proxy0.ID}");

            var proxy1 = RemoteExecutionClient.CreateRemoteInstance<string>(RemoteExecutionName, "Hello, world!".ToCharArray());
            Console.WriteLine($"Created remote object with ID: {proxy1.ID}");

            var proxy2 = RemoteExecutionClient.CreateRemoteInstance<List<double>>(RemoteExecutionName);
            Console.WriteLine($"Created remote object with ID: {proxy2.ID}");

            var proxy3 = RemoteExecutionClient.CreateRemoteInstance<List<int>>(RemoteExecutionName, 5);
            Console.WriteLine($"Created remote object with ID: {proxy3.ID}");
            proxy3.InvokeAsync("Add", 3).Wait();
            proxy3.InvokeAsync("Add", 5).Wait();
            var index = proxy3.InvokeAsync<int>("IndexOf", 5).Result;
            var count = proxy3.GetPropertyAsync<int>("Count").Result;
            Console.WriteLine($"Added items to remote list (count: {count}) and found index of '5' is: {index}");

            var proxy4 = RemoteExecutionClient.CreateRemoteInstance<RemoteProxy>(RemoteExecutionName, typeof(Program), RemoteExecutionName, null);
            Console.WriteLine($"Created remote object with ID: {proxy4.ID}");
        }
    }
}