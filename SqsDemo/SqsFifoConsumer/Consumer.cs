using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Amazon;
using Amazon.SQS;

namespace SqsFifoConsumer
{
    class Consumer
    {
        static async Task Main(string[] args)
        {
            string queueUrl = $"https://sqs.<Region e.g. us-east-2>.amazonaws.com/<Account number>/{args[0]}";
            const string accessKey = "<Access Key>";
            const string secretKey = "<Secret Key>";

            IAmazonSQS sqsClient = new AmazonSQSClient(accessKey, secretKey, RegionEndpoint.USEast2);
            Console.Clear();

            while (true)
            {
                var message = await sqsClient.ReceiveMessageAsync(queueUrl);
                if (message.Messages.Count == 0) continue;
                Console.WriteLine($"Received: {message.Messages.First().Body}");
                Thread.Sleep(1500);
                var response = await sqsClient.DeleteMessageAsync(queueUrl, message.Messages.First().ReceiptHandle);
                Console.Clear();
                if (response.HttpStatusCode == HttpStatusCode.OK) continue;
                Console.WriteLine("Bad Request");
                break;
            }
        }
    }
}
