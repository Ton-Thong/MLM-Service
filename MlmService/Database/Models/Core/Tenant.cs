﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MlmService.Database.Models.Core;

public sealed class Tenant
{
    [Key]
    public Guid Id { get; set; }
    public ICollection<User> Users { get; set; }
}