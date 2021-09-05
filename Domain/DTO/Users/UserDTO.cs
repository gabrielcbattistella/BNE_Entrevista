﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Users
{
    public record UserDTO
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Email { get; init; }
        public string Phone { get; init; }
        public string Address { get; init; }
        public string Gender { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}
