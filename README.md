# Rabbit RPC
A lightweight cross-platform RPC.
## Features
1. Apache License 2.0 protocol open source
2. Supports client load balancing (polling and random)
3. Support ZooKeeper and file sharing the service coordination
4. Runtime client proxy generation (based Roslyn)
5. Pre-generated client agent (based Roslyn)
6. Abstract codec (JSON and ProtoBuffer)
7. Abstract transmission channel (DotNetty)
8. Exception information transfer (Local exceptions to the server runtime can be passed to the client)
9. **NET Core Project structure**
10. **Cross-platform**

## Overview
![](http://images2015.cnblogs.com/blog/384997/201607/384997-20160708082111186-595090265.png)
### Rabbit.Rpc
1. Rpc core class library, has the following functions:
2. Service Id generation
3. Transfer the message model
4. Type conversion
5. Service routing abstraction
6. Serializer abstraction (the default provides JSON serializer)
7. Transport abstraction
8. Codec abstraction (default provides JSON codec implementation)
9. Client runtime (address resolver, address selector, remote call service)
10. Service-side runtime (service entry management, service executor, service discovery abstraction, RpcServiceAttribute tagging service discovery implementation)

### Rabbit.Rpc.ProxyGenerator
Service Agent Builder, provides features:

1. Service agent implementation generation
2. Service agent instance creation

### extensions
#### Rabbit.Rpc.Codec.ProtoBuffer
ProtoBuffer protocol codec implementation.

#### Rabbit.Rpc.Coordinate.Zookeeper
Service Routing Management Based on ZooKeeper.

#### Rabbit.Transport.DotNetty
Implementation of DotNetty Transmission.

### tools
#### Rabbit.Rpc.Tests
Unit test project.

#### Rabbit.Rpc.ClientGenerator
Pre-production service agent tool, provides the following functions:

1. Generate the service proxy implementation code file
2. Generate the service agent to implement the assembly file

## Performance Testing
Test environment

OS | CPU | Memory | disk | network | VM
------------ | ------------- | ------------- | ------------- | ------------- | -------------
Windows 10 x64 | I7 3610QM | 16GB | SSD | 127.0.0.1 | no
Ubuntu 16.04 x64 | I7 3610QM | 4GB | SSD | 127.0.0.1 | yes

### Windows10 + NETCoreApp1.0 + JSON protocol
loop 10,000  
first&ensp;&ensp;&ensp;&ensp;2626ms  
second&ensp;2597ms  
third&ensp;&ensp;&ensp;2581ms

### Windows10 + NETCoreApp1.0 + ProtoBuffer protocol
loop 10,000  
first&ensp;&ensp;&ensp;&ensp;2567ms  
second&ensp;2617ms  
third&ensp;&ensp;&ensp;2474ms

### Ubuntu16.04-x64 + NETCoreApp1.0 + JSON protocol
loop 10,000  
first&ensp;&ensp;&ensp;&ensp;3205ms  
second&ensp;3252ms  
third&ensp;&ensp;&ensp;2837ms

### Ubuntu16.04-x64 + NETCoreApp1.0 + ProtoBuffer protocol
loop 10,000  
first&ensp;&ensp;&ensp;&ensp;3391ms  
second&ensp;3391ms  
third&ensp;&ensp;&ensp;3574ms

## related articles
* [拥抱.NET Core，跨平台的轻量级RPC：Rabbit.Rpc](http://www.cnblogs.com/ants/p/5652132.html)
* [.NET轻量级RPC框架：Rabbit.Rpc](http://www.cnblogs.com/ants/p/5605754.html)

## communication
* [QQ Group：384413261（RabbitHub）](http://jq.qq.com/?_wv=1027&k=29DzAfj)
* [Email：majian159@live.com](mailto:majian159@live.com)

https://github.com/RabbitTeam/RabbitCloud/tree/dev
httpclient 支持负载和服务发现

介绍
http://www.cnblogs.com/ants/p/8445965.html
