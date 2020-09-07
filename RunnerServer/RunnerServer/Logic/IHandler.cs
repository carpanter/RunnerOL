using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunnerServer.Logic
{
    public interface IHandler
    {
        void OnRequest(RunnerClient client, byte subCode, OperationRequest operationRequest);

        void OnDisConnect(RunnerClient client);
    }
}
