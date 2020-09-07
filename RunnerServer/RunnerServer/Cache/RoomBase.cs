using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RunnerServer.Cache
{
    public class RoomBase
    {
        public int ID;

        int count;
        public Dictionary<int, RunnerClient> dicIDClient;

        public RoomBase(int id,int cout)
        {
            this.ID = id;
            this.count = cout;
            dicIDClient = new Dictionary<int, RunnerClient>();
        }

        public bool EnterRoom(RunnerClient client, int playerID)
        {
            if (dicIDClient.ContainsKey(playerID))
                return false;
            dicIDClient.Add(playerID, client);
            return true;
        }

        public bool LeaveRoom(RunnerClient client, int playerID)
        {
            if (!dicIDClient.ContainsKey(playerID))
                return false;
            dicIDClient.Remove(playerID);
            return true;
        }

        public bool isFull()
        {
            if (dicIDClient.Count >= count)
                return true;
            else
                return false;
        }

        public bool isEmpty()
        {
            if (dicIDClient.Count == 0)
                return true;
            else
                return false;
        }

        public void Brocast(byte code, byte subCode, byte retCode, string msg, params object[] parameters)
        {
            OperationResponse response = new OperationResponse();
            response.OperationCode = code;
            response.ReturnCode = retCode;
            response.DebugMessage = msg;
            response.Parameters = new Dictionary<byte, object>();
            response[80] = subCode;
            for (int i = 0; i < parameters.Length; i++)
                response[(byte)i] = parameters[i];
            foreach(var client in dicIDClient.Values)
            {
                client.SendOperationResponse(response, new SendParameters());
            }    
            
        }
    }
}
