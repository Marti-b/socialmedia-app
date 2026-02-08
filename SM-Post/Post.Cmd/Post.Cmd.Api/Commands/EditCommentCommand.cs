namespace Post.Cmd.Domain.Commands
public class EditCommentCommand : BaseCommand
{
    public Guid CommentID { get; set; }
    public string Comment { get; set; }
    public string UserName { get; set; }
}