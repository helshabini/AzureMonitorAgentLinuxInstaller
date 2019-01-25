# AzureMonitorAgentLinuxInstaller

A little tool to deploy Microsoft Monitoring Agent (OMS Agent) onto multiple Linux machines.

To do:
- Installation logic still not working, struggling with SSH.NET
- Handle prequisites installation (especially in offline servers)
- Maybe add Dependency Agent Installation
- Task management and parallelism, right now everything is sequencial
- Load csv files for servers
- Handle .rsa key files for login
