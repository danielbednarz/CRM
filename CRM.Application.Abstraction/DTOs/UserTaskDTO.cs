﻿using CRM.Infrastructure.Dictionaries;

namespace CRM.Application.Abstraction
{
    public class UserTaskDTO
    {
        public Guid Id { get; set; }
        public string Signature { get; set; }
        public string Description { get; set; }
        public int? AssignedUserId { get; set; }
        public int? SupervisorId { get; set; }
        public DateTime? CompletionDate { get; set; }
        public int? ClientId { get; set; }
        public string Step { get; set; }
        public string Priority { get; set; }
        public int CreatedById { get; set; }
    }
}