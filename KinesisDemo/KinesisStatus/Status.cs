using System;
using System.Threading;
using System.Threading.Tasks;
using Amazon;
using Amazon.Kinesis;
using Amazon.Kinesis.Model;

namespace KinesisStatus
{
    class Status
    {
        static async Task Main(string[] args)
        {
            const string accessKey = "<Access Key>";
            const string secretKey = "<Secret Key>";

            AmazonKinesisClient client = new AmazonKinesisClient(accessKey, secretKey, RegionEndpoint.USEast2);
            var request = new ListShardsRequest()
            {
                StreamName = args[0]
            };
            while (true)
            {
                var response = await client.ListShardsAsync(request);
                Console.Clear();
                foreach (var shard in response.Shards)
                {
                    Console.WriteLine($"Shard {shard.ShardId}");
                }
                Thread.Sleep(1000);
            }
        }
    }
}
