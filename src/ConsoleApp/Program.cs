﻿using org.apache.zookeeper;
using RabbitCloud.Abstractions;
using RabbitCloud.Rpc.Abstractions.Codec;
using RabbitCloud.Rpc.Abstractions.Internal;
using RabbitCloud.Rpc.Abstractions.Protocol;
using RabbitCloud.Rpc.Abstractions.Proxy;
using RabbitCloud.Rpc.Abstractions.Proxy.Castle;
using RabbitCloud.Rpc.Cluster.Abstractions.HaStrategy;
using RabbitCloud.Rpc.Cluster.Abstractions.Internal;
using RabbitCloud.Rpc.Cluster.Abstractions.LoadBalance;
using RabbitCloud.Rpc.Default;
using RabbitCloud.Rpc.Default.Service;
using System;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class UserModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    public interface IUserService
    {
        void Test();

        Task Test2();

        Task<string> Test3();

        Task Test4(UserModel model);
    }

    internal class UserService : IUserService
    {
        public void Test()
        {
            Console.WriteLine("test");
        }

        public Task Test2()
        {
            Console.WriteLine("test2");
            return Task.CompletedTask;
        }

        public Task<string> Test3()
        {
            //            Console.WriteLine("test3");
            return Task.FromResult("3");
        }

        public Task Test4(UserModel model)
        {
            Console.WriteLine(model.Name);
            return Task.CompletedTask;
        }
    }

    internal class MyClass : Watcher
    {
        #region Overrides of Watcher

        /// <summary>Processes the specified event.</summary>
        /// <param name="event">The event.</param>
        /// <returns></returns>
        public override Task process(WatchedEvent @event)
        {
            return Task.CompletedTask;
        }

        #endregion Overrides of Watcher
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var url1 = new Url("rabbitrpc://127.0.0.1:9981/test/a?a=1&b=2");
            var url2 = new Url("rabbitrpc://127.0.0.1:9982/test/a?a=1");
            Task.Run(async () =>
            {
                ICodec codec = new RabbitCodec();
                IProtocol protocol = new RabbitProtocol(new ServerTable(codec), new ClientTable(codec));

                protocol.Export(new DefaultProvider(() => new UserService(), url1, typeof(IUserService)), url1);
                protocol.Export(new DefaultProvider(() => new UserService(), url1, typeof(IUserService)), url2);

                //                                var registry = new ZookeeperRegistryFactory().GetRegistry(new Url("zookeeper://172.18.20.132:2181"));

                /*                var registry = new RedisRegistryFactory().GetRegistry(new Url("redis://?connectionString=127.0.0.1:6379&database=-1&application=test"));

                                await registry.Subscribe(url1, async (url, urls) =>
                                {
                                    foreach (var url3 in urls)
                                    {
                                        Console.WriteLine(url3);
                                    }
                                    await Task.CompletedTask;
                                });

                                await registry.Register(url1);
                                await registry.Register(url2);*/
                /*                foreach (var u in await registry.Discover(url1))
                                {
                                    Console.WriteLine(u);
                                }*/

                var referer = protocol.Refer(typeof(IUserService), url1);

                var cluster = new DefaultCluster
                {
                    HaStrategy = new FailfastHaStrategy(),
                    LoadBalance = new RoundRobinLoadBalance()
                };
                cluster.OnRefresh(new[] { referer });

                IProxyFactory factory = new CastleProxyFactory();

                var invocationHandler = new RefererInvocationHandler(cluster);
                var userService = factory.GetProxy<IUserService>(invocationHandler.Invoke);

                userService.Test();
                await userService.Test2();
                Console.WriteLine(await userService.Test3());
                await userService.Test4(new UserModel
                {
                    Id = 1,
                    Name = "test"
                });

                await Task.CompletedTask;
            }).Wait();
            Console.ReadLine();
        }
    }
}