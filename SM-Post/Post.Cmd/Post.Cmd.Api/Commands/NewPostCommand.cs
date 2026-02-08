namespace Post.Cmd.Domain.Commands
public class NewPostCommand : BaseCommand
{
  public string Author { get; set; }
  public string Message { get; set; }
} 