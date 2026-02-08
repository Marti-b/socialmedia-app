namespace Post.Cmd.Domain.Commands

public class EditMessageCommand : BaseCommand
{
    public string Message { get; set; }
}