using CQRS.Core.Domain;
using Post.Common.Events;

namespace Post.Cmd.Domain.Aggregate;

public class PostAggregate: AggregateRoot
{
     private bool _active;
     private string _author;
     public record Comment(string Username, string Text);
     private readonly Dictionary<Guid, Comment> _comments = new();


     public bool Active => _active;

     //Aggragate should always have empty ctor 
     public PostAggregate()
     {
          
     }

     public PostAggregate(Guid id, string author, string message)
     {
          RaiseEvent(new PostCreatedEvent
          {
               Id = id,
               Author = author,
               Message = message,
               DatePosted = DateTime.Now
          });
     }

     public void Apply(PostCreatedEvent @event)
     {
          _id = @event.Id;
          _active = true;
          _author = @event.Author;
     }

     public void EditMessage(string message)
     {
          if (!_active)
          {
               throw new InvalidOperationException("You cannot edit message of an inactive post.");
          }

          if (string.IsNullOrWhiteSpace(message))
          {
               throw new InvalidOperationException(
                    $"The name of {nameof(message)} cannot be null or empty. " +
                    $"Please provide a valid {nameof(message)}.");
          }
          
          RaiseEvent(new MessageUpdatedEvent
          {
               Id  = _id,
               Message = message
          });
     }

     public void Apply(MessageUpdatedEvent @event)
     {
          _id = @event.Id;
     }

     public void LikePost()
     {
          if (!_active)
          {
               throw new InvalidOperationException("You cannot like an inactive post.");
          }
          
          RaiseEvent(new PostLikedEvent()
          {
               Id  = _id,
          });
     }

     public void Apply(PostLikedEvent @event)
     {
          _id = @event.Id;
     }

     public void AddComment(string comment, string username)
     {
          if (!_active)
          {
               throw new InvalidOperationException("You cannot add a comment an inactive post.");
          } 
          if (string.IsNullOrWhiteSpace(comment))
          {
               throw new InvalidOperationException(
                    $"The name of {nameof(comment)} cannot be null or empty. " +
                    $"Please provide a valid {nameof(comment)}.");
          }
          
          RaiseEvent(new CommentAddedEvent()
          {
               Id  = _id,
               CommentId = Guid.NewGuid(),
               Comment = comment,
               Username = username,
               CommentDate = DateTime.Now
          });
     }

     public void Apply(CommentAddedEvent @event)
     {
          //do I need to set Id again or it is redundant?!
          _id = @event.Id;
          _comments.Add(
               @event.CommentId, 
               new Comment(@event.Username, @event.Comment));
     }
}