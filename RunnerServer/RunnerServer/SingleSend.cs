using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunnerServer
{
    public class SingleSend
    {
        protected void send(RunnerClient client, byte code, byte subCode, byte retCode, string msg, params object[] parameters)
        {
            OperationResponse response = new OperationResponse();
            response.OperationCode = code;
            response.ReturnCode = retCode;
            response.DebugMessage = msg;
            response.Parameters = new Dictionary<byte, object>();
            response[80] = subCode;
            for (int i=0; i < parameters.Length; i++)
                response[(byte)i] = parameters[i];
            client.SendOperationResponse(response, new SendParameters());
        }
    }
}
