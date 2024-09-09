namespace AdventureWorks.Identity.Application.Dto;

public class CreateRoleDto(string name)
{
    public string Name { get; set; } = name;
}