using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SocialMediaAppAna.Models;
namespace SocialMediaAppAna.Models;
public class Post
{
    [Key]
    public int Id { get; set; }
    //validare pt required ?? si min/max length
    public string Content { get; set; }
    public DateTime Date { get; set; }

    //imagine
    public string? Image { get; set; }

    [NotMapped]
    public IFormFile? ImageFile { get; set; }

    //video
    public string? Video { get; set; }

    // cheie externa user - o postare e asociata unui user
    public string? UserId { get; set; }
    // o postare are o colectie de comantarii 

    // PASUL 6: useri si roluri
    // proprietatea virtuala - un articol este postat de catre un user
    public virtual ApplicationUser? User { get; set; }
    public virtual ICollection<Comment>? Comments { get; set; }
}