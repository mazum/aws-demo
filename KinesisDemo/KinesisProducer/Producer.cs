using System;
using Amazon;
using Amazon.Kinesis;
using Amazon.Kinesis.Model;

namespace KinesisProducer
{
    class Producer
    {
        static void Main(string[] args)
        {
            const string accessKey = "<Access Key>";
            const string secretKey = "<Secret Key>";

            AmazonKinesisClient client = new AmazonKinesisClient(accessKey, secretKey, RegionEndpoint.USEast2);

            int i = 1;

            var message = $"{args[1]}-{i++}";
            var messageBytes = System.Text.Encoding.UTF8.GetBytes(message);
            var messageBase64 = Convert.ToBase64String(messageBytes);
            Console.WriteLine(messageBase64);
            var request = new PutRecordRequest
            {
                Data = messageBase64
            };
        }
    }
}
