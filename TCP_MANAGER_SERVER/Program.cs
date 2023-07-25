
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using TCP_MANAGER_SERVER;

var ip= IPAddress.Loopback;
var port = 27001;

var listener= new TcpListener(ip, port);
listener.Start();

while (true)
{
    var client= listener.AcceptTcpClient();
    var stream= client.GetStream();
    var br = new BinaryReader(stream);
    var bw= new BinaryWriter(stream);
    while (true)
    {
        var input =br.ReadString();
        var command=JsonSerializer.Deserialize<Command>(input);
        if(command is null) { continue; }
        Console.WriteLine(command.Text);
        Console.WriteLine(command.Param);
        switch (command.Text)
        {
            case Command.ProcessList:
                var processes = Process.GetProcesses();
                var processNames = JsonSerializer.Serialize(processes.Select(p => p.ProcessName));
                bw.Write(processNames);
                break;
            case Command.Kill:
                string processName = command.Param!;
                Process[] processess = Process.GetProcessesByName(processName);
                foreach (var process in processess) { process.Kill(); }
                bw.Write("Process started successfully");

                break;
            case Command.Run:
                string processName1 = command.Param!;
                Process.Start(processName1);
                break;
            default:
                break;
        }
    }
}
