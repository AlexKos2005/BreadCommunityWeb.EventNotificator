using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace BreadCommunityWeb.EventNotificator.Application.Dto.ResponseDto
{
   public class FactoryResponseDto
    {
        public int Id { get; set; }
        public int ExternalId { get; set; }

        public int UtcOffset { get; set; }

        public string Description { get; set; }

        public FactoryActionsInfoResponseDto EnterpriseActionsInfo { get; set; }
    }
}
