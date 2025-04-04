﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class Attachments
{
    [Key] public Guid Id { get; set; }
    public Guid PostId { get; set; }
    public string Type { get; set; }
    public string FileName { get; set; }
    public string RealName { get; set; }
   [JsonIgnore] public Post Post { get; set; }
}