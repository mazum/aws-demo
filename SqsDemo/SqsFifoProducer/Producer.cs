using System;
using System.Threading;
using System.Threading.Tasks;
using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace SqsFifoProducer
{
    class Producer
    {
        static async Task Main(string[] args)
        {
            string queueUrl = $"https://sqs.<Region e.g. us-east-2>.amazonaws.com/<Account number>/{args[0]}";
            const string accessKey = "<Access Key>";
            const string secretKey = "<Secret Key>";

            IAmazonSQS sqsClient = new AmazonSQSClient(accessKey, secretKey, RegionEndpoint.USEast2);

            int i = 1;
            while (true)
            {
                var message = $"{args[1]}-{i++}";
                var request = new SendMessageRequest
                {
                    QueueUrl = queueUrl,
                    MessageBody = message,
                    MessageGroupId = args[1],
                    MessageDeduplicationId = message
                };
                var response = await sqsClient.SendMessageAsync(request);
                Console.Clear();
                if (response != null)
                {
                    Console.WriteLine($"Sent: {message}");
                }
                else
                {
                    Console.WriteLine("Bad Request");
                    break;
                }
                Thread.Sleep(1000);
            }
        }
    }
}
