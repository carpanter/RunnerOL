using LitJsonEx;
using Photon.SocketServer;
using RunnerCommon;
using RunnerCommon.Dto;
using RunnerServer.Cache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunnerServer.Logic
{
    public class AccHandler : SingleSend ,IHandler
    {
        public void OnDisConnect(RunnerClient client)
        {
            Caches.accoutCache.OffLine(client);
        }

        public void OnRequest(RunnerClient client, byte subCode, OperationRequest operationRequest)
        {
            switch(subCode)
            {
                case AccoutCode.Register:
                    OnRegister(client, operationRequest);
                    break;
                case AccoutCode.Sigin:
                    OnSigin(client, operationRequest);
                    break;
                case AccoutCode.CreateName:
                    OnCreateName(client, operationRequest);
                    break;
            }
        }

        void OnRegister(RunnerClient client, OperationRequest operationRequest)
        {
            AccoutDto dto = JsonMapper.ToObject<AccoutDto >(operationRequest[0].ToString());
            bool succes = Caches.accoutCache.AddAcc(dto.acc, dto.psw);
            if(succes)
                send(client, OpCode.AccCode, AccoutCode.Register, 0, "注册成功");
            else
                send(client, OpCode.AccCode, AccoutCode.Register, 1, "注册失败");
        }

        void OnSigin(RunnerClient client, OperationRequest operationRequest)
        {
            AccoutDto dtoByClient = JsonMapper.ToObject<AccoutDto>(operationRequest[0].ToString());
            AccoutDto dtoByServer = Caches.accoutCache.CheckAcc(dtoByClient.acc, dtoByClient.psw);
            if (dtoByServer != null)
            {
                Caches.accoutCache.OnLine(client, dtoByServer);
                if (dtoByServer.name != null)
                    send(client, OpCode.AccCode, AccoutCode.Sigin, 0, "登录成功，账号已创建角色", dtoByServer.name, dtoByServer.ID);
                else
                    send(client, OpCode.AccCode, AccoutCode.Sigin, 1, "登录成功，账号未创建角色", dtoByServer.ID);
            }
            else
                send(client, OpCode.AccCode, AccoutCode.Sigin, 2, "账号或密码错误");
        }

        void OnCreateName(RunnerClient client, OperationRequest operationRequest)
        {
            string name = operationRequest[0].ToString();
            bool sucess = Caches.accoutCache.CreateName(client, name);
            if(sucess)
                send(client, OpCode.AccCode, AccoutCode.CreateName, 0, "创建角色成功");
            else
                send(client, OpCode.AccCode, AccoutCode.CreateName, 1, "创建失败，名字重复");
        }
    }
}
