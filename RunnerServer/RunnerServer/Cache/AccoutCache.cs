using RunnerCommon.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunnerServer.Cache
{
    public class AccoutCache
    {
        Dictionary<string, AccoutDto> DicAccDto = new Dictionary<string, AccoutDto>();
        Dictionary<RunnerClient, AccoutDto> DiClientDto = new Dictionary<RunnerClient, AccoutDto>();

        int id = 0;
        public bool AddAcc(string acc,string psw)
        {
            if (string.IsNullOrEmpty(acc) || string.IsNullOrEmpty(psw))
                return false;
            if (DicAccDto.ContainsKey(acc))
                return false;

            AccoutDto dto = new AccoutDto()
            {
                ID = id++,
                acc = acc,
                psw = psw
            };
            DicAccDto[acc] = dto;
            return true;
        }

        public AccoutDto CheckAcc(string acc,string psw)
        {
            if (string.IsNullOrEmpty(acc) || string.IsNullOrEmpty(psw))
                return null;
            if (!DicAccDto.ContainsKey(acc))
                return null;
            if (DicAccDto[acc].psw != psw)
                return null;
            return DicAccDto[acc];
        }

        public void OnLine(RunnerClient client,AccoutDto accDto)
        {
            if (DiClientDto.ContainsKey(client))
                return;
            DiClientDto[client] = accDto;
        }

        public void OffLine(RunnerClient client)
        {
            if (!DiClientDto.ContainsKey(client))
                return;
            DiClientDto.Remove(client);
        }

        public bool CreateName(RunnerClient client, string name)
        {
            foreach(var dto in DicAccDto.Values)
            {
                if (name == dto.name)
                    return false;
            }
            DiClientDto[client].name = name;

            return true;
        }

        public int GetIDbyClient(RunnerClient client)
        {
            if (!DiClientDto.ContainsKey(client))
                return -1;
            AccoutDto dto = DiClientDto[client];
            if (dto != null)
                return dto.ID;
            else
                return -1;
        }

        public AccoutDto GetDtoByClient(RunnerClient client)
        {
            if (!DiClientDto.ContainsKey(client))
                return null;
            AccoutDto dto = DiClientDto[client];
            return dto;
        }
    }
}
