# AzureMonitorAgentLinuxInstaller

A little tool to deploy Microsoft Monitoring Agent (OMS Agent) onto multiple Linux machines.

Here are the objective of the tool:
- Automatically download OMS Agents and Transfer them to the target computer
- Check whether all the prerequisites are installed on the target computer
- Download, transfer, and install any missing prerequisites on the target computer
- Install the agent and Onboard the computer onto the Log Analytics Workspace
- Configure proxy configuration for the agent if the computer has no direct connectivity

To do:
- Installation logic still not working, struggling with SSH.NET
- Handle prequisites installation (especially in offline servers)
- Maybe add Dependency Agent Installation
- Task management and parallelism, right now everything is sequencial
- Load csv files for servers
- Handle .rsa key files for login
- Logging, error handling, etc.
