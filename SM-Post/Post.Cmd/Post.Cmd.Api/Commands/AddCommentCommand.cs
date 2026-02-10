namespace Post.Cmd.Domain.Commands;

public class AddCommentCommand : BaseCommand
{
    public string Comment { get; set; }
    public string UserName { get; set; }  
}