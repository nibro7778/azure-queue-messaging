
using System;
using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Queue; // Namespace for Queue storage types

namespace HelloWorld
{
    public class Program
    {
        static void Main(string[] args)
        {

            //Step 1: Setting up storge account and create the queue

            // Parse the connection string and return a reference to the storage account.
            var storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the queue client.
            var queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a container.
            var queue = queueClient.GetQueueReference("local-nt");

            // Create the queue if it doesn't already exist
            queue.CreateIfNotExists();

            //Step 2: Send the message

            // Create a message and add it to the queue.
            var message = new CloudQueueMessage("Hello, World final time");
            queue.AddMessage(message);

            //Step 3: Receive the message

            // Get the next message
            var retrievedMessage = queue.GetMessage();

            // Delete the message from queue
            queue.DeleteMessage(retrievedMessage);

            // Display message.
            Console.WriteLine(retrievedMessage.AsString);
            Console.Read();
        }
    }
}
