using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WingsOn.Api.Models.Patch;

public class PersonPatchRequest
{
    [FromBody, Required]
    public string Address { get; set; } = null!;
}
