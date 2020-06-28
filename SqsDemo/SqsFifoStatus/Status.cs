using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amazon;
using Amazon.SQS;

namespace SqsFifoStatus
{
    class Status
    {
        static async Task Main(string[] args)
        {
            string queueUrl = $"https://sqs.<Region e.g. us-east-2>.amazonaws.com/<Account number>/{args[0]}";
            const string accessKey = "<Access Key>";
            const string secretKey = "<Secret Key>";
            var attributes = new List<string> { "ApproximateNumberOfMessages", "ApproximateNumberOfMessagesNotVisible" };

            IAmazonSQS sqsClient = new AmazonSQSClient(accessKey, secretKey, RegionEndpoint.USEast2);

            while (true)
            {
                var response = await sqsClient.GetQueueAttributesAsync(queueUrl, attributes);
                Console.Clear();
                Console.WriteLine($"Visible messages:    {response.ApproximateNumberOfMessages} msgs");
                Console.WriteLine($"Processing messages: {response.ApproximateNumberOfMessagesNotVisible} msgs");
                Thread.Sleep(1000);
            }
        }
    }
}
